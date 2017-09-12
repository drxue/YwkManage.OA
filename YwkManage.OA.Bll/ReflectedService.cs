using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.Common;
using YwkManage.OA.IBll;
using YwkManage.OA.Model.ModelClass;

namespace YwkManage.OA.Bll
{
    /// <summary>
    /// 数据管理类，负责数据库数据的导入及导出，使用用户选择的excel等文件导入初始数据，指定结构的数据导出到文件。
    /// </summary>
    public partial class ReflectedService : IReflectedService
    {

        #region 从配置文件读取模型类程序及信息
        private static readonly string modelAssemblyName = ConfigurationManager.AppSettings.Get("modelAssemblyName");
        private static readonly string modelNameSpace = ConfigurationManager.AppSettings.Get("modelNameSpace");
        private static readonly string dalAssemblyName = ConfigurationManager.AppSettings.Get("dalAssemblyName");
        private static readonly string dalNameSpace = ConfigurationManager.AppSettings.Get("dalNameSpace");
        private static Assembly modelAssembly = Assembly.Load(modelAssemblyName);
        private static Assembly dalAssembly = Assembly.Load(dalAssemblyName);
        #endregion

        //获取Model,Dal的类型属性
        public string ModelName { get; set; }       //模型类名称
        public Type DalType { get; set; }           //Dal的类型
        public object DalInstance { get; set; }     //Dal的实例
        public Type ModelType { get; set; }         //模型的类型
        public object ModelInstance { get; set; }   //模型的实例
        //设置构造方法
        public ReflectedService(string modelName)
        {
            ModelName = modelName;
            //获取Dal的类型
            DalType = GetDalType();
            //获取模型类Dal的实例
            DalInstance = GetDalInstance();
            //获取模型类类型
            ModelType = GetModelType();
        }

        /// <summary>
        /// 添加记录到数据库
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        public object AddEntity(object modelInstance)
        {
            //动态调用Dal中的LoadEntities方法。
            MethodInfo mi = DalType.GetMethod("AddEntity");
            //创建调用参数
            object[] parameters = new object[] { modelInstance };
            //动态调用Dal中的方法。
            object temp = mi.Invoke(DalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            return temp;
        }

        /// <summary>
        /// 添加DataTable模型集合到数据库
        /// </summary>
        /// <param name="dt">含有标题行的DataTable</param>
        /// <returns></returns>
        public bool AddEntities(DataTable dt)
        {
            //1 创建类实例
            int rowsLength = dt.Rows.Count;
            int columnLength = dt.Columns.Count;
            object modelInstance = GetModelInstance();
            PropertyInfo[] propertyInfoes = ModelType.GetProperties();

            //3 循环dt行，赋值类属性，添加记录
            foreach (DataRow dr in dt.Rows)
            {
                object obj = GetModelInstance();
                foreach (DataColumn dc in dt.Columns)
                {
                    foreach (PropertyInfo t in propertyInfoes)
                    {
                        //想获取属性是否为自增量，目前不知道如何获取，如果为自增量属性，不需要赋值。
                        //非virtual属性并且名称相同并且非空
                        //DBNull与null不同，DBNull.Value是其唯一实例，表示数据为空，是个对象
                        if (dc.ColumnName == t.Name && dr[dc.ColumnName] != System.DBNull.Value)
                        {
                            //转换为指定类型的对象。
                            var value = Convert.ChangeType(dr[dc.ColumnName], t.PropertyType);
                            t.SetValue(obj, value, null);//给对象赋值
                        }
                    }

                }
                AddEntity(obj);//将对象填充到数据库
            }
            //4 保存数据
            return SaveChanges();
        }

        /// <summary>
        /// 添加记录集合到数据库
        /// </summary>
        /// <param name="modelInstances"></param>
        /// <returns></returns>
        public bool AddEntities(List<object> modelInstances)
        {
            //动态调用Dal中的LoadEntities方法。
            MethodInfo mi = DalType.GetMethod("AddEntity");
            int length = modelInstances.Count();
            for (int i = 0; i < length; i++)
            {
                //创建调用参数
                object[] parameters = new object[] { modelInstances[i] };
                //动态调用Dal中的方法。
                object temp = mi.Invoke(DalInstance, parameters);
            }
            return SaveChanges();
        }

        /// <summary>
        /// 删除记录到数据库
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        public bool DeleteEntity(object modelInstance)
        {
            MethodInfo mi = DalType.GetMethod("DeleteEntity");
            object[] parameters = new object[] { modelInstance };
            mi.Invoke(DalInstance, parameters);
            return SaveChanges();

        }

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        public bool EditEntity(object modelInstance)
        {
            MethodInfo mi = DalType.GetMethod("EditEntity");
            object[] parameters = new object[] { modelInstance };
            mi.Invoke(DalInstance, parameters);
            return SaveChanges();

        }

        /// <summary>
        /// 获取类的所有数据集合
        /// </summary>
        /// <returns></returns>
        public IQueryable LoadAllEntities()
        {
            //创建MethodInfo
            MethodInfo methodLoadEntities = DalType.GetMethod("LoadEntities");
            //创建查询用Lambda表达式
            Expression expression = LambdaHelper.True(ModelType);
            //生成调用参数数组
            object[] parameters = new object[] { expression };
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(DalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            return obj as IQueryable;
        }

        /// <summary>
        /// 获取全部集合数据到DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable LoadAllEntitiestoDataTable()
        {
            //获取所有IQueryable类型数据，调用上面的现成方法
            IQueryable queryable = LoadAllEntities();
            //创建DataTable
            DataTable dt = new DataTable(ModelName);
            PropertyInfo[] propertyInfoes = ModelType.GetProperties();
            //循环data赋值
            foreach (PropertyInfo propertyInfo in propertyInfoes)
            {
                Type t = propertyInfo.PropertyType;
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    continue;
                }
                if (t == typeof(Nullable<DateTime>))
                {
                    t = typeof(DateTime);
                }
                else if (t == typeof(Nullable<int>))
                {
                    t = typeof(int);
                }
                else if (t == typeof(Nullable<bool>))
                {
                    t = typeof(bool);
                }
                dt.Columns.Add(propertyInfo.Name, t);//设置表头
            }
            foreach (var model in queryable)
            {
                DataRow row = dt.NewRow();//创建新行
                foreach (PropertyInfo propertyInfo in propertyInfoes)
                {
                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        continue;
                    }
                    row[propertyInfo.Name] = propertyInfo.GetValue(model);//将属性值赋给每一列
                }
                dt.Rows.Add(row);//添加新行
            }
            return dt;
        }

        /// <summary>
        /// 获取模型类的部分集合
        /// </summary>
        /// <param name="parameters">选择条件的Lambda表达式参数</param>
        /// <returns></returns>
        public IQueryable LoadEntities(Expression whereLambda)
        {
            MethodInfo methodLoadEntities = DalType.GetMethod("LoadEntities");
            //创建参数
            object[] parameters = new object[] { whereLambda };
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(DalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            return obj as IQueryable;
        }

        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalIndex">总记录数 out返回</param>
        /// <param name="whereLambda">查询lambda表达式，由LambdaHelper生成</param>
        /// <param name="orderbyLambda">排序lambda表达式，由LambdaHelper生成</param>
        /// <param name="isAsc">升序标志</param>
        /// <returns></returns>
        public IQueryable LoadPageEntities(int pageSize, int pageIndex, out int totalIndex, Expression whereLambda, Expression orderbyLambda, bool isAsc)
        {
            //创建MethodInfo
            MethodInfo methodLoadEntities = DalType.GetMethod("LoadPageEntities");
            //创建参数数组
            totalIndex = 0;
            object[] parameters = new object[] { pageSize, pageIndex, totalIndex, whereLambda, orderbyLambda, isAsc };
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(DalInstance, parameters);
            SaveChanges();
            return obj as IQueryable;
        }

        /// <summary>
        /// 动态调用指定model的dal相应方法
        /// </summary>
        /// <param name="methodName">调用方法的名称</param>
        /// <param name="parameters">动态调用方法需要的参数</param>
        /// <returns></returns>
        public object DalMethiodInvoke(string methodName, object[] parameters)
        {
            //创建MethodInfo
            MethodInfo mi = DalType.GetMethod(methodName);
            //调用mi方法
            object result = mi.Invoke(DalInstance, parameters);
            SaveChanges();
            return result;
        }

        /// <summary>
        /// 调用Dal的savechanges方法
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            MethodInfo mi = DalType.GetMethod("SaveChanges");
            object[] parameters = new object[] { };
            object obj = mi.Invoke(DalInstance, parameters);
            if (obj!=null)
            {
                return (bool)obj;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取模型类实例
        /// </summary>
        /// <returns></returns>
        public object GetModelInstance()
        {
            string fullNameSpace = modelNameSpace + '.' + ModelName;
            return modelAssembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取Dal类实例
        /// </summary>
        /// <returns></returns>
        public object GetDalInstance()
        {

            string fullNameSpace = dalNameSpace + '.' + ModelName + "Dal";
            return dalAssembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取模型类类型
        /// </summary>
        /// <returns></returns>
        public Type GetModelType()
        {
            return modelAssembly.GetType(modelNameSpace + '.' + ModelName);
        }

        /// <summary>
        /// 获取Dal类类型
        /// </summary>
        /// <returns></returns>
        public Type GetDalType()
        {
            return dalAssembly.GetType(dalNameSpace + '.' + ModelName + "Dal");
        }

        #region 扩展方法，将IQueryable集合对象转换为DataTable对象,暂时未启用
        //public static DataTable ConvertToDataTable(this IQueryable queryable)
        //{
        //    var dataTable = new DataTable();
        //    foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(modeltype))
        //    {
        //        dataTable.Columns.Add(pd.Name, pd.PropertyType);
        //    }
        //    foreach (T item in queryable)
        //    {
        //        var Row = dataTable.NewRow();

        //        foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
        //        {
        //            Row[pd.Name] = pd.GetValue(item);
        //        }
        //        dataTable.Rows.Add(Row);
        //    }
        //    return dataTable;

        //}
        #endregion

    }
}

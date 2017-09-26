using System;
using System.Collections;
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
using System.Windows;

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
            DalType = GetDalType(modelName);
            //获取模型类Dal的实例
            DalInstance = GetDalInstance(modelName);
            //获取模型类类型
            ModelType = GetModelType(modelName);
            //获取模型类实例
            ModelInstance = GetModelInstance(modelName);
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
        public int AddEntities(DataTable dt)
        {
            //1 创建类实例
            if (dt == null)
            {
                return 0;
            }
            int updateRows = 0;
            int unUpdateRows = 0;
            int rowsLength = dt.Rows.Count;
            int columnLength = dt.Columns.Count;
            
            PropertyInfo[] propertyInfoes = ModelType.GetProperties();
            //1 动态调用Dal中的LoadEntities方法。
            
            //2 循环dt行，赋值类属性，添加记录
            foreach (DataRow dr in dt.Rows)
            {
                var instanceModel = GetModelInstance(ModelName);
                object resultInstanceModele = null; //通过LoadEntity方法调用的返回值
                foreach (DataColumn dc in dt.Columns)
                {
                    // 如果为空值，跳过。
                    if (Convert.IsDBNull(dr[dc.ColumnName]) || dr[dc.ColumnName] == null)
                    {
                        continue;
                    }
                    foreach (PropertyInfo t in propertyInfoes)
                    {
                        //想获取属性是否为自增量，目前不知道如何获取，如果为自增量属性，不需要赋值。
                        //非virtual属性并且名称相同并且非空
                        //DBNull与null不同，DBNull.Value是其唯一实例，表示数据为空，是个对象
                        if (dc.ColumnName == t.Name)  /*&& dr[dc.ColumnName] != System.DBNull.Value*/
                        {
                            //If the property's type is a class and is a model class. 
                            if (t.PropertyType.IsClass && t.PropertyType.Namespace ==modelNameSpace)
                            {
                                //load the entity
                                //string propertyName = t.PropertyType.GetProperties()[0].Name;//属性名称，需要创建类的key，一般为第一个属性
                                //Type propertyType = t.PropertyType.GetProperties()[0].GetType();//获取key的类型
                                //var propertyValue = Convert.ChangeType(dr[dc.ColumnName], propertyType);//将dt中的数据转换成key类型
                                //Expression lambda = LambdaHelper.CreateEqual(t.PropertyType, propertyName, propertyValue);//构建查询lambda表达式
                                //ReflectedService rs2 = new ReflectedService(t.PropertyType.FullName);//创建一个反射类实例，也就是该方法所在的类。由于需要制定另外一个模型，所以重新创建。
                                //var propertyClassInStance = rs2.LoadEntity(lambda);//通过调用其中的方法查询一个记录，返回一个模型类实例。
                                //if (propertyClassInStance != null)
                                //{
                                //    t.SetValue(instanceModel, propertyClassInStance, null);//给对象赋值
                                //}
                            }
                            else
                            {
                                var dtValue = dr[dc.ColumnName];
                                var value = ChangeType(dtValue, t.PropertyType);//调用自己的类型转换方法
                                t.SetValue(instanceModel, value, null);//给对象赋值
                            }
                        }
                    }//end foreach (PropertyInfo t in propertyInfoes)
                }//end  foreach (DataColumn dc in dt.Columns)
                 //完成模型实例创建准备添加
                 //判定数据库中是否存在相同记录.howhowhow？
                #region 判断记录是否已经存在于数据库库中，如果存在则修改，不存在添加,exist标记结果
                //public static LambdaExpression CreateEqual(Type T, string propertyName, object propertyValue)
                bool exist = false;
                string propertyName = propertyInfoes[0].Name;
                var propertyValue = propertyInfoes[0].GetValue(instanceModel);
                LambdaExpression exp = Common.LambdaHelper.CreateEqual(ModelType, propertyName, propertyValue);
                MethodInfo MethodInfo = DalType.GetMethod("LoadEntity");
                object[] parametersLoad = new object[] { exp };
                try
                {
                    resultInstanceModele = MethodInfo.Invoke(DalInstance, parametersLoad);
                    if (resultInstanceModele!=null)
                    {
                        exist = true;
                    }
                    else
                    {
                        exist = false;
                    }
                }
                catch (Exception)
                {
                    exist = false;
                }
                
                #endregion

                if (exist)
                {
                    //复制model
                    foreach (PropertyInfo t in propertyInfoes)
                    {
                      var propertyValue1=  t.GetValue(instanceModel);
                        if (propertyValue1!=null)
                        {
                            t.SetValue(resultInstanceModele, propertyValue1);
                        }
                    }
                    //更新到数据库
                    //创建调用参数
                    object[] parameters = new object[] { resultInstanceModele };
                    MethodInfo miEditEntity = DalType.GetMethod("EditEntity");
                    //动态调用Dal中的方法。
                    try
                    {
                        //4 保存数据
                        miEditEntity.Invoke(DalInstance, parameters);
                        if (SaveChanges())
                        {
                            updateRows += 1;
                        }
                        else
                        {
                            unUpdateRows += 1;
                        }
                    }
                    catch (Exception)
                    {
                        unUpdateRows += 1;
                        continue;
                    }
                }
                else
                {
                    //创建调用参数
                    object[] parameters = new object[] { instanceModel };
                    MethodInfo miAddEntity = DalType.GetMethod("AddEntity");
                    //动态调用Dal中的方法。
                    try
                    {
                        miAddEntity.Invoke(DalInstance, parameters);
                        //4 保存数据
                        if (SaveChanges())
                        {
                            updateRows += 1;
                        }
                        else
                        {
                            unUpdateRows += 1;
                        }
                    }
                    catch (Exception)
                    {
                        unUpdateRows += 1;
                        continue;
                    }
                }
            }//end foreach (DataRow dr in dt.Rows)
            if (updateRows > 0)
            {
                return updateRows;
            }
            else
            {
                return -unUpdateRows;
            }
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
        /// 删除数据库表中所有记录
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllEntities()
        {
            IQueryable iq = LoadAllEntities();
            foreach (var model in iq)
            {
                //使用DeleteEntity方法删除一条记录
                MethodInfo mi = DalType.GetMethod("DeleteEntity");
                object[] parameters = new object[] { model };
                mi.Invoke(DalInstance, parameters);
            }
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

        public int EntityCount()
        {
            MethodInfo mi = DalType.GetMethod("EntityCount");
            //创建参数
            object[] parameters = new object[] { };
            //动态调用Dal中的EntityCount方法。
            var obj = mi.Invoke(DalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return (int)obj;
            }
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public object LoadEntity(Expression whereLambda)
        {
            MethodInfo methodLoadEntities = DalType.GetMethod("LoadEntitiy");
            //创建参数
            object[] parameters = new object[] { whereLambda };
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(DalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            return obj;
        }

        /// <summary>
        /// 查询所有记录
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
        /// 查询所有记录ToList
        /// </summary>
        /// <returns></returns>
        public object LoadAllEntitiesToList()
        {
            //创建MethodInfo
            MethodInfo methodLoadEntities = DalType.GetMethod("LoadEntitiesToList");
            //创建查询用Lambda表达式
            Expression expression = LambdaHelper.True(ModelType);
            //生成调用参数数组
            object[] parameters = new object[] { expression };
            //动态调用Dal中的LoadEntities方法。
            var objList = methodLoadEntities.Invoke(DalInstance, parameters) ;
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges();
            return objList;
        }

        /// <summary>
        /// 获取全部集合数据到DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable LoadAllEntitiestoDataTable()
        {
            DataTable dt = new DataTable(ModelName);
            PropertyInfo[] propertyInfoes = ModelType.GetProperties();
            //循环data赋值
            foreach (PropertyInfo propertyInfo in propertyInfoes)
            {
                Type t = propertyInfo.PropertyType;
                Type columnType = null;
                //不能读取或者为非Nullable<>的泛型
                if (!propertyInfo.CanRead || t.IsGenericType && t.Name != "Nullable`1")
                {
                    continue;
                }
                //Nullable<>类型
                if (t.IsGenericType && t.Name == "Nullable`1") //是个可空的泛型值类型
                {
                    //获取构成该泛型的参数类型，默认取第一个泛型参数，一般我们也用一个参数构成Nullable<>泛型。
                    columnType = t.GenericTypeArguments[0];
                }
                else
                {
                    columnType = t;
                }
                dt.Columns.Add(propertyInfo.Name, columnType);//设置表头
                
            }
            //根据EntityCount方法返回的数据库记录数，决定后续是否要添加记录。
            int entityCount = EntityCount();
            if (entityCount > 0)
            {
                //载入表数据
                IEnumerable modelList = LoadAllEntitiesToList() as IEnumerable;
                //录入DataTable数据
                foreach (var model in modelList) //每个model实例构成一行
                {
                    DataRow row = dt.NewRow();//创建新行
                    foreach (PropertyInfo propertyInfo in propertyInfoes)
                    {
                        //daattable中的列已经经过上面判断可用，现在重新判断类实例的property名称是否在列名称集合中。
                        if (!dt.Columns.Contains(propertyInfo.Name))
                        {
                            continue;
                        }
                        Type t = propertyInfo.PropertyType;

                        #region 判断属性是否是模型类
                        //if (t.IsClass && t.Namespace == modelNameSpace)
                        //{
                        //    object classInstance = propertyInfo.GetValue(model);
                        //    //创建类实例
                        //    if (classInstance == null)
                        //    {
                        //        continue;
                        //    }
                        //    PropertyInfo pi = t.GetProperties()[0]; //获取类的第一个属性，一般为key
                        //    object key = pi.GetValue(classInstance);//将类设置为外键值。
                        //    if (key == null)
                        //    {
                        //        continue;
                        //    }
                        //    row[propertyInfo.Name] = classInstance;//将类设置为外键值。
                        //    continue;
                        //}
                        #endregion

                        var valueTemp = propertyInfo.GetValue(model); //这句错，如果属性为类，重新获取需要重用该类，需要重新创建一个该类实例。在数据库读取过程中重新读取存在问题，改进方式将前面的读取改为ToList。
                        if (Convert.IsDBNull(valueTemp) || valueTemp == null)
                        {
                            continue;
                        }
                        row[propertyInfo.Name] = valueTemp;//将属性值赋给每一列
                    }
                    dt.Rows.Add(row);//添加新行
                }
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
            if (obj != null)
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
        public object GetModelInstance(string modelName)
        {
            string fullNameSpace = modelNameSpace + '.' + modelName;
            return modelAssembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取Dal类实例
        /// </summary>
        /// <returns></returns>
        public object GetDalInstance(string modelName)
        {

            string fullNameSpace = dalNameSpace + '.' + modelName + "Dal";
            return dalAssembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取模型类类型
        /// </summary>
        /// <returns></returns>
        public Type GetModelType(string modelName)
        {
            return modelAssembly.GetType(modelNameSpace + '.' + modelName);
        }

        /// <summary>
        /// 获取Dal类类型
        /// </summary>
        /// <returns></returns>
        public Type GetDalType(string modelName)
        {
            return dalAssembly.GetType(dalNameSpace + '.' + modelName + "Dal");
        }

        /// <summary>
        /// 类型转换，将指定object转换为type类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.Name=="Nullable`1")
            {
                NullableConverter convertor = new NullableConverter(type);
                return Convert.IsDBNull(value) ? null : convertor.ConvertFrom(value);
            }
            else
            {
                return Convert.ChangeType(Convert.IsDBNull(value) ? null : value, type);
            }
            
        }

        /// <summary>
        /// 将DataTable中可写的列选出来转化为Dictionary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private Dictionary<string, PropertyInfo> GetFields(DataTable dt)
        {
            Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();
            int columnCount = dt.Columns.Count;
            PropertyInfo[] properties = ModelType.GetProperties();
            if (properties != null)
            {
                List<string> dtColumns = new List<string>();
                for (int i = 0; i < columnCount; i++)
                {
                    dtColumns.Add(dt.Columns[i].ColumnName.ToString());
                }
                IEnumerable resList =
                    (from PropertyInfo prop in properties
                     where prop.CanWrite && dtColumns.Contains(prop.Name)
                     select prop);
                foreach (PropertyInfo p in resList)
                {
                    result.Add(p.Name, p);
                }
            }
            return result;
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

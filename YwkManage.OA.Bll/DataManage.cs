using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.Common;
using YwkManage.OA.IBll;

namespace YwkManage.OA.Bll
{
    /// <summary>
    /// 数据管理类，负责数据库数据的导入及导出，使用用户选择的excel等文件导入初始数据，指定结构的数据导出到文件。
    /// </summary>
    public partial class DataManage : IDataManage
    {
        #region 从配置文件读取模型类程序及信息
        private static readonly string modelAssemblyName = ConfigurationManager.AppSettings.Get("modelAssemblyName");
        private static readonly string modelNameSpace = ConfigurationManager.AppSettings.Get("modelNameSpace");
        private static readonly string dalAssemblyName = ConfigurationManager.AppSettings.Get("dalAssemblyName");
        private static readonly string dalNameSpace = ConfigurationManager.AppSettings.Get("dalNameSpace");
        private static Assembly modelAssembly = Assembly.Load(modelAssemblyName);
        private static Assembly dalAssembly = Assembly.Load(dalAssemblyName);
        #endregion

        /// <summary>
        /// 动态调用指定model的dal相应方法
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <param name="methodName">调用方法的名称</param>
        /// <param name="parameters">动态调用方法需要的参数</param>
        /// <returns></returns>
        public object DalMethiodInvoke(string modelName, string methodName, object[] parameters)
        {
            //获取Dal的类型
            Type dalType = GetDalType(modelName);
            //动态创建相应model的Dal实例。
            object dalInstance = GetDalInstance(modelName);
            //创建MethodInfo
            MethodInfo mi = dalType.GetMethod(methodName);
            //调用mi方法
            object result = mi.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges(dalInstance, dalType);
            return result;
        }

        /// <summary>
        /// 获取类的所有数据集合
        /// </summary>
        /// <param name="modelName">模型类的字符串名称</param>
        /// <returns></returns>
        public IQueryable LoadAllEntities(string modelName)
        {
            //动态创建相应model的Dal实例。
            var dalInstance = GetDalInstance(modelName);
            //获取模型类的类型
            Type modelType = GetModelType(modelName);
            //获取Dal的类型
            Type dalType = GetDalType(modelName);
            //创建MethodInfo
            MethodInfo methodLoadEntities = dalType.GetMethod("LoadEntities");
            //创建查询用Lambda表达式
            Expression expression = LambdaHelper.True(modelType);
            //生成调用参数数组
            object[] parameters = new object[] { expression };
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges(dalInstance, dalType);
            return obj as IQueryable;
        }

        /// <summary>
        /// 获取模型类的部分集合
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <param name="parameters">选择条件的Lambda表达式参数</param>
        /// <returns></returns>
        public IQueryable LoadEntities(string modelName, object[] parameters)
        { //动态创建相应model的Dal实例。
            var dalInstance = GetDalInstance(modelName);
            //获取模型类的类型
            Type modelType = GetModelType(modelName);
            //获取Dal的类型
            Type dalType = GetDalType(modelName);
            //创建MethodInfo
            MethodInfo methodLoadEntities = dalType.GetMethod("LoadEntities");
            //动态调用Dal中的LoadEntities方法。
            var obj = methodLoadEntities.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges(dalInstance, dalType);
            return obj as IQueryable;
        }

        /// <summary>
        /// 添加记录到数据库
        /// </summary>
        /// <returns></returns>
        public object AddEntity(string modelName, object modelInstance)
        {
            //获取Dal的类型
            Type dalType = GetDalType(modelName);
            //获取模型类的类型
            var dalInstance = GetDalInstance(modelName);
            //动态调用Dal中的LoadEntities方法。
            MethodInfo mi = dalType.GetMethod("AddEntity");
            //创建调用参数
            object[] parameters = new object[] { modelInstance };
            //动态调用Dal中的方法。
            object temp = mi.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges(dalInstance, dalType);
            return true;
        }

        public object AddEntities(string modelName, object[] modelInstances)
        {
            //获取Dal的类型
            Type dalType = GetDalType(modelName);
            //获取模型类的类型
            var dalInstance = GetDalInstance(modelName);
            //动态调用Dal中的LoadEntities方法。
            MethodInfo mi = dalType.GetMethod("AddEntity");
            int count = modelInstances.Count();
            for (int i = 0; i < count; i++)
            {
                //创建调用参数
                object[] parameters = new object[] { modelInstances[i] };
                //动态调用Dal中的方法。
                object temp = mi.Invoke(dalInstance, parameters);
            }

            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            SaveChanges(dalInstance, dalType);
            return true;
        }
        /// <summary>
        /// 删除记录到数据库
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        public bool DeleteEntity(string modelName, object modelInstance)
        {
            Type dalType = GetDalType(modelName);
            var dalInstance = GetDalInstance(modelName);
            MethodInfo mi = dalType.GetMethod("DeleteEntity");
            object[] parameters = new object[] { modelInstance };
            mi.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            return SaveChanges(dalInstance, dalType);

        }

        public bool EditEntity(string modelName, object modelInstance)
        {
            Type dalType = GetDalType(modelName);
            var dalInstance = GetDalInstance(modelName);
            MethodInfo mi = dalType.GetMethod("EditEntity");
            object[] parameters = new object[] { modelInstance };
            mi.Invoke(dalInstance, parameters);
            //BaseService中的各种方法返回值不一样，有的是返回SaveChange()结果，有的是返回具体操作方法的结果。
            return SaveChanges(dalInstance, dalType);

        }


        /// <summary>
        /// 调用Dal的savechanges方法
        /// </summary>
        /// <param name="dalInstance">创建的dal实例</param>
        /// <param name="dalType">生成的dalType</param>
        /// <returns></returns>
        public bool SaveChanges(object dalInstance, Type dalType)
        {
            MethodInfo mi = dalType.GetMethod("SaveChanges");
            object[] parameters = new object[] { };
            return (bool)mi.Invoke(dalInstance, parameters);
        }
        /// <summary>
        /// 获取模型类实例
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public object GetModelInstance(string modelName)
        {
            string fullNameSpace = modelNameSpace + '.' + modelName;
            return modelAssembly.CreateInstance(fullNameSpace);
        }
        /// <summary>
        /// 获取Dal类实例
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public object GetDalInstance(string modelName)
        {

            string fullNameSpace = dalNameSpace + '.' + modelName + "dal";
            return dalAssembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取模型类类型
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public Type GetModelType(string modelName)
        {
            return modelAssembly.GetType(modelNameSpace + '.' + modelName);
        }
        /// <summary>
        /// 获取Dal类类型
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public Type GetDalType(string modelName)
        {
            return dalAssembly.GetType(dalNameSpace + '.' + modelName + "Dal");
        }
    }
}

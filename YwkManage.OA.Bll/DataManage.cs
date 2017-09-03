using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.IBll;

namespace YwkManage.OA.Bll
{
    /// <summary>
    /// 数据管理类，负责数据库数据的导入及导出，使用用户选择的excel等文件导入初始数据，指定结构的数据导出到文件。
    /// </summary>
    public partial class DataManage : IDataManage
    {
        //从配置文件读取模型类程序及信息
        private static readonly string modelAssemblyName = ConfigurationManager.AppSettings.Get("modelAssemblyName");
        private static readonly string modelNameSpace = ConfigurationManager.AppSettings.Get("modelNameSpace");
        private static readonly string dalAssemblyName = ConfigurationManager.AppSettings.Get("dalAssemblyName");
        private static readonly string dalNameSpace = ConfigurationManager.AppSettings.Get("dalNameSpace");
        private Assembly modelassembly = Assembly.Load(modelAssemblyName);
        private Assembly dalassembly = Assembly.Load(dalAssemblyName);
        /// <summary>
        /// 获取类的所有数据集合
        /// </summary>
        /// <param name="modelName">模型类的字符串名称</param>
        /// <returns></returns>
        public IQueryable GetAllEntities(string modelName)
        {
            var dal = GetDalInstance(modelName);
            Type dalType = GetDalType(modelName);
            MethodInfo methodLoadEntities = dalType.GetMethod("LoadEntities");
            object[] par = new object[] { };
            var obj = methodLoadEntities.Invoke(dal, par);
            return obj as IQueryable;
        }
        /// <summary>
        /// 添加记录到数据库
        /// </summary>
        /// <returns></returns>
        public bool AddEntities()
        {
            return true;
        }
        //public bool Read




        /// <summary>
        /// 获取模型类实例
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public object GetModelInstance(string modelName)
        {


            string fullNameSpace = modelNameSpace + '.' + modelName;
            return modelassembly.CreateInstance(fullNameSpace);
        }
        /// <summary>
        /// 获取Dal类实例
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public object GetDalInstance(string modelName)
        {

            string fullNameSpace = dalNameSpace + '.' + modelName + "dal";
            return dalassembly.CreateInstance(fullNameSpace);
        }

        /// <summary>
        /// 获取模型类类型
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public Type GetModelType(string modelName)
        {
            return modelassembly.GetType(modelNameSpace + '.' + modelName);
        }
        /// <summary>
        /// 获取Dal类类型
        /// </summary>
        /// <param name="modelName">模型类名称</param>
        /// <returns></returns>
        public Type GetDalType(string modelName)
        {
            return dalassembly.GetType(dalNameSpace + '.' + modelName + "Dal");
        }
    }
}

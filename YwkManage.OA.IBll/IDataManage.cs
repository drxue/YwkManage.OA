    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.IBll
{
    /// <summary>
    /// 数据管理类接口文件，负责数据库数据的导入及导出，使用用户选择的excel等文件导入初始数据，指定结构的数据导出到文件。
    /// </summary>
    public partial interface IDataManage
    {
        object DalMethiodInvoke(string modelName, string methodName, object[] parameters);
        IQueryable LoadAllEntities(string modelName);
        IQueryable LoadEntities(string modelName, object[] parameters);
        object AddEntity(string modelName, object modelInstance);
        object AddEntities(string modelName, object[] modelInstances);
        bool DeleteEntity(string modelName, object modelInstance);
        bool EditEntity(string modelName, object modelInstance);
        bool SaveChanges(object dalInstance, Type dalType);
        object GetModelInstance(string modelName);
        object GetDalInstance(string modelName);
        Type GetModelType(string modelName);
        Type GetDalType(string modelName);

    }
}

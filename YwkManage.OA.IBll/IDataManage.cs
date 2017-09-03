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
        IQueryable GetAllEntities(string modelName);
        object GetModelInstance(string modelName);
        object GetDalInstance(string modelName);
    }
}

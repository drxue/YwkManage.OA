using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.IBll
{
    /// <summary>
    /// 数据管理类接口文件，负责数据库数据的导入及导出，使用用户选择的excel等文件导入初始数据，指定结构的数据导出到文件。
    /// </summary>
    public partial interface IReflectedService
    {
        //获取Model,Dal的类型属性
        string ModelName { get; set; }       //模型类名称
        Type DalType { get; set; }           //Dal的类型
        object DalInstance { get; set; }     //Dal的实例
        Type ModelType { get; set; }         //模型的类型
        object ModelInstance { get; set; }   //模型的实例

        /// <summary>
        /// 添加记录到数据库
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        object AddEntity(object modelInstance);

        /// <summary>
        /// 添加记录集合到数据库
        /// </summary>
        /// <param name="modelInstances"></param>
        /// <returns></returns>
        bool AddEntities(List<object> modelInstances);

        /// <summary>
        /// 添加DataTable模型集合到数据库
        /// </summary>
        /// <param name="dt">含有标题行的DataTable</param>
        /// <returns></returns>
        int AddEntities(DataTable dt);

        /// <summary>
        /// 删除记录到数据库
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        bool DeleteEntity(object modelInstance);

        /// <summary>
        /// 删除数据库表中所有记录
        /// </summary>
        /// <returns></returns>
        bool DeleteAllEntities();

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="modelInstance"></param>
        /// <returns></returns>
        bool EditEntity(object modelInstance);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        object LoadEntity(Expression whereLambda);

        /// <summary>
        /// 获取类的所有数据集合
        /// </summary>
        /// <returns></returns>
        IQueryable LoadAllEntities();

        /// <summary>
        /// 查询所有记录ToList
        /// </summary>
        /// <returns></returns>
        object LoadAllEntitiesToList();

        /// <summary>
        /// 获取模型类的部分集合
        /// </summary>
        /// <param name="parameters">选择条件的Lambda表达式参数</param>
        /// <returns></returns>
        IQueryable LoadEntities(Expression whereLambda);

        /// <summary>
        /// 获取全部集合数据到DataTable
        /// </summary>
        /// <returns></returns>
        DataTable LoadAllEntitiestoDataTable();

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
        IQueryable LoadPageEntities(int pageSize, int pageIndex, out int totalIndex, Expression whereLambda, Expression orderbyLambda, bool isAsc);

        /// <summary>
        /// 动态调用指定model的dal相应方法
        /// </summary>
        /// <param name="methodName">调用方法的名称</param>
        /// <param name="parameters">动态调用方法需要的参数</param>
        /// <returns></returns>
        object DalMethiodInvoke(string methodName, object[] parameters);

        /// <summary>
        /// 调用Dal的savechanges方法
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();

        /// <summary>
        /// 获取模型类实例
        /// </summary>
        /// <returns></returns>
        object GetModelInstance(string modelName);

        /// <summary>
        /// 获取Dal类实例
        /// </summary>
        /// <returns></returns>
        object GetDalInstance(string modelName);

        /// <summary>
        /// 获取模型类类型
        /// </summary>
        /// <returns></returns>
        Type GetModelType(string modelName);

        /// <summary>
        /// 获取Dal类类型
        /// </summary>
        /// <returns></returns>
        Type GetDalType(string modelName);
    }
}

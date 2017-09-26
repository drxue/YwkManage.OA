using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.Model;

namespace YwkManage.OA.IDal
{
    public interface IBaseDal<T> where T : class, new()
    {
        OAContext Db { get; }
        //增删改查
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T AddEntity(T entity);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditEntity(T entity);

        /// <summary>
        /// 返回总记录数
        /// </summary>
        /// <returns></returns>
        int EntityCount();

        /// <summary>
        /// Load the FisrtOrDefault entity
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        T LoadEntity(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// Load the FisrtOrDefault entity
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询记录ToList<T>
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>List<T></returns>
        List<T> LoadEntitiesToList(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 分页查询记录
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalIndex"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 分页查询记录ToList
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalIndex"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns>List<T></returns>
        List<T> LoadPageEntitiesToList<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 查询所有记录，为了实现数据导入动态创建类实例并调用该方法用于写入前判断。
        /// </summary>
        /// <returns></returns>
        IQueryable<T> LoadAllEntities();

        /// <summary>
        /// 查询所有记录toList
        /// </summary>
        /// <returns>List<T></returns>
        List<T> LoadAllEntitiesToList();

        //SaveChange
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();


    }
}

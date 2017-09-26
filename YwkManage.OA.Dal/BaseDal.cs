using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.IDal;
using YwkManage.OA.Model;

namespace YwkManage.OA.Dal
{
    public class BaseDal<T> : IBaseDal<T> where T : class, new()
    {
        /// <summary>
        /// 获取数据库连接上下文
        /// </summary>
        public OAContext Db
        {
            get
            {
                return OAContextFactory.GetDbContext();
            }
        }

       /// <summary>
       /// 添加记录
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        public T AddEntity(T entity)
        {
            return Db.Set<T>().Add(entity);
        }
                
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// 返回总记录数
        /// </summary>
        /// <returns></returns>
        public int EntityCount()
        {
            return LoadEntities(p => true).Count();
        }

        /// <summary>
        /// Load the FisrtOrDefault entity
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T LoadEntity(Expression<Func<T, bool>> whereLambda)
        {
            return LoadEntities(whereLambda).FirstOrDefault();

        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
           return  Db.Set<T>().Where(whereLambda);
        }

        /// <summary>
        /// 查询记录ToList<T>
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>List<T></returns>
        public List<T> LoadEntitiesToList(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda).ToList();
        }

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
        public IQueryable<T> LoadPageEntities<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = Db.Set<T>().Where(whereLambda);
            totalIndex = temp.Count();
            if (isAsc)
            {
                return temp.Skip(pageSize * (pageIndex - 1)).Take(pageSize).OrderBy(orderbyLambda);
            }
            else
            {
                return temp.Skip(pageSize * (pageIndex - 1)).Take(pageSize).OrderByDescending(orderbyLambda);
            }
        }

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
        public List<T> LoadPageEntitiesToList<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = Db.Set<T>().Where(whereLambda);
            totalIndex = temp.Count();
            if (isAsc)
            {
                return temp.Skip(pageSize * (pageIndex - 1)).Take(pageSize).OrderBy(orderbyLambda).ToList();
            }
            else
            {
                return temp.Skip(pageSize * (pageIndex - 1)).Take(pageSize).OrderByDescending(orderbyLambda).ToList();
            }
        }

        #region 查询所有记录，为了实现数据导入动态创建类实例并调用该方法用于写入前判断。
        public IQueryable<T> LoadAllEntities()
        {
            return Db.Set<T>().Where(T => true);
        }

        /// <summary>
        /// 查询所有记录toList
        /// </summary>
        /// <returns>List<T></returns>
        public List<T> LoadAllEntitiesToList()
        {
            return Db.Set<T>().Where(T => true).ToList();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
        #endregion  
    }
}

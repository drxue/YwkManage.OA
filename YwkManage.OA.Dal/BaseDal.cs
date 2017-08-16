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
        public OAContext Db
        {
            get
            {
                return OAContextFactory.GetDbContext();
            }
        }

        public T AddEntity(T entity)
        {
            return Db.Set<T>().Add(entity);
        }

        public bool DelelteEntity(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public bool EditEntity(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
           return  Db.Set<T>().Where(whereLambda);

        }

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

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
        //
    }
}

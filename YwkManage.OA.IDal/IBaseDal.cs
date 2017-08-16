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
        OAContext Db { get;  }
        //增删改查
        T AddEntity(T entity);
        bool EditEntity(T entity);
        bool DelelteEntity(T entity);
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);
        //SaveChange
        bool SaveChanges();
    }
}

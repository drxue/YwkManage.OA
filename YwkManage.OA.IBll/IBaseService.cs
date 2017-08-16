using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.IDal;

namespace YwkManage.OA.IBll
{
    /// <summary>
    /// 业务层基接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class, new()
    {
        //Spring.net 使用的IOC注入用属性
        IBaseDal<T> CurrentDal { get; set; }

        //增删改查
        T AddEtnity(T entity);
        bool EditEntity(T entity);
        bool DeleteEntity(T entity);
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);

    }
}

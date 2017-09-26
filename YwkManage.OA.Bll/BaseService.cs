using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YwkManage.OA.IBll;
using YwkManage.OA.IDal;

namespace YwkManage.OA.Bll
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        //设置Spring.net属性
        public abstract IBaseDal<T> CurrentDal { get; set; }
       //增删改查方法
        public T AddEtnity(T entity)
        {
            var temp = CurrentDal.AddEntity(entity);
            CurrentDal.SaveChanges();
            return temp;
        }

        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDal.SaveChanges();
        }

        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDal.SaveChanges();
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            var temp = CurrentDal.LoadEntities(whereLambda);
            CurrentDal.SaveChanges();
            return temp;
        }

        public IQueryable<T> LoadPageEntities<s>(int pageSize, int pageIndex, out int totalIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = CurrentDal.LoadPageEntities(pageSize, pageIndex, out totalIndex, whereLambda, orderbyLambda, isAsc);
            CurrentDal.SaveChanges();
            return temp;
            
        }
    }
}

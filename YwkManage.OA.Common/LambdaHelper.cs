using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Common
{
    /// <summary>
    /// 2017-09-03 DrXue:在原有lambdaHelper基础上，增加了参数Type T的传送，用于动态创建的lambda表达式用于反射动态创建的对象方式时传递方法参数。在反射机制调用时，需要传递Lambda表达式作为参数，而且在调用该类产生Lambda表达式时只知道类型T的Type，例如知道 Type T=typeof（string）中的T，而没有string类型。
    /// </summary>
    public class LambdaHelper
    {
        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return p => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static Expression True(Type T)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");
            return Expression.Lambda(Expression.Constant(true), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return p => false;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static Expression False(Type T)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");
            return Expression.Lambda(Expression.Constant(false), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, propertyName), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression GetOrderExpression(Type T, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");
            return Expression.Lambda(Expression.Property(parameter, propertyName), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }


        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// 参数propertyValue类型修改为object,在方法中判断具体类型。
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static LambdaExpression CreateEqual(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.Equal(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateNotEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <param name="T">泛型类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public static LambdaExpression CreateNotEqual(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.NotEqual(member, constant), parameter);
        }
        
        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThan<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression CreateGreaterThan(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName < propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThan<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName < propertyValue
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression CreateLessThan(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        public static Expression<Func<T, bool>> CreateGreaterThanOrEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression CreateGreaterThanOrEqual(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName <= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThanOrEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName <= propertyValue
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression CreateLessThanOrEqual(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);//创建常数
            return Expression.Lambda(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetContains<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static Expression GetContains(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetNotContains<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <param name="T"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static Expression GetNotContains(Type T, string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(T, "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }
    }
}

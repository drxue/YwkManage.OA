using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ClassView
{
    ///
    ///     进行从 DataTable 或者实现 IDataReader 接口的对象读取记录
    ///     并将结果集转换为给定类型列表的方法。
    ///
    public class ModelConvertor<T> where T : class
    {
        ///
        ///     从 reader 对象中逐行读取记录并将记录转化为 T 类型的集合
        ///
        ///目标类型参数 
        ///实现 IDataReader 接口的对象。 
        ///指定类型的对象集合。 
        public static List<T> ConvertToObject(IDataReader reader)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            Type t = typeof(T);
            Assembly ass = t.Assembly;
            Dictionary<string, PropertyInfo> propertys = ModelConvertor<T>.GetFields(reader);
            PropertyInfo p = null;
            if (reader != null)
            {
                while (reader.Read())
                {
                    obj = ass.CreateInstance(t.FullName) as T;
                    foreach (string key in propertys.Keys)
                    {
                        p = propertys[key];
                        p.SetValue(obj, ModelConvertor<T>.ChangeType(reader[key], p.PropertyType));
                    }
                    list.Add(obj);
                }
            }

            return list;
        }

        ///
        ///     从 DataTale 对象中逐行读取记录并将记录转化为 T 类型的集合
        ///
        ///目标类型参数 
        ///DataTale 对象。 
        ///指定类型的对象集合。 
        public static List<T> ConvertToObject(DataTable table)

        {
            return table == null ? new List<T>() : ModelConvertor<T>.ConvertToObject(table.CreateDataReader() as IDataReader);
        }

        ///
        ///     将数据转化为 type 类型
        ///
        ///要转化的值 
        ///目标类型 
        ///转化为目标类型的 Object 对象 
        private static object ChangeType(object value, Type type)
        {
            if (type.FullName == typeof(string).FullName)
            {
                return Convert.ChangeType(Convert.IsDBNull(value) ? null : value, type);
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter convertor = new NullableConverter(type);
                return Convert.IsDBNull(value) ? null : convertor.ConvertFrom(value);
            }
            return value;
        }

        ///
        ///     获取reader存在并且在 T 类中包含同名可写属性的集合
        ///
        /// 
        ///     可写域的集合
        /// 
        /// 
        ///     以属性名为键，PropertyInfo 为值得字典对象
        /// 
        private static Dictionary<string, PropertyInfo> GetFields(IDataReader reader)
        {
            Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();
            int columnCount = reader.FieldCount;
            Type t = typeof(T);

            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                List<string> readerFields = new List<string>();
                for (int i = 0; i < columnCount; i++)
                {
                    readerFields.Add(reader.GetName(i));
                }
                IEnumerable resList =
                    (from PropertyInfo prop in properties
                     where prop.CanWrite && readerFields.Contains(prop.Name)
                     select prop);
                foreach (PropertyInfo p in resList)
                {
                    result.Add(p.Name, p);
                }
            }
            return result;
        }
    }
}

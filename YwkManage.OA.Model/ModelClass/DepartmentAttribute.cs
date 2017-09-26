using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
   public partial class DepartmentAttribute
    {
        public DepartmentAttribute()
        {
            Departments = new HashSet<Department>();
        }
        public int DepartmentAttributeID { get; set; }
        public string DepartmentAttributeName { get; set; }
        public string Comment { get; set; }
        public bool DeleteFlag { get; set; }
        //导航属性
        public virtual ICollection<Department> Departments { get; set; }
    }
}

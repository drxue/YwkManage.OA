using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
    public partial class Contact
    {
        public int ContactID { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string ShortPhone66 { get; set; }
        public string ShortPhone77 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        ///导航属性
        public virtual Department Department { get; set; }
    }
}

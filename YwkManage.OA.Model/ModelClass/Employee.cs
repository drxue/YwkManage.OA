using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
    public partial class Employee
    {
        public Employee()
        {
            TitleAward = new HashSet<TitleAward>();
            Leave = new HashSet<Leave>();
        }
        //Primary key
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? Age { get; set; }
        public string IDCard { get; set; }
        public DateTime? HireDate { get; set; }
        public string Notes { get; set; }
        public int? Title_TitleID { get; set; }

        //Foreign key
        public int DepartmentID { get; set; }

        #region  Navigation Property 导航属性 
        [JsonIgnore]
        public virtual Department Department { get; set; }

        //职称信息
        [JsonIgnore]
        public virtual ICollection<TitleAward> TitleAward { get; set; }
        //职工外出登记
        [JsonIgnore]
        public virtual ICollection<Leave> Leave { get; set; }
        //医师注册信息
        public virtual DoctorRegister DoctorRegister { get; set; }
        #endregion

    }
}

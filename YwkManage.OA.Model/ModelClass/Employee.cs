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
            WardHeadNurse = new HashSet<Ward>();
            WardDeputyHeadNurse = new HashSet<Ward>();
            WardDirector = new HashSet<Ward>();
            DepartmentDirector = new HashSet<Department>();
            DepartmentDeputyDirector = new HashSet<Department>();
            DepartmentHeadNurse = new HashSet<Department>();
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
        public int? TitleLevelInfoID { get; set; }
        public bool DeleteFlag { get; set; }
        //Foreign key
        public int? DepartmentID { get; set; }
        #region  Navigation Property 导航属性 
        
        //医师注册信息
        public virtual DoctorRegister DoctorRegister { get; set; }
        //public virtual Contact Contact { get; set; }
        public virtual TitleLevelInfo TitleLevelInfo { get; set; }
        public virtual Department Department { get; set; }
        //职称信息
        [JsonIgnore]
        public virtual ICollection<TitleAward> TitleAward { get; set; }
        //职工外出登记
        [JsonIgnore]
        public virtual ICollection<Leave> Leave { get; set; }
        //科室
        [JsonIgnore]
        public virtual ICollection<Department> DepartmentDirector { get; set; }
        [JsonIgnore]
        public virtual ICollection<Department> DepartmentDeputyDirector { get; set; }
        [JsonIgnore]
        public virtual ICollection<Department> DepartmentHeadNurse { get; set; }
        //病区
        [JsonIgnore]
        public virtual ICollection<Ward> WardHeadNurse { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ward> WardDeputyHeadNurse { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ward> WardDirector { get; set; }
        #endregion

    }
}

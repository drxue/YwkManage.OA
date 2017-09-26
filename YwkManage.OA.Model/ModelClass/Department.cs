using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
    /// <summary>
    /// 科室表
    /// </summary>
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
            Ward = new HashSet<Ward>();

        }

        //Primary key
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string HisDepartmentName { get; set; }
        public Nullable<int> BedNumbers { get; set; }
        public Nullable<int> StatisticsBedNumbers { get; set; }
        public Nullable<int> DepartmentAttributeID { get; set; }
        public Nullable<bool> InpatientFlag { get; set; }
        public Nullable<bool> PediatricsFlag { get; set; }
        public Nullable<bool> OutpatientFlag { get; set; }
        public Nullable<bool> TechnicalFlag { get; set; }
        public Nullable<bool> AdministractiveFlag { get; set; }
        public string Comment { get; set; }
        public bool DeleteFlag { get; set; }
        //Foreign key
        public string DepartmentDirectorID { get; set; }
        public string DepartmentDeputyDirectorID { get; set; }
        public string DepartmentHeadNurseID { get; set; }

        //Navigation property
        public virtual Employee DepartmentDirector { get; set; }
        public virtual Employee DepartmentDeputyDirector { get; set; }
        public virtual Employee DepartmentHeadNurse { get; set; }
        public virtual DepartmentAttribute DepartmentAttribute { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee> Employee  { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ward> Ward { get; set; }
    }
}

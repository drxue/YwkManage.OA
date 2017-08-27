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
            //Leave = new HashSet<Leave>();
            //Ward = new HashSet<Ward>();
            
        }
        //Primary key
        public int DepartmenID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<int> BedNumbers { get; set; }
        public Nullable<int> StatisticsBedNumbers { get; set; }
        public int DepartmentAttributeID { get; set; }
        public Nullable<bool> InpatientFlag { get; set; }
        public Nullable<bool> PediatricsFlag { get; set; }
        public Nullable<bool> OutpatientFlag { get; set; }
        public Nullable<bool> TechnicalFlag { get; set; }
        public string Comment { get; set; }

        //Foreign key

        //Navigation property
        [JsonIgnore]
        public virtual ICollection<Employee> Employee  { get; set; }
        [JsonIgnore]
        public virtual Employee DepartmentDirector { get; set; }
        [JsonIgnore]
        public virtual Employee DepartmentDeputyDirector { get; set; }
        [JsonIgnore]
        public virtual Employee HeadNurse { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Leave> Leave { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Ward> Ward { get; set; }
    }
}

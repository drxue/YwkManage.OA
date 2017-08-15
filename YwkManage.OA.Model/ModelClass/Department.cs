using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model
{
    public partial class Department
    {
        public Department()
        {
            Leave = new HashSet<Leave>();
            Ward = new HashSet<Ward>();
        }
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

        public string DepartmentDeputyDirectorID { get; set; }
        public string DepartmentDirectorID { get; set; }
        public string HeadNurseID { get; set; }

        //导航属性
        [JsonIgnore]
        public virtual ICollection<Leave> Leave { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ward> Ward { get; set; }
    }
}

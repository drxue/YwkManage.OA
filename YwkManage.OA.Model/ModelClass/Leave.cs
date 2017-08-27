using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    public partial class Leave
    {
        public int LeaveID { get; set; }
        public string EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public Nullable<int> ProjectClassifyID { get; set; }
        public string ProjectName { get; set; }
        public string Destination { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        #region 导航属性
        //[JsonIgnore]
        //public virtual ProjectClassify ProjectClassify { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        #endregion

    }
}

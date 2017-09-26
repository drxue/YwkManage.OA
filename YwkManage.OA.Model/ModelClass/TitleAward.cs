using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    /// <summary>
    /// 职称信息
    /// </summary>
    public partial class TitleAward
    {
        public int TitleAwardID { get; set; }
        public string EmployeeID { get; set; }
        public int TitleLevelInfoID { get; set; }
        public DateTime? AwardDate { get; set; }
        public string AwardHospital { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool DeleteFlag { get; set; }
        //导航属性
        //[JsonIgnore]
        public virtual TitleLevelInfo TitleLevelInfo { get; set; }
        //[JsonIgnore]
        public virtual Employee Employee { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    public partial class TitleLevelInfo
    {
       /// <summary>
       /// 职称等级信息
       /// </summary>
        public TitleLevelInfo()
        {
            TitleAward = new HashSet<TitleAward>();
            Employees = new HashSet<Employee>();
        }
        //基本属性
        public int TitleLevelInfoID { get; set; }
        public string TitleName { get; set; }
        public int TitleLevel { get; set; }
        public bool DeleteFlag { get; set; }
        //导航属性
        [JsonIgnore]
        public virtual ICollection<TitleAward> TitleAward { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

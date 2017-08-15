using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model
{
    public partial class TitleLevelInfo
    {
        public TitleLevelInfo()
        {
            TitleAward = new HashSet<TitleAward>();
        }
        //基本属性
        public int TitleID { get; set; }
        public string TitleName { get; set; }
        public int TitleLevel { get; set; }
        //导航属性
        [JsonIgnore]
        public virtual ICollection<TitleAward> TitleAward { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model
{
    public partial class ProjectClassify
    {
        public ProjectClassify()
        {
            Leave = new HashSet<Leave>();
        }
        public int ProjectClassifyID { get; set; }
        public string ProjectClassifyName { get; set; }
        public string Description { get; set; }
        //导航属性
        [JsonIgnore]
        public virtual ICollection<Leave> Leave { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model
{
    public class Ward
    {
        //构造方法
        public Ward()
        {
            Department = new HashSet<Department>();
        }
        //基本属性
        public int WardID { get; set; }
        public string WardName { get; set; }
        public string HeadNurseID { get; set; }
        public string DeputyHeadNurseID { get; set; }
        public Nullable<int> BedNumbers { get; set; }
        public Nullable<int> DesignedBedNumbers { get; set; }
        //导航属性
        [JsonIgnore]
        public virtual Employee HeadNurse { get; set; } //一个病区只有一个护士长，必须有
        [JsonIgnore]
        public virtual Employee DeputyHeadNurse { get; set; }
        [JsonIgnore]
        public virtual ICollection<Department> Department { get; set; }
    }
}

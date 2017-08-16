using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
    public partial class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
        public string Sort { get; set; }
        //导航属性

    }
}

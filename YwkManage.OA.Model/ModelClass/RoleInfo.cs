using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model
{
    public partial class RoleInfo
    {
        public int ID { get; set; }

        public string RoleName { get; set; }

        public short DelFlag { get; set; }

        public DateTime SubTime { get; set; }

        public string Remark { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Sort { get; set; }
    }
}

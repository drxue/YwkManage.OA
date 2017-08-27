using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    /// <summary>
    /// 登录用户角色信息
    /// </summary>
    public partial class RoleInfo
    {
        public RoleInfo()
        {
            UserInfo = new HashSet<UserInfo>();
            ActionInfo = new HashSet<ActionInfo>();
        }

        public int RoleInfoID { get; set; }
        public string RoleName { get; set; }
        public short DelFlag { get; set; }
        public DateTime SubTime { get; set; }
        public string Remark { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Sort { get; set; }

        //Navigation Property
        public virtual ICollection<UserInfo> UserInfo { get; set; }
        public virtual ICollection<ActionInfo> ActionInfo { get; set; }
    }
}

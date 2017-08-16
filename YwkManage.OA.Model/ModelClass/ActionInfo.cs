using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model.ModelClass
{
    public partial class ActionInfo
    {
        public ActionInfo()
        {

        }

        public int ID { get; set; }

        public DateTime SubTime { get; set; }

        public short DelFlag { get; set; }

        public string ModifiedOn { get; set; }

        public string Remark { get; set; }

        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public string ActionMethodName { get; set; }

        public string ControllerName { get; set; }

        public string ActionInfoName { get; set; }

        public string Sort { get; set; }

        public short ActionTypeEnum { get; set; }

        public string MenuIcon { get; set; }

        public int IconWidth { get; set; }

        public int IconHeight { get; set; }
    }
}

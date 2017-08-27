﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    public partial class TitleAward
    {
        public int TitleAwardID { get; set; }
        public string EmployeeID { get; set; }
        public int TitleID { get; set; }
        public DateTime? AwardDate { get; set; }
        public string AwardHospital { get; set; }
        public DateTime? UpdateDate { get; set; }
        //导航属性
        //[JsonIgnore]
        //public virtual TitleLevelInfo TitleLevelInfo { get; set; }
        //[JsonIgnore]
        //public virtual Employee Employee { get; set; }
    }
}

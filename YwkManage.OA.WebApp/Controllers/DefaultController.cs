using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YwkManage.OA.IBll;
using YwkManage.OA.Model.ModelClass;

namespace YwkManage.OA.WebApp.Controllers
{
    public class DefaultController : Controller
    {
        public IBaseService<Contact> ContactService { get; set; }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}
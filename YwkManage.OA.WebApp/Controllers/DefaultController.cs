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
        //Spring.net设置bll类实例属性，Spring的IOC注入机制自动生成类实例。同时生成Dal类实例。
        public IBaseService<Contact> ContactService { get; set; }
        // GET: Default
        public ActionResult Index()
        {
            var temp = ContactService.LoadEntities(e => true);
            ViewData.Model = temp;
            return View();
        }
    }
}
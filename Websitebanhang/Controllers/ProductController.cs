using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebanhang.Context;

namespace Websitebanhang.Controllers
{
    public class ProductController : Controller
    {
        WebsitebanhangEntities2 objwebsitebanhangEntities = new WebsitebanhangEntities2();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwebsitebanhangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}
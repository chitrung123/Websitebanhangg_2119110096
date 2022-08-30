using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebanhang.Context;

namespace Websitebanhang.Controllers
{
    public class CategoryController : Controller
    {
      WebsitebanhangEntities2 objwebsitebanhangEntities = new WebsitebanhangEntities2();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objwebsitebanhangEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objwebsitebanhangEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}
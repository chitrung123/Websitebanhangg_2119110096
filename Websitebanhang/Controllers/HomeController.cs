using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Websitebanhang.Context;
using Websitebanhang.Models;

namespace Websitebanhang.Controllers
{
    public class HomeController : Controller
    {
      WebsitebanhangEntities2 objwebsitebanhangEntities = new WebsitebanhangEntities2();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objwebsitebanhangEntities.Categories.Where(n => n.ShowOnHomePage == true).ToList();
            objHomeModel.ListProduct = objwebsitebanhangEntities.Products.Where(n => n.ShowOnHomePage == true).ToList();
            return View(objHomeModel);
        }
        public ActionResult Account()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objwebsitebanhangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objwebsitebanhangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objwebsitebanhangEntities.Users.Add(_user);
                    objwebsitebanhangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }


            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (email.Length == 0)
                    ViewBag.error1 = "Vui lòng nhập Email!";
                if (password.Length == 0)
                    ViewBag.error2 = "Vui lòng nhập Password!";
                else
                {
                    var f_password = GetMD5(password);
                    var data = objwebsitebanhangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                    if (data.Count() > 0)
                    {
                        //add session
                        Session["FullName"] = data.FirstOrDefault().LastName + " " + data.FirstOrDefault().FirstName;
                        Session["Email"] = data.FirstOrDefault().Email;
                        Session["idUser"] = data.FirstOrDefault().Id;
                        Session["Password"] = data.FirstOrDefault().Password;
                        Session["IsAdmin"] = data.FirstOrDefault().IsAdmin;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Sai thông tin đăng nhập!";
                        return View();
                    }
                }
                return View();
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        public ActionResult SendEmail()
        {
            return View();
        }

       
            
    }
}
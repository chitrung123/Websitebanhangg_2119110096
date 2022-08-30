using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebanhang.Context;
using Websitebanhang.Models;

namespace Websitebanhang.Controllers
{
    public class PaymentController : Controller
    {
        WebsitebanhangEntities2 objwebsitebanhangEntities = new WebsitebanhangEntities2();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");

            }
            else
            {
                //lay t.tin gio hang từ biến sesion
                var lstCart = (List<CartModel>)Session["cart"];
                // gán dl cho Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 2;
                objwebsitebanhangEntities.Orders.Add(objOrder);
                //luu t.tin dl vào bảng Order
                objwebsitebanhangEntities.SaveChanges();
                // lấy oder id vừa mới tạo ra để lưu vào bảng OrderDetail
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebsitebanhangEntities.OrderDetails.AddRange(lstOrderDetail);
                objwebsitebanhangEntities.SaveChanges();
            }
            return View();
        }
    }
}
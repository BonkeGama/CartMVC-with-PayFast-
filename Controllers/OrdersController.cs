
using CartMvc.Models;
using CartMvc.Shop.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CartMvc.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        private ApplicationDbContext db = new ApplicationDbContext();

        Order_Business ob = new Order_Business();

        //Customer orders
        public ActionResult Customer_Orders(string id)
        {
            if (String.IsNullOrEmpty(id) || id == "all")
            {
                ViewBag.Status = "All";
                return View(ob.cust_all());
            }
            else
            {
                ViewBag.Status = id;
                return View(ob.cust_find_by_status(id));
            }
        }
        public ActionResult New_Orders(string id)
        {
            if (String.IsNullOrEmpty(id) || id == "all")
            {
                ViewBag.Status = "All";
                return View(ob.cust_all());
            }
            else
            {
                ViewBag.Status = id;
                return View(ob.cust_find_by_status(id));
            }
        }
        public ActionResult Order_Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ob.cust_find_by_id(id) != null)
            {
                ViewBag.Order_Items = ob.cust_Order_items(id);
                ViewBag.Address = db.Order_Addresses.ToList().Find(x => x.Order_ID == id);
                ViewBag.Total = get_order_total((int)id);

                return View(ob.cust_find_by_id(id));
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Order_Tracking(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ob.cust_find_by_id(id) != null)
            {
                ViewBag.Order = ob.cust_find_by_id(id);
                return View(ob.get_tracking_report(id));
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }

        public ActionResult Mark_As_Packed(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ob.cust_find_by_id(id) != null)
            {
                ob.mark_as_packed(id);
                return RedirectToAction("Order_Details", new { id = id });
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public double get_order_total(int id)
        {
            double amount = 0;
            foreach (var item in db.Order_Items.ToList().FindAll(match: x => x.Order_id == id))
            {
                amount += (item.price * item.quantity);
            }
            return amount;
        }

        //account orders
        public ActionResult Order_History()
        {
            return View(ob.cust_all().Where(x => x.Patient.UserName == User.Identity.Name));
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CartMvc.Models;
using CartMvc.Shop.Logic;

namespace CartMvc.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Item_Business ib = new Item_Business();
        Category_Business cb = new Category_Business();


        public string shoppingCartID { get; set; }

        public const string CartSessionKey = "CartId";

        public ItemsController() { }

        public ActionResult Index()
        {
            return View(ib.all());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ib.find_by_id(id) != null)
                return View(ib.find_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(cb.all(), "Category_ID", "Category_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles ="Admin")]
        public ActionResult Create(Item model, HttpPostedFileBase img_upload)
        {
            ViewBag.Category_ID = new SelectList(cb.all(), "Category_ID", "Category_Name");
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            model.Picture = data;
            //model.QuantityInStock = 0;
            if (ModelState.IsValid)
            {
                ib.add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Category_ID = new SelectList(cb.all(), "Category_ID", "Category_Name");
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ib.find_by_id(id) != null)
                return View(ib.find_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item model, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            model.Picture = data;
            if (ModelState.IsValid)
            {
                ib.edit(model);
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(cb.all(), "Category_ID", "Category_Name");
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ib.find_by_id(id) != null)
                return View(ib.find_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ib.delete(ib.find_by_id(id));
            return RedirectToAction("Index");
        }


        public ActionResult Fall_catalog()
        {
            return View(ib.all());
        }

        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp_cart_ID = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[CartSessionKey] = temp_cart_ID.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        }
    }
}
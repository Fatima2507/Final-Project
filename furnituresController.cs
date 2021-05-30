using Stationary_management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stationary_management.Controllers
{
    public class furnituresController : Controller
    {
        stationary_managementEntities db = new stationary_managementEntities();

        // GET: furnitures
        public ActionResult Furniture()
        {
            var data = db.furnitures.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(furniture e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.furnitures.Add(e);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var Emprow = db.furnitures.Where(x => x.id == id).FirstOrDefault();
            Session["Image"] = Emprow.image;

            return View(Emprow);
        }
        [HttpPost]
        public ActionResult Edit(furniture e)
        {

            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Delete(int id)
        {
                var Employeeraw = db.furnitures.Where(x => x.id == id).FirstOrDefault();
                db.Entry(Employeeraw).State = EntityState.Deleted;
                db.SaveChanges();
                string ImagePath = Request.MapPath(Employeeraw.image.ToString());
                System.IO.File.Delete(ImagePath);
                return RedirectToAction("Furniture", "furnitures");
        }


        public ActionResult Details(int id)
        {
            var Emprow = db.furnitures.Where(x => x.id == id).FirstOrDefault();
            
            return View(Emprow);
        }
    }
}
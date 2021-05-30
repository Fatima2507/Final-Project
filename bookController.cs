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
    public class bookController : Controller
    {
        stationary_managementEntities db = new stationary_managementEntities();

        // GET: book
        public ActionResult Book()
        {
            var data = db.Books.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
   

        [HttpPost]
        public ActionResult Create(Book e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Books.Add(e);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Edit(int id)
        {
            Book Emprow = db.Books.Where(x => x.id ==id).FirstOrDefault();
            Session["Image"] = Emprow.image;

            return View(Emprow);
        }
        [HttpPost]
        public ActionResult Edit(Book e)
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
            var Employeeraw = db.Books.Where(x => x.id == id).FirstOrDefault();
            db.Entry(Employeeraw).State = EntityState.Deleted;
            db.SaveChanges();
            string ImagePath = Request.MapPath(Employeeraw.image.ToString());
            System.IO.File.Delete(ImagePath);
            return RedirectToAction("Book", "book");
        }


        public ActionResult Details(int id)
        {
            var Emprow = db.Books.Where(x => x.id == id).FirstOrDefault();
            
            return View(Emprow);
        }
    }
}
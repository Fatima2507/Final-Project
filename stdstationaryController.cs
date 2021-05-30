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
    public class stdstationaryController : Controller
    {

        stationary_managementEntities db = new stationary_managementEntities();

        // GET: stdstationary
        public ActionResult Student()
        {
            var data = db.Std_Stationary.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Std_Stationary e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.S_image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Std_Stationary.Add(e);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var Emprow = db.Std_Stationary.Where(x => x.S_id == id).FirstOrDefault();
            Session["Image"] = Emprow.S_image;

            return View(Emprow);
        }
        [HttpPost]
        public ActionResult Edit(Std_Stationary e)
        {

            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.S_image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var Employeeraw = db.Std_Stationary.Where(x => x.S_id == id).FirstOrDefault();
            db.Entry(Employeeraw).State = EntityState.Deleted;
            db.SaveChanges();
            string ImagePath = Request.MapPath(Employeeraw.S_image.ToString());
            System.IO.File.Delete(ImagePath);
            return RedirectToAction("Student", "stdstationary");
        }


        public ActionResult Details(int id)
        {
            var Emprow = db.Std_Stationary.Where(x => x.S_id == id).FirstOrDefault();
            
            return View(Emprow);
        }
    }
}
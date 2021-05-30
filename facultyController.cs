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
    public class facultyController : Controller
    {
        stationary_managementEntities db = new stationary_managementEntities();
        // GET: faculty
        public ActionResult Faculty()
        {
            var data = db.Faculty_Stationary.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Faculty_Stationary e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.T_image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Faculty_Stationary.Add(e);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var Emprow = db.Faculty_Stationary.Where(x => x.T_id == id).FirstOrDefault();
            Session["Image"] = Emprow.T_image;

            return View(Emprow);
        }

        [HttpPost]
        public ActionResult Edit(Faculty_Stationary e)
        {

            string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
            string extension = Path.GetExtension(e.ImageFile.FileName);
            fileName = fileName + extension;
            e.T_image = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            e.ImageFile.SaveAs(fileName);
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var Employeeraw = db.Faculty_Stationary.Where(x => x.T_id == id).FirstOrDefault();
            db.Entry(Employeeraw).State = EntityState.Deleted;
            db.SaveChanges();
            string ImagePath = Request.MapPath(Employeeraw.T_image.ToString());
            System.IO.File.Delete(ImagePath);
            return RedirectToAction("Faculty", "faculty");
        }


        public ActionResult Details(int id)
        {
            var Emprow = db.Faculty_Stationary.Where(x => x.T_id == id).FirstOrDefault();
   
            return View(Emprow);
        }
    }
}
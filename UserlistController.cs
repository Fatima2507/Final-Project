using Stationary_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stationary_management.Controllers
{
    public class UserlistController : Controller
    {

        stationary_managementEntities db = new stationary_managementEntities();
        // GET: Userlist

        public ActionResult UserList()
        {
            var user = db.emp_roles.ToList();
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Add(emp_roles s)
        {
            db.emp_roles.Add(s);
            db.SaveChanges();
            return RedirectToAction("UserList","Userlist");
        }

        public ActionResult Edit(int id)
        {
            emp_roles r = db.emp_roles.Where(x => x.emp_no == id).FirstOrDefault();
            return View(r);
        }
        public ActionResult Save(emp_roles s)
        {
            emp_roles r = db.emp_roles.Where(x => x.emp_no == s.emp_no).FirstOrDefault();
            r.emp_name = s.emp_name;
            r.roles = s.roles;
            r.reg_no = s.reg_no;
            db.SaveChanges();
            return RedirectToAction("UserList","Userlist");
        }
        public ActionResult Details(int id)
        {
            emp_roles r = db.emp_roles.Where(x => x.emp_no == id).FirstOrDefault();
            return View(r);
        }
        public ActionResult Delete(int id)
        {
            emp_roles r = db.emp_roles.Where(x => x.emp_no == id).FirstOrDefault();
            return View(r);
        }
        public ActionResult Remove(emp_roles s)
        {
            emp_roles r = db.emp_roles.Where(x => x.emp_no == s.emp_no).FirstOrDefault();
            db.emp_roles.Remove(r);
            db.SaveChanges();
            return RedirectToAction("UserList","Userlist");
        }
    }
}
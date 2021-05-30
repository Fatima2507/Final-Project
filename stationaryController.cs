using Stationary_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Stationary_management.Controllers
{
    public class stationaryController : Controller
    {
        stationary_managementEntities db = new stationary_managementEntities();
        // GET: stationary
       
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(user_reg reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.user_reg.Add(reg);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
            return View();
        }
       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user_reg reg,string ReturnUrl)
        {
            try
            {
                var details = db.user_reg.Where(x => x.fname == reg.fname && x.pass == reg.pass).FirstOrDefault();
                if (details != null)
                {
                    FormsAuthentication.SetAuthCookie(reg.fname, false);
                    Session["fname"] = reg.fname.ToString();
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user or password");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["fname"] = null;
            return RedirectToAction("Index","Home");
        }

       
    }
}
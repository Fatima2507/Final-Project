using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationary_management.Models;

namespace Stationary_management.Controllers
{
    public class RequestController : Controller
    {
        stationary_managementEntities db = new stationary_managementEntities();
        // GET: Request

        public ActionResult ProductRequest()
        {
            var req = db.stationary_requests.ToList();
            return View(req);
        }

        public ActionResult NewRequest()
        {

            return View();
        }
        public ActionResult Add(stationary_requests r)
        {
            db.stationary_requests.Add(r);
            db.SaveChanges();
            return RedirectToAction("ProductRequest","Request");
        }
     
        public ActionResult Reject(int id)
        {
            stationary_requests model = db.stationary_requests.Where(x => x.req_id == id).FirstOrDefault();
            db.stationary_requests.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ProductRequest", "Request");
        }
       
    }
}
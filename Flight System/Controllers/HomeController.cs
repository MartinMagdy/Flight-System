using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flight_System.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Search()
        //{

        //    return View();
        //}
        public ActionResult Search(string searchname)
        {
            Flight_SystemEntities db =new Flight_SystemEntities();

            var Result = db.Employees.Where(model => model.firstname.Contains(searchname)
              || model.secondname.Contains(searchname) || model.ssn.Contains(searchname)
              || model.supervisor.Contains(searchname) || model.position.Contains(searchname));
            return View("Search",Result.ToList());
        }
        public ActionResult Details(int Id)
        {
            Flight_SystemEntities db = new Flight_SystemEntities();

            var Emp = db.Employees.Find(Id);
            if (Emp == null)
            {
                return HttpNotFound();
            }
            Session["Id"] = Id;
            return View(Emp);
        }
    }
}
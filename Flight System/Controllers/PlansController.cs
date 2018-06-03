using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flight_System.Models;

namespace Flight_System.Controllers
{
    public class PlansController : Controller
    {
        private Flight_SystemEntities db = new Flight_SystemEntities();

        // GET: Plans
        [Authorize]
        public ActionResult Index()
        {
            var plans = db.Plans.Include(p => p.Companies).Include(p => p.Employees);
            return View(plans.ToList());
        }

        // GET: Plans/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            return View(plans);
        }

        // GET: Plans/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.companynumber = new SelectList(db.Companies, "companynumber", "name");
            ViewBag.ssn_employee = new SelectList(db.Employees, "ssn","ssn");
            ViewBag.ssn_employee_first = new SelectList(db.Employees, "ssn", "firstname");
            ViewBag.ssn_employee_last = new SelectList(db.Employees, "ssn", "secondname");
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ssn_employee,firstname,lastname,planId,chair,companynumber")] Plans plans)
        {
            if (ModelState.IsValid)
            {
                db.Plans.Add(plans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.companynumber = new SelectList(db.Companies, "companynumber", "name", plans.companynumber);
            ViewBag.ssn_employee = new SelectList(db.Employees, "ssn",  plans.ssn_employee);
            ViewBag.ssn_employee_first = new SelectList(db.Employees, "ssn", "firstname",plans.firstname);
            ViewBag.ssn_employee_last = new SelectList(db.Employees, "ssn", "secondname",plans.lastname);
            return View(plans);
        }

        // GET: Plans/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            ViewBag.companynumber = new SelectList(db.Companies, "companynumber", "name", plans.companynumber);
            ViewBag.ssn_employee = new SelectList(db.Employees, "ssn", "ssn", plans.ssn_employee);
            return View(plans);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ssn_employee,firstname,lastname,position,planId,chair,companynumber")] Plans plans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companynumber = new SelectList(db.Companies, "companynumber", "name", plans.companynumber);
            ViewBag.ssn_employee = new SelectList(db.Employees, "ssn", "firstname", plans.ssn_employee);
            return View(plans);
        }

        // GET: Plans/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            return View(plans);
        }

        // POST: Plans/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plans plans = db.Plans.Find(id);
            db.Plans.Remove(plans);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

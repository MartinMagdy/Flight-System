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
    [Authorize]
    public class Employees_PhoneController : Controller
    {
        private Flight_SystemEntities db = new Flight_SystemEntities();
        [Authorize]
        // GET: Employees_Phone
        public ActionResult Index()
        {
            var employees_Phone = db.Employees_Phone.Include(e => e.Employees);
            return View(employees_Phone.ToList());
        }
        [Authorize]
        // GET: Employees_Phone/Details/5
        public ActionResult Details(string id,int? phone)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (phone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Phone employees_Phone = db.Employees_Phone.Find(id,phone);
            if (employees_Phone == null)
            {
                return HttpNotFound();
            }
            return View(employees_Phone);
        }
        [Authorize(Roles = "Admin")]
        // GET: Employees_Phone/Create
        public ActionResult Create()
        {
            ViewBag.ssn = new SelectList(db.Employees, "ssn", "firstname");
            return View();
        }

        // POST: Employees_Phone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ssn,Phone")] Employees_Phone employees_Phone)
        {
            if (ModelState.IsValid)
            {
                db.Employees_Phone.Add(employees_Phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ssn = new SelectList(db.Employees, "ssn", "firstname", employees_Phone.ssn);
            return View(employees_Phone);
        }
        [Authorize(Roles = "Admin")]
        // GET: Employees_Phone/Edit/5
        public ActionResult Edit(string id,int? Phone)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Phone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Phone employees_Phone = db.Employees_Phone.Find(id,Phone);
            if (employees_Phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ssn = new SelectList(db.Employees, "ssn", "firstname", employees_Phone.ssn);
            return View(employees_Phone);
        }

        // POST: Employees_Phone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ssn,Phone")] Employees_Phone employees_Phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees_Phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ssn = new SelectList(db.Employees, "ssn", "firstname", employees_Phone.ssn);
            return View(employees_Phone);
        }
        [Authorize(Roles = "Admin")]
        // GET: Employees_Phone/Delete/5
        public ActionResult Delete(string id,int? phone)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (phone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Phone employees_Phone = db.Employees_Phone.Find(id,phone);
            if (employees_Phone == null)
            {
                return HttpNotFound();
            }
            return View(employees_Phone);
        }

        // POST: Employees_Phone/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id,int? phone)
        {
            Employees_Phone employees_Phone = db.Employees_Phone.Find(id,phone);
            db.Employees_Phone.Remove(employees_Phone);
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

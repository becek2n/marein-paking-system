﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parking.Repository.Models;

namespace Parking.UI.Controllers
{
    public class TransportationController : Controller
    {
        private ParkingContext db = new ParkingContext();

        // GET: Transportation
        public ActionResult Index()
        {
            return View(db.Transportations.ToList());
        }

        // GET: Transportation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // GET: Transportation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transportation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Transportations.Add(transportation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportation);
        }

        // GET: Transportation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Transportation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportation);
        }

        // GET: Transportation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Transportation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportation transportation = db.Transportations.Find(id);
            db.Transportations.Remove(transportation);
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

using System;
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
    public class ParkingController : Controller
    {
        private ParkingContext db = new ParkingContext();

        // GET: Parking
        public ActionResult Index()
        {
            return View(db.Parkings.ToList());
        }

        // GET: Parking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingArea parkingArea = db.Parkings.Find(id);
            if (parkingArea == null)
            {
                return HttpNotFound();
            }
            return View(parkingArea);
        }

        // GET: Parking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketCode,TransportationTypeId,PlateNumber,CheckIn,CheckOut,Duration,Amount")] ParkingArea parkingArea)
        {
            if (ModelState.IsValid)
            {
                db.Parkings.Add(parkingArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkingArea);
        }

        // GET: Parking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingArea parkingArea = db.Parkings.Find(id);
            if (parkingArea == null)
            {
                return HttpNotFound();
            }
            return View(parkingArea);
        }

        // POST: Parking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketCode,TransportationTypeId,PlateNumber,CheckIn,CheckOut,Duration,Amount")] ParkingArea parkingArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkingArea);
        }

        // GET: Parking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingArea parkingArea = db.Parkings.Find(id);
            if (parkingArea == null)
            {
                return HttpNotFound();
            }
            return View(parkingArea);
        }

        // POST: Parking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingArea parkingArea = db.Parkings.Find(id);
            db.Parkings.Remove(parkingArea);
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

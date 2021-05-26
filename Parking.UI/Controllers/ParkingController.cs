using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parking.Repository.Models;
using System.Linq.Dynamic;
using Parking.Interfaces;
using Parking.DTO;

namespace Parking.UI.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IParking _parking;
        private readonly ITransportation _transportation;
        private readonly ParkingContext db = new ParkingContext();
        public ParkingController(IParking parking, ITransportation transportation) {
            _parking = parking;
            _transportation = transportation;
        }
        // GET: Parking
        public ActionResult Index() {
            var transportations = _transportation.GetAll().ResponseData;
            return View(transportations);
        }

        public ActionResult GetAll(JqueryDatatableRequestDTO param)
        {
            var pageIndex = (param.Start / param.Length) + 1;
            var pageSize = param.Length;

            string search = this.Request.QueryString["search[value]"] ?? string.Empty;

            var data = _parking.GetAll(pageIndex, pageSize, search);

            return Json(new
            {
                param.Draw,
                iTotalRecords = data.ResponseData.RowCount,
                iTotalDisplayRecords = data.ResponseData.RowCount,
                aaData = data.ResponseData.Results
            }, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult GetCode() {
            var data = _parking.GetCode();
            
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Parking/Details/5
        public ActionResult Details(int id)
        {
            var data = _parking.GetId(id);

            if (data.ResponseData == null)
            {
                return HttpNotFound();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: Parking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkingDTO parkingDTO)
        {
            if (parkingDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = _parking.Add(parkingDTO);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Parking/Edit/5
        public ActionResult Edit(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = _parking.GetId(id);

            if (data.ResponseData == null)
            {
                return HttpNotFound();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: Parking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParkingDTO parkingDTO)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var result = _parking.Edit(id, parkingDTO);
                return RedirectToAction("Index");
            }
            return View();
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
            if (string.IsNullOrWhiteSpace(id.ToString())) { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = _parking.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
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

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
using Parking.UI.Extentions;

namespace Parking.UI.Controllers
{
    public class ParkingController : Controller
    {
        private ParkingContext db = new ParkingContext();

        // GET: Parking
        public ActionResult Index() {
            var transportations = db.Transportations.ToList();
            return View(transportations);
        }

        public ActionResult GetAll(JqueryDatatableParam param)
        {
            var pageIndex = (param.Start / param.Length) + 1;
            var pageSize = param.Length;

            string search = this.Request.QueryString["search[value]"] ?? string.Empty;
            var data = db.Parkings
                .Where(x => x.Transportation.Name.ToLower().Contains(search))
                .GetPage(pageIndex, pageSize);


            return Json(new
            {
                param.Draw,
                iTotalRecords = data.PageCount,
                iTotalDisplayRecords = data.RowCount,
                aaData = data.Results
            }, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult GetCode() {
            //get date first
            var xxx = DateTime.Now.ToString("yyyyMMdd");
            var data = db.Parkings
                .Where(x => x.TicketCode.Substring(3, 8) == xxx)
                .Select(x => x.TicketCode.Substring(11, 3))
                .Cast<int?>()
                .Max();

            var maxCode = (data == null ? 1 : data + 1);
            //set attribut
            var transactionCode = "TRX" + DateTime.Now.ToString("yyyyMMdd") + maxCode?.ToString("000");


            return Json(new { UniqueCode = transactionCode }, JsonRequestBehavior.AllowGet);
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

namespace Parking.UI.Extentions {
    public class JqueryDatatableParam
    {
        public string Echo { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
        public int Columns { get; set; }
        public int Draw { get; set; }
    }
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
    public static class GetPaged
    {
        public static PagedResult<T> GetPage<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.OrderBy("Id").Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}

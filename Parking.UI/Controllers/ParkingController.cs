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
using Newtonsoft.Json;
using Rotativa;

namespace Parking.UI.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IParking _parking;
        private readonly ITransportation _transportation;
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

            string search = Request["search[value]"];

            var data = _parking.GetAll(pageIndex, pageSize, search);

            return new JsonNetResult() { 
                Data = new {
                    param.Draw,
                    iTotalRecords = data.ResponseData.RowCount,
                    iTotalDisplayRecords = data.ResponseData.RowCount,
                    aaData = data.ResponseData.Results
                } 
            };

        }
        
        public ActionResult GetCode() {
            var data = _parking.GetCode();

            return new JsonNetResult() { Data = data };

        }

        // GET: Parking/Details/5
        public ActionResult Details(int id)
        {
            var data = _parking.GetId(id);

            //return Json(data, JsonRequestBehavior.AllowGet);
            return new JsonNetResult() { Data = data };
        }

        // POST: Parking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkingRequestDTO parkingDTO)
        {
            if (parkingDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = _parking.Add(parkingDTO);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Parking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParkingRequestDTO parkingDTO)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()) || parkingDTO == null) {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Bad Request", JsonRequestBehavior.AllowGet);
            }
            if (parkingDTO != null)
            {
                var result = _parking.Edit(id, parkingDTO);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        // POST: Parking/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) {
                Response.StatusCode = 400;
                return Json("Bad Request", JsonRequestBehavior.AllowGet);
            }
            var result = _parking.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print() {
            var data = _parking.Report().ResponseData;

            return new PartialViewAsPdf("_Report", data)
            {
                FileName = "Parking Report.pdf"
            };
        }

    }

    //convert primitive value 
    public class JsonNetResult : JsonResult
    {
        public object Data { get; set; }

        public JsonNetResult()
        {
        }
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }

}

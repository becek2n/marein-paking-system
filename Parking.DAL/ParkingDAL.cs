﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Parking.DAL.DTO;
using Parking.Helper;
using Parking.Interfaces;
using Parking.Repository.DTO;
using Parking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Parking.DAL
{
    public class ParkingDAL : IParking
    {
        private readonly ParkingContext _context;
        private readonly IMapper _mapper;

        public ParkingDAL(ParkingContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public ResultModel<object> Add(ParkingDTO parkingDTO)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                using (var transaction = _context.Database.BeginTransaction()) {
                    try
                    {
                        var model = _mapper.Map<ParkingDTO, ParkingArea>(parkingDTO);

                        _context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        _context.SaveChanges();

                        transaction.Commit();
                        result.SetSuccess("success");
                    }
                    catch (Exception ex)
                    {
                        result.SetFailed(ex.Message);
                        Logging.WriteErr(ex);
                        transaction.Rollback();
                    }
                }

            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
                Logging.WriteErr(ex);
            }
            return result;
        }

        public ResultModel<object> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<object> Edit(int id, ParkingDTO parkingDTO)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = _context.Transportations.Where(x => x.Id == id).FirstOrDefault();
                        
                        if (data == null) throw new Exception("Not found!");

                        var model = _mapper.Map(parkingDTO, data);

                        _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        _context.SaveChanges();

                        transaction.Commit();
                        result.SetSuccess("success");
                    }
                    catch (Exception ex)
                    {
                        result.SetFailed(ex.Message);
                        Logging.WriteErr(ex);
                        transaction.Rollback();
                    }
                }

            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
                Logging.WriteErr(ex);
            }
            return result;
        }

        public ResultModel<PagedResult<ParkingDTO>> GetAll(int pageIndex = 1, int pageSize = 10, string search = "")
        {
            ResultModel<PagedResult<ParkingDTO>> result = new ResultModel<PagedResult<ParkingDTO>>();
            try
            {
                var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

                var data = _context.Parkings
                    .Where(x => x.Transportation.Name.ToLower().Contains(search))
                    .ProjectTo<ParkingDTO>(mapperConfig)
                    .GetPage(pageIndex, pageSize);

                result.SetSuccess("success", data);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public ResultModel<string> GetCode()
        {
            ResultModel<string> result = new ResultModel<string>();

            try
            {
                //get date first
                var dateFormat = DateTime.Now.ToString("yyyyMMdd");
                var data = _context.Parkings
                    .Where(x => x.TicketCode.Substring(3, 8) == dateFormat)
                    .Select(x => x.TicketCode.Substring(11, 3))
                    .Cast<int?>()
                    .Max();

                var maxCode = (data == null ? 1 : data + 1);

                //set attribut
                var transactionCode = "TRX" + DateTime.Now.ToString("yyyyMMdd") + maxCode?.ToString("000");

                result.SetSuccess("success", transactionCode);
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
                Logging.WriteErr(ex);
            }

            return result;
        }

        public ResultModel<List<ParkingDTO>> Report()
        {
            throw new NotImplementedException();
        }
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Parking.DAL.DTO;
using Parking.Helper;
using Parking.Interfaces;
using Parking.DTO;
using Parking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace Parking.DAL
{
    public class ParkingDAL : IParking
    {
        private readonly ParkingContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        public ParkingDAL(ParkingContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public ResultModel<object> Add(ParkingRequestDTO parkingDTO)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                //validate
                if (parkingDTO.CheckOut != null) {
                    if (parkingDTO.CheckOut < parkingDTO.CheckIn) {
                        throw new Exception("Check Out not less than Check In!");
                    }
                }

                using (var transaction = _context.Database.BeginTransaction()) {
                    try
                    {
                        var model = _mapper.Map<ParkingRequestDTO, ParkingArea>(parkingDTO);

                        _context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        _context.SaveChanges();

                        transaction.Commit();
                        result.SetSuccess("success");
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        List<string> errors = new List<string>();
                        foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                        {
                            foreach (DbValidationError error in entityErr.ValidationErrors)
                            {
                                errors.Add(string.Format("Error Message: {0}", error.ErrorMessage));
                            }
                        }

                        result.SetFailed(String.Join(", ", errors.ToArray()));
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
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = _context.Parkings.Where(x => x.Id == id).FirstOrDefault();

                        if (data == null) throw new Exception("Not found!");

                        _context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
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

        public ResultModel<object> Edit(int id, ParkingRequestDTO parkingDTO)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var data = _context.Parkings.Where(x => x.Id == id).FirstOrDefault();
                        
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
                var data = _context.Parkings
                    .Where(x => x.Transportation.Name.ToLower().Contains(search) || 
                        x.PlateNumberFirst.ToLower().Contains(search) ||
                        x.PlateNumberMiddle.ToLower().Contains(search) ||
                        x.PlateNumberLast.ToLower().Contains(search)
                    )
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

        public ResultModel<ParkingDTO> GetId(int id)
        {
            ResultModel<ParkingDTO> result = new ResultModel<ParkingDTO>();
            try
            {

                var data = _context.Parkings
                    .Where(x => x.Id == id)
                    .ProjectTo<ParkingDTO>(mapperConfig)
                    .FirstOrDefault();

                result.SetSuccess("success", data);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public ResultModel<List<ParkingDTO>> Report()
        {
            throw new NotImplementedException();
        }
    }
}

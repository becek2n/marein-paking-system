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

namespace Parking.DAL
{
    public class TransportationDAL : ITransportation
    {
        private readonly ParkingContext _context;
        
        public TransportationDAL(ParkingContext context, IMapper mapper)
        {
            _context = context;
        }

        public ResultModel<List<TransportationDTO>> GetAll()
        {
            ResultModel<List<TransportationDTO>> result = new ResultModel<List<TransportationDTO>>();
            try
            {
                var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
                
                var data = _context.Transportations
                    .ProjectTo<TransportationDTO>(mapperConfig)
                    .ToList();

                result.SetSuccess("success", data);
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
                Logging.WriteErr(ex);
            }

            return result;
        }
    }
}

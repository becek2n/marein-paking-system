using AutoMapper;
using Parking.DTO;
using Parking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ParkingArea, ParkingDTO>();
            CreateMap<ParkingDTO, ParkingArea>();
            CreateMap<Transportation, TransportationDTO>();
        }
    }
}

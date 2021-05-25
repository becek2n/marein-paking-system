using Parking.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Interfaces
{
    public interface IParking
    {
        void Add(ParkingDTO parkingDTO);
    }
}

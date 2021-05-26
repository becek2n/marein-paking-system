using Parking.DAL.DTO;
using Parking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface ITransportation
    {
        ResultModel<List<TransportationDTO>> GetAll();
        
    }
}

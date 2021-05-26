using Parking.DAL.DTO;
using Parking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IParking
    {
        ResultModel<object> Add(ParkingRequestDTO parkingDTO);
        ResultModel<object> Edit(int id, ParkingRequestDTO parkingDTO);
        ResultModel<object> Delete(int id);
        ResultModel<PagedResult<ParkingDTO>> GetAll(int pageIndex = 1, int pageSize = 10, string search = "");
        ResultModel<ParkingDTO> GetId(int id);
        ResultModel<string> GetCode();
        ResultModel<List<ParkingDTO>> Report();

    }
}

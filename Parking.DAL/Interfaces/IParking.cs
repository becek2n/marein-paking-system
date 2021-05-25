using Parking.DAL.DTO;
using Parking.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IParking
    {
        ResultModel<object> Add(ParkingDTO parkingDTO);
        ResultModel<object> Edit(int id, ParkingDTO parkingDTO);
        ResultModel<object> Delete(int id);
        ResultModel<PagedResult<ParkingDTO>> GetAll(int pageIndex = 1, int pageSize = 10, string search = "");
        ResultModel<string> GetCode();
        ResultModel<List<ParkingDTO>> Report();

    }
}

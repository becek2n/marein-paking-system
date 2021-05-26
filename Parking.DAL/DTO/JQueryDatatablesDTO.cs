using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.DTO
{
    public class JqueryDatatableRequestDTO
    {
        public string Echo { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
        public int Columns { get; set; }
        public int Draw { get; set; }
    }
}

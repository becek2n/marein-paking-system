using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Models
{
    public class ParkingArea
    {
        public int Id { get; set; }
        [Required]
        public string TicketCode { get; set; }
        [Required]
        public int TransportationId { get; set; }
        [Required]
        public string PlateNumberFirst { get; set; }
        [Required]
        public string PlateNumberMiddle { get; set; }
        [Required]
        public string PlateNumberLast { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public TimeSpan? Duration { get; set; }
        public double Amount { get; set; }

        public virtual Transportation Transportation { get; set; }
    }
}

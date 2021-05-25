﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Models
{
    public class ParkingArea
    {
        public int Id { get; set; }
        public string TicketCode { get; set; }
        public int TransportationId { get; set; }
        public string PlateNumberFirst { get; set; }
        public string PlateNumberMiddle { get; set; }
        public string PlateNumberLast { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public TimeSpan? Duration { get; set; }
        public double Amount { get; set; }

        public virtual Transportation Transportation { get; set; }
    }
}

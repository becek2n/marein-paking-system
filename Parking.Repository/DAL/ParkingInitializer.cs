using Parking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.DAL
{
    public class ParkingInitializer : System.Data.Entity.DropCreateDatabaseAlways<ParkingContext>
    {
        protected override void Seed(ParkingContext context)
        {
            //master transportation type
            var transportations = new List<Transportation>
            {
                new Transportation(){ Id = 1, Name = "Motor" },
                new Transportation(){ Id = 2, Name = "Mobil" }
            };
            transportations.ForEach(x => context.Transportations.Add(x));
            context.SaveChanges();

            //parking areas
            var parkings = new List<ParkingArea>
            {
                new ParkingArea{ Id = 1, TicketCode = "PRK001", TransportationTypeId = 1, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 2, TicketCode = "PRK002", TransportationTypeId = 1, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 3, TicketCode = "PRK003", TransportationTypeId = 1, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 4, TicketCode = "PRK004", TransportationTypeId = 1, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 5, TicketCode = "PRK005", TransportationTypeId = 1, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 6, TicketCode = "PRK006", TransportationTypeId = 2, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 7, TicketCode = "PRK007", TransportationTypeId = 2, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 8, TicketCode = "PRK008", TransportationTypeId = 2, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 9, TicketCode = "PRK009", TransportationTypeId = 2, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
                new ParkingArea{ Id = 10, TicketCode = "PRK010", TransportationTypeId = 2, PlateNumber = "xxx123", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 0, 0), Amount = 2000 },
            };
            parkings.ForEach(x => context.Parkings.Add(x));
            context.SaveChanges();

            base.Seed(context);

        }
    }
}

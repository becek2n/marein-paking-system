using Parking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Seed
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
                new ParkingArea{ Id = 1, TicketCode = "PRK20210525001", TransportationId = 1, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 10, 0), Amount = 2000 },
                new ParkingArea{ Id = 2, TicketCode = "PRK20210525002", TransportationId = 1, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 20, 0), Amount = 2000 },
                new ParkingArea{ Id = 3, TicketCode = "PRK20210525003", TransportationId = 1, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 30, 0), Amount = 2000 },
                new ParkingArea{ Id = 4, TicketCode = "PRK20210525004", TransportationId = 1, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 40, 0), Amount = 2000 },
                new ParkingArea{ Id = 5, TicketCode = "PRK20210525005", TransportationId = 1, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 50, 0), Amount = 2000 },
                new ParkingArea{ Id = 6, TicketCode = "PRK20210525006", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 60, 0), Amount = 2000 },
                new ParkingArea{ Id = 7, TicketCode = "PRK20210525007", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 05, 0), Amount = 2000 },
                new ParkingArea{ Id = 8, TicketCode = "PRK20210525008", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 06, 0), Amount = 2000 },
                new ParkingArea{ Id = 9, TicketCode = "PRK20210525009", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 07, 0), Amount = 2000 },
                new ParkingArea{ Id = 10, TicketCode = "PRK20210525010", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 08, 0), Amount = 2000 },
                new ParkingArea{ Id = 11, TicketCode = "PRK20210525011", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 09, 0), Amount = 2000 },
                new ParkingArea{ Id = 12, TicketCode = "PRK20210525012", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 10, 0), Amount = 2000 },
                new ParkingArea{ Id = 13, TicketCode = "PRK20210525013", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 11, 0), Amount = 2000 },
                new ParkingArea{ Id = 14, TicketCode = "PRK20210525014", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 12, 0), Amount = 2000 },
                new ParkingArea{ Id = 15, TicketCode = "PRK20210525015", TransportationId = 2, PlateNumberFirst = "xxx", PlateNumberMiddle = "123", PlateNumberLast = "zzz", CheckIn = DateTime.Now, CheckOut = DateTime.Now, Duration = new TimeSpan(1, 13, 0), Amount = 2000 },
            };
            parkings.ForEach(x => context.Parkings.Add(x));
            context.SaveChanges();

            base.Seed(context);

        }
    }
}

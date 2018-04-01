using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueYonder.Reservations.Models
{
    public class ReservationDTO
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Confirmation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
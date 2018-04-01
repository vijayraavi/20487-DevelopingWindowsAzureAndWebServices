using BlueYonder.DataAccess;
using BlueYonder.Entities;
using BlueYonder.Entities.Enums;
using BlueYonder.Reservations.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlueYonder.Reservations.Controllers
{
    public class ReservationsController : ApiController
    {
        private const string RESERVATIONS_KEY = "BlueYonder.Reservations.LatestReservations";

        public IEnumerable<ReservationDTO> Get()
        {
            // TODO: Lab 12, Exercise 1, Task 1.3 : Fetch the query results from Redis
            string cachedReservations = RedisProvider.Cache.StringGet(RESERVATIONS_KEY);
            if (!String.IsNullOrEmpty(cachedReservations))
            {
                return JsonConvert.DeserializeObject<List<ReservationDTO>>(cachedReservations);
            }

            var reservations = GetLatestReservations(latest: 10);
            // TODO: Lab 12, Exercise 1, Task 1.2 : Store the query results in Redis
            RedisProvider.Cache.StringSet(RESERVATIONS_KEY, JsonConvert.SerializeObject(reservations));
            return reservations;
        }

        public HttpResponseMessage Post([FromUri]string traveler)
        {
            using (var context = new TravelCompanionContext())
            {
                Traveler newTraveler = new Traveler
                {
                    FirstName = traveler,
                    LastName = "Mc" + traveler,
                    HomeAddress = "One Microsoft Way",
                    MobilePhone = "555-555-5555",
                    Passport = "AB123456789",
                    TravelerUserIdentity = Guid.NewGuid().ToString()
                };
                context.Travelers.Add(newTraveler);
                context.SaveChanges();

                Flight flight = context.Flights.First();

                Reservation reservation = new Reservation
                {
                    ReservationDate = DateTime.Now,
                    DepartureFlight = new Trip
                    {
                        FlightInfo = flight.Schedules.First(),
                        Status = FlightStatus.Confirmed,
                        Class = SeatClass.Economy
                    },
                    ReturnFlight = null,
                    ConfirmationCode = Guid.NewGuid().ToString(),
                    TravelerId = newTraveler.TravelerId
                };
                context.Reservations.Add(reservation);
                context.SaveChanges();
            }

            // TODO: Lab 12, Exercise 1, Task 1.4 : Delete the cached results from Redis
            RedisProvider.Cache.KeyDelete(RESERVATIONS_KEY);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        private List<ReservationDTO> GetLatestReservations(int latest)
        {
            using (var context = new TravelCompanionContext())
            {
                return (from reservation in context.Reservations
                        orderby reservation.ReservationDate descending
                        select new ReservationDTO
                        {
                            Source = reservation.DepartureFlight.FlightInfo.Flight.Source.City,
                            Destination = reservation.DepartureFlight.FlightInfo.Flight.Destination.City,
                            DepartureDate = reservation.DepartureFlight.FlightInfo.Departure,
                            Confirmation = reservation.ConfirmationCode,
                            ReservationDate = reservation.ReservationDate
                        }).Take(latest).ToList();
            }
        }
    }
}

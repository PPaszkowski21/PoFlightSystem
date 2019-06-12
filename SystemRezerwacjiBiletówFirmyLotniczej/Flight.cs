using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Flight : ObjectID
    {
        public Route Route { get; set; }
        public Airplane Airplane { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public List<Client> Clients { get; set; }
        public Flight(FlightSystem flightSystem, Route route, DateTime departureTime)
        {
            this.Route = route;
            this.Airplane = flightSystem.GetAirplane(route);
            this.DepartureTime = departureTime;
            this.ArrivalTime = departureTime.AddHours(CalculateTravelTimeHours());
        }
        public Flight(Flight flight, double days)
        {
            this.Route = flight.Route;
            this.Airplane = flight.Airplane;
            this.DepartureTime = flight.DepartureTime.AddDays(days);
            this.ArrivalTime = flight.DepartureTime.AddDays(days).AddHours(CalculateTravelTimeHours());
        }
        double CalculateTravelTimeHours()
        {
            return Route.Distance / Airplane.Speed;
        }
        
    }
}

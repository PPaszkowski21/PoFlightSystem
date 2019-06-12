using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Flight : ObjectID
    {
        //Klasa lot, składa się z trasy, samolotu, czasu odlotu, przylotu na miejsce i powrotu ( struktura DateTime )
        // oraz listy pasażerów/klientów
        public Route Route { get; set; }
        public Airplane Airplane { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public List<Client> Clients { get; set; }
        //Konstruktor do planowania nowych lotów
        public Flight(FlightSystem flightSystem, Route route, DateTime departureTime)
        {
            this.Route = route;
            this.DepartureTime = departureTime;
            //Przypisuje optymalny samolot na podstawie trasy i czasu odlotu, metoda GetAirplane(trasa,czas odlotu))
            this.Airplane = flightSystem.GetAirplane(route,departureTime);
            if(this.Airplane == null)
            {
                Console.WriteLine("Nie ma wolnych samolotów na ten typ trasy!");
            }
            //Obliczanie poszczególnych czasów metodą CalculateTravelTimeHours())
            this.ArrivalTime = departureTime.AddHours(CalculateTravelTimeHours());
            //Dodawanie 24 na odpoczynek pilota
            this.ReturnTime = ArrivalTime.AddHours(CalculateTravelTimeHours() + 24);
            //Dodawanie do listy samolotów, które wyleciały
            flightSystem.ReturningAirplanes.Add(new ReturningAirplane(this.Airplane, this.ReturnTime));
            this.Clients = new List<Client>();
        }
        //Konstruktor kopiujący do powielania lotów, działa na podobnych zasach co poprzedni,
        // z tym, że parametrem jest też ilość dni o który chcemy przesunąć lot
        public Flight(FlightSystem flightSystem, Flight flight, double days)
        {
            this.Route = flight.Route;
            this.DepartureTime = flight.DepartureTime.AddDays(days);
            this.Airplane = flightSystem.GetAirplane(flight.Route,flight.DepartureTime.AddDays(days));
            if (this.Airplane == null)
            {
                Console.WriteLine("Nie ma wolnych samolotów na ten typ trasy!");
            }
            this.ArrivalTime = flight.DepartureTime.AddDays(days).AddHours(CalculateTravelTimeHours());
            this.ReturnTime = ArrivalTime.AddHours(CalculateTravelTimeHours() + 24);
            flightSystem.ReturningAirplanes.Add(new ReturningAirplane(this.Airplane, this.ReturnTime));
            this.Clients = new List<Client>();
        }
        //Metoda obliczająca czas podróży na podstawie długości trasy i prędkości samolotu
        double CalculateTravelTimeHours()
        {
            if (Airplane == null)
            {
                return 0;
            }
            else
            {
                return Route.Distance / Airplane.Speed;
            }
        }
        //Metoda służąca do zliczenia ilości zarezerwowanych biletów na lot
        public int CountTickets()
        {
            int Tickets = 0;
            foreach (Client client in Clients)
            {
                if (client is Agent agent)
                {
                    foreach (FlightTicket ticket in agent.FlightTickets)
                    {
                        if (ticket.FlightId == this.Id)
                        {
                            Tickets++;
                        }

                    }
                }
                else if (client is PrivatePerson privatePerson)
                {
                    if (privatePerson.FlightTicket.FlightId == this.Id)
                    {
                        Tickets++;
                    }
                }
            }
            return Tickets;
        }
        
    }
}

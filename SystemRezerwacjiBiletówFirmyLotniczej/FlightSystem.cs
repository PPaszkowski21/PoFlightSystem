using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class FlightSystem
    {
        //Klasa systemu lotniczego, zawiera listy wszystkich obiektów niezbędnych do funkcjonowania systemu, czyli
        //trasy, lotniska, samoloty, powracające samoloty, loty i klienci.
        public List<Flight> Flights { get; set; }
        public List<Client> Clients { get; set; }
        public List<Airport> Airports { get; set; }
        public List<Airplane> Airplanes { get; set; }
        public List<ReturningAirplane> ReturningAirplanes { get; set; }
        public List<Route> Routes { get; set; }
        public FlightSystem()
        {
            this.Flights = new List<Flight>();
            this.Clients = new List<Client>();
            this.Airports = new List<Airport>();
            this.Airplanes = new List<Airplane>();
            this.Routes = new List<Route>();
            this.ReturningAirplanes = new List<ReturningAirplane>();
        }
        //Metoda pobierania optymalnego samolotu, wykorzystywana w klasie Flight
        public Airplane GetAirplane(Route route, DateTime departureTime)
        {
            Airplane pom;
            ReturningAirplane pom2;
            // Jeżeli dystans jest dłuższy niż 4500km to wybiera samolot AirbusA300
            if (route.Distance > 4500)
            {
                //Na początek przeszukiwanie listy samolotów powracających ( takie rozwiązanie zostało zaimplementowane
                //ze względu na konieczność funkcjonalności powielania lotów i wygodę użycia )
                pom2 = ReturningAirplanes.FirstOrDefault(x => x.Airplane.GetType() == typeof(AirbusA300));
                if (pom2 == null)
                {
                    //Jeżeli nie znalazło samolotu powracającego to nastąpi przejście do części kodu odpowiedzialnego za dalsze szukanie
                    //na zwykłej liście samolotów
                }
                else
                {
                    //Jeżeli znalazło samolot tego typu to sprawdza czy samolot wróci do bazy przed planowanym odlotem,
                    //jeżeli tak to metoda usunie go z listy samolotów powracających ( tak by się nie dublowały ) i zwraca go
                    int GoIn = DateTime.Compare(pom2.ReturnTime, departureTime);
                    if (GoIn < 0)
                    {
                        ReturningAirplanes.Remove(pom2);
                        return pom2.Airplane;
                    }
                }
                //Jeżeli nie znalazło -> szukanie na normalnej liście, jeżeli jest to usunięcie z listy samolotów dostępnych i zwrócenie go
                pom = Airplanes.FirstOrDefault(x => x.GetType() == typeof(AirbusA300));
                if(pom == null)
                {
                    return null;
                }
                Airplanes.Remove(pom);
                return pom;
            }
            // Jeżeli dystans jest Krótszy niż 4500km, ale dłuższy niż 1800km to wybiera samolot Boeing737
            else if (route.Distance > 1800)
            {
                pom2 = ReturningAirplanes.FirstOrDefault(x => x.Airplane.GetType() == typeof(Boeing737));
                if (pom2 == null)
                {

                }
                else
                {
                    int GoIn = DateTime.Compare(pom2.ReturnTime, departureTime);
                    if (GoIn < 0)
                    {
                        ReturningAirplanes.Remove(pom2);
                        return pom2.Airplane;
                    }
                }
                pom = Airplanes.FirstOrDefault(x => x.GetType() == typeof(Boeing737));
                if (pom == null)
                {
                    return null;
                }
                Airplanes.Remove(pom);
                return pom;
            }
            // Jeżeli dystans jest krótszy niż 1800km to wybiera samolot Boeing737
            else
            {
                pom2 = ReturningAirplanes.FirstOrDefault(x => x.Airplane.GetType() == typeof(Tu_134));
                if (pom2 == null)
                {

                }
                else
                {
                    int GoIn = DateTime.Compare(pom2.ReturnTime, departureTime);
                    if (GoIn < 0)
                    {
                        ReturningAirplanes.Remove(pom2);
                        return pom2.Airplane;
                    }
                }
                pom = Airplanes.FirstOrDefault(x => x.GetType() == typeof(Tu_134));
                if (pom == null)
                {
                    return null;
                }
                Airplanes.Remove(pom);
                return pom;
            }
        }
    }
}

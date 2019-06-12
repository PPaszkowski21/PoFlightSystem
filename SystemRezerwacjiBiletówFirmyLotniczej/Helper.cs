using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRezerwacjiBiletówFirmyLotniczej;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    //Klasa w której znajdują się metody pomocniczne
    public class Helper
    {
        //Metody które dodają pożądany obiekt do listy i ustawiają jego ID, przeciążenie parametrów
        public static void SetIDAndAddToList(List<Client> Objects, Client Object)
        {
            if (Objects.Any())
            {
                Object.Id = Objects.Max(x => x.Id) + 1;
            }
            else
            {
                Object.Id = 0;
            }
            Objects.Add(Object);
        }
        public static void SetIDAndAddToList(List<Airplane> Objects, Airplane Object)
        {
            if (Objects.Any())
            {
                Object.Id = Objects.Max(x => x.Id) + 1;
            }
            else
            {
                Object.Id = 0;
            }
            Objects.Add(Object);
        }
        public static void SetIDAndAddToList(List<Airport> Objects, Airport Object)
        {
            if (Objects.Any())
            {
                Object.Id = Objects.Max(x => x.Id) + 1;
            }
            else
            {
                Object.Id = 0;
            }
            Objects.Add(Object);
        }
        public static void SetIDAndAddToList(List<Route> Objects, Route Object)
        {
            if (Objects.Any())
            {
                Object.Id = Objects.Max(x => x.Id) + 1;
            }
            else
            {
                Object.Id = 0;
            }
            Objects.Add(Object);
        }
        public static void SetIDAndAddToList(List<Flight> Objects, Flight Object)
        {
            if (Objects.Any())
            {
                Object.Id = Objects.Max(x => x.Id) + 1;
            }
            else
            {
                Object.Id = 0;
            }
            Objects.Add(Object);
        }
        //Metody usuwające obiekt z listy, w przypadku samolotu zwraca on samolot, ponieważ, jest to potrzebne do dodania
        //do listy samolotów powracających
        public static void RemoveFromList(List<Client> Objects)
        {
            Console.WriteLine("Podaj id");
            int currentID = Int32.Parse(Console.ReadLine());
            var currentItem = Objects.FirstOrDefault(x => x.Id == currentID);
            if (currentItem != null)
            {
                Objects.Remove(currentItem);
            }
        }
        public static void RemoveFromList(List<Airplane> Objects)
        {
            Console.WriteLine("Podaj id");
            int currentID = Int32.Parse(Console.ReadLine());
            var currentItem = Objects.FirstOrDefault(x => x.Id == currentID);
            if (currentItem != null)
            {
                Objects.Remove(currentItem);
            }
        }
        public static void RemoveFromList(List<Airport> Objects)
        {
            Console.WriteLine("Podaj id");
            int currentID = Int32.Parse(Console.ReadLine());
            var currentItem = Objects.FirstOrDefault(x => x.Id == currentID);
            if (currentItem != null)
            {
                Objects.Remove(currentItem);
            }
        }
        public static void RemoveFromList(List<Route> Objects)
        {
            Console.WriteLine("Podaj id");
            int currentID = Int32.Parse(Console.ReadLine());
            var currentItem = Objects.FirstOrDefault(x => x.Id == currentID);
            if (currentItem != null)
            {
                Objects.Remove(currentItem);
            }
        }
        public static Airplane RemoveFromList(List<Flight> Objects)
        {
            Console.WriteLine("Podaj id");
            int currentID = Int32.Parse(Console.ReadLine());
            var currentItem = Objects.FirstOrDefault(x => x.Id == currentID);
            if (currentItem != null)
            {
                Objects.Remove(currentItem);
            }
                return currentItem?.Airplane;
        }
        
        //Metoda zapisująca stan systemu do pliku lub wypisująca na ekran, zależnie od podanego parametru, zastowana refleksja
        public static void Print(FlightSystem flightSystem, Type type)
        {
            //Zabezpiecznie przed podaniem niewłaściwych parametrów, pożadane to StreamWriter ( zapisuje do pliku ) i Console ( do konsoli )
            if (type != typeof(StreamWriter) && type != typeof(Console))
            {
                Console.WriteLine("Błędne parametry");
                return;
            }
            //Pobranie informacji o metodzie
            var writeLineMethod = type.GetMethod("WriteLine", new[] { typeof(string) });
            //Instacja obiektu potrzebna w przypadku StreamWritera
            object classInstance = null;
            if (type == typeof(StreamWriter))
            {
                //Stworzenie instacji StreamWriter z nazwą pliku
                classInstance = Activator.CreateInstance(type, "Stan systemu.txt");
            }
            //Kolejne linijki to wydruk stanu systemu za pomocą wywołania metody pobranej z przekazanego typu
            writeLineMethod.Invoke(classInstance, new object[] { "Lista lotnisk:" });
            if (flightSystem.Airports.Any())
            {
                foreach (var airport in flightSystem.Airports)
                {
                    //Jeżeli w stringu chcemy przekazać zmienne to musimy użyć konstrukcji string.Format
                    writeLineMethod.Invoke(classInstance, new object[] { string.Format("{0}. {1} Wspolrzedne geograficzne {2}",
                        airport.Id, airport.Name, airport.Coordinate) });
                }
            }
            else
            {
                writeLineMethod.Invoke(classInstance, new object[] { "Pusta" });
            }
            writeLineMethod.Invoke(classInstance, new object[] { "Lista samolotow:" });
            if (flightSystem.Airplanes.Any())
            {
                foreach (var airplane in flightSystem.Airplanes)
                {
                    writeLineMethod.Invoke(classInstance, new object[] { string.Format("{0}. {1} - {2}",
                        airplane.Id, airplane.Name, airplane.GetType().Name) });
                }
            }
            else
            {
                writeLineMethod.Invoke(classInstance, new object[] { "Pusta" });
            }
            writeLineMethod.Invoke(classInstance, new object[] { "Lista tras:" });
            if (flightSystem.Routes.Any())
            {
                foreach (var route in flightSystem.Routes)
                {
                    writeLineMethod.Invoke(classInstance, new object[] { string.Format("{0}. {1} - {2} = {3}km",
                        route.Id, route.Start.Name, route.End.Name, Math.Round(route.Distance)) });
                }
            }
            else
            {
                writeLineMethod.Invoke(classInstance, new object[] { "Pusta" });
            }
            writeLineMethod.Invoke(classInstance, new object[] { "Lista klientów:" });
            if (flightSystem.Clients.Any())
            {
                foreach (var client in flightSystem.Clients)
                {
                    if (client is Agent agent)
                    {
                        writeLineMethod.Invoke(classInstance, new object[] { string.Format("{0}. {1}",
                            agent.Id, agent.Name) });
                    }
                    else if (client is PrivatePerson privatePerson)
                    {
                        writeLineMethod.Invoke(classInstance, new object[] { string.Format("{0}. {1} {2} ur. {3}",
                            privatePerson.Id, privatePerson.Name, privatePerson.Surname, 
                            privatePerson.DateOfBirth.ToString("dd/MM/yyyy")) });
                    }
                }
            }
            else
            {
                writeLineMethod.Invoke(classInstance, new object[] { "Pusta" });
            }
            writeLineMethod.Invoke(classInstance, new object[] { "Lista lotów:" });
            if (flightSystem.Flights.Any())
            {
                int Tickets = 0;
                foreach (Flight flight in flightSystem.Flights)
                {
                    Tickets = 0;
                    foreach (Client client in flight.Clients)
                    {
                        if (client is Agent agent)
                        {
                            foreach (FlightTicket ticket in agent.FlightTickets)
                            {
                                if (ticket.FlightId == flight.Id)
                                {
                                    Tickets++;
                                }

                            }
                        }
                        else if (client is PrivatePerson privatePerson)
                        {
                            if (privatePerson.FlightTicket.FlightId == flight.Id)
                            {
                                Tickets++;
                            }
                        }
                    }
                    writeLineMethod.Invoke(classInstance, new object[]
                    { string.Format("{0}. {1} - {2} = {3}km ({4}) Odlot: {5} Przylot: {6} Ilość zajetych miejsc: {7}/{8}",
                    flight.Id, flight.Route.Start.Name,
                            flight.Route.End.Name, Math.Round(flight.Route.Distance), flight.Airplane.Name, 
                            flight.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            flight.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss"), Tickets, flight.Airplane.NumberOfSeats) });
                }
            }
            else
            {
                writeLineMethod.Invoke(classInstance, new object[] { "Pusta" });
            }
            writeLineMethod.Invoke(classInstance, new object[] { "" });
            //Jeżeli typ to StreamWriter, potrzebne jest zamknięcie strumienia
            if (type == typeof(StreamWriter))
            {
                var closeMethod = type.GetMethod("Close");
                closeMethod.Invoke(classInstance, null);
            }
        }
        //Metody zapisujące stan systemu do pliku tekstowego i do konsoli, zakomentowane ze względu na znalezione lepsze rozwiązanie)
        //public static void PrintStateOfSystem(FlightSystem flightSystem)
        //{
        //    Console.WriteLine("Lista lotnisk:");
        //    if (flightSystem.Airports.Any())
        //    {
        //        foreach (var airport in flightSystem.Airports)
        //        {
        //            Console.WriteLine("{0}. {1} Wspolrzedne geograficzne {2}", airport.Id, airport.Name, airport.Coordinate);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pusta");
        //    }
        //    Console.WriteLine("Lista samolotow:");
        //    if (flightSystem.Airplanes.Any())
        //    {
        //        foreach (var airplane in flightSystem.Airplanes)
        //        {
        //            Console.WriteLine("{0}. {1} - {2}", airplane.Id, airplane.Name, airplane.GetType().Name);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pusta");
        //    }
        //    Console.WriteLine("Lista tras:");
        //    if (flightSystem.Routes.Any())
        //    {
        //        foreach (var route in flightSystem.Routes)
        //        {
        //            Console.WriteLine("{0}. {1} - {2} = {3}km", route.Id, route.Start.Name, route.End.Name, Math.Round(route.Distance));
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pusta");
        //    }
        //    Console.WriteLine("Lista klientów:");
        //    if (flightSystem.Clients.Any())
        //    {
        //        foreach (var client in flightSystem.Clients)
        //        {
        //            if (client is Agent agent)
        //            {
        //                Console.WriteLine("{0}. {1}", agent.Id, agent.Name);
        //            }
        //            else if (client is PrivatePerson privatePerson)
        //            {
        //                Console.WriteLine("{0}. {1} {2} ur. {3}", privatePerson.Id, privatePerson.Name, privatePerson.Surname, privatePerson.DateOfBirth.ToString("dd/MM/yyyy"));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pusta");
        //    }
        //    Console.WriteLine("Lista lotów:");
        //    if (flightSystem.Flights.Any())
        //    {
        //        int Tickets = 0;
        //        foreach (Flight flight in flightSystem.Flights)
        //        {
        //            Tickets = 0;
        //            foreach (Client client in flight.Clients)
        //            {
        //                if (client is Agent agent)
        //                {
        //                    foreach (FlightTicket ticket in agent.FlightTickets)
        //                    {
        //                        Tickets++;
        //                    }
        //                }
        //                else if (client is PrivatePerson)
        //                {
        //                    Tickets++;
        //                }
        //            }
        //            Console.WriteLine("{0}. {1} - {2} = {3}km ({4}) Odlot: {5} Przylot: {6} Ilość zajetych miejsc: {7}/{8}", flight.Id, flight.Route.Start.Name,
        //                    flight.Route.End.Name, Math.Round(flight.Route.Distance), flight.Airplane.Name, flight.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss"),
        //                    flight.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss"), Tickets, flight.Airplane.NumberOfSeats);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pusta");
        //    }
        //    Console.WriteLine();
        //}
        //public static void PrintStateOfSystemToFile(FlightSystem flightSystem)
        //{
        //    using (StreamWriter stream = new StreamWriter("Stan systemu.txt"))
        //    {
        //        stream.WriteLine("Lista lotnisk:");
        //        if (flightSystem.Airports.Any())
        //        {
        //            foreach (var airport in flightSystem.Airports)
        //            {
        //                stream.WriteLine("{0}. {1} Wspolrzedne geograficzne {2}", airport.Id, airport.Name, airport.Coordinate);
        //            }
        //        }
        //        else
        //        {
        //            stream.WriteLine("Pusta");
        //        }
        //        stream.WriteLine("Lista samolotow:");
        //        if (flightSystem.Airplanes.Any())
        //        {
        //            foreach (var airplane in flightSystem.Airplanes)
        //            {
        //                stream.WriteLine("{0}. {1} - {2}", airplane.Id, airplane.Name, airplane.GetType().Name);
        //            }
        //        }
        //        else
        //        {
        //            stream.WriteLine("Pusta");
        //        }
        //        stream.WriteLine("Lista tras:");
        //        if (flightSystem.Routes.Any())
        //        {
        //            foreach (var route in flightSystem.Routes)
        //            {
        //               stream.WriteLine("{0}. {1} - {2} = {3}km", route.Id, route.Start.Name, route.End.Name, Math.Round(route.Distance));
        //            }
        //        }
        //        else
        //        {
        //            stream.WriteLine("Pusta");
        //        }
        //        stream.WriteLine("Lista klientów:");
        //        if (flightSystem.Clients.Any())
        //        {
        //            foreach (var client in flightSystem.Clients)
        //            {
        //                if (client is Agent agent)
        //                {
        //                    stream.WriteLine("{0}. {1}", agent.Id, agent.Name);
        //                }
        //                else if (client is PrivatePerson privatePerson)
        //                {
        //                    stream.WriteLine("{0}. {1} {2} ur. {3}", privatePerson.Id, privatePerson.Name, privatePerson.Surname, privatePerson.DateOfBirth.ToString("dd/MM/yyyy"));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            stream.WriteLine("Pusta");
        //        }
        //        stream.WriteLine("Lista lotów:");
        //        if (flightSystem.Flights.Any())
        //        {
        //            foreach (var flight in flightSystem.Flights)
        //            {
        //                int Tickets = 0;
        //                foreach (Client client in flight.Clients)
        //                {
        //                    if (client is Agent agent)
        //                    {
        //                        foreach (FlightTicket ticket in agent.FlightTickets)
        //                        {
        //                            Tickets++;
        //                        }
        //                    }
        //                    else if (client is PrivatePerson)
        //                    {
        //                        Tickets++;
        //                    }
        //                }
        //                stream.WriteLine("{0}. {1} - {2} = {3}km ({4}) Odlot: {5} Przylot: {6} Ilość zajetych miejsc: {7}/{8}", flight.Id, flight.Route.Start.Name,
        //                    flight.Route.End.Name, Math.Round(flight.Route.Distance), flight.Airplane.Name, flight.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss"),
        //                    flight.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss"), Tickets, flight.Airplane.NumberOfSeats);

        //            }
        //        }
        //        else
        //        {
        //            stream.WriteLine("Pusta");
        //        }
        //        stream.Close();
        //    }
        //}


    }
}

using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRezerwacjiBiletówFirmyLotniczej.CustomException;
//Wykorzystana biblioteka języka C# to System.Device.Location

//Przestrzeń nazw "System.Device.Location umożliwia deweloperom łatwy sposób uzyskiwać dostęp do komputera lokalizacji
//przy użyciu pojedynczego interfejsu API. Informacje o lokalizacji może pochodzić z wielu dostawców,
//takich jak GPS, triangulacji sieci Wi-Fi oraz komórki triangulacji tower telefonu. System.Device.Location
//Klasy zapewnić pojedynczy interfejs API do hermetyzacji wielu dostawców lokalizacji na komputerze i obsługują
//priorytetyzację bezproblemowe i przechodzenie między nimi. W rezultacie deweloperzy aplikacji, którzy korzystają
//z tego interfejsu API nie trzeba dostosować aplikacje na konkretnych konfiguracjach sprzętu.

//Użyliśmy tutaj klasy GeoCoordinate, która znalazła swoje zastosowanie w określaniu lokalizacji lotnisk i wyznaczaniu odległości między nimi

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inicjalizacja systemu lotniczego, czyli "głównej klasy"
            FlightSystem flightSystem = new FlightSystem();
            //Uzupełnienie bazy danych systemu przykładowymi wartościami
            TestDataBase(flightSystem);
            //Konsolowe menu
            Menu(flightSystem);
        }
        static void Menu(FlightSystem flightSystem)
        {
            //Pętla która po każdej skończonej operacji powraca nam do interfejsu
            while (true)
            {
                Console.Clear();
                Console.WriteLine("System rezerwacji biletow firmy lotniczej:");
                //Metoda z wykorzystaniem refleksji, wydruk stanu systemu do konsoli
                Helper.Print(flightSystem,typeof(Console));
                Console.WriteLine("1.Lotniska.");
                Console.WriteLine("2.Samoloty.");
                Console.WriteLine("3.Trasy.");
                Console.WriteLine("4.Klienci.");
                Console.WriteLine("5.Loty.");
                Console.WriteLine("6.Rezerwacja biletów.");
                Console.WriteLine("7.Zapis stanu systemu na dysk.");
                //Pętla, dzięki której interfejs główny jest przyjazny użytkownikowi ( uwzględnia błędne dane wejściowe )
                if (!Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }

                switch (choice)
                {  
                    case 1:
                        {
                            Console.WriteLine("1.Dodaj lotnisko.");
                            Console.WriteLine("2.Usun lotnisko");
                            int choice2 = Int32.Parse(Console.ReadLine());
                            switch (choice2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Podaj nazwe i dokladne wspolrzedne");
                                        Helper.SetIDAndAddToList(flightSystem.Airports, new Airport(Console.ReadLine(),
                                            double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine())));
                                        break;
                                    }
                                case 2:
                                    {
                                        Helper.RemoveFromList(flightSystem.Airports);
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("1.Dodaj samolot.");
                            Console.WriteLine("2.Usun samolot");
                            int choice3 = Int32.Parse(Console.ReadLine());
                            switch (choice3)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Jaki samolot?");
                                        Console.WriteLine("1.Boeing737");
                                        Console.WriteLine("2.AirbusA300");
                                        Console.WriteLine("3.Tu-134");
                                        int choice4 = Int32.Parse(Console.ReadLine());
                                        Console.WriteLine("Podaj nazwe");
                                        switch (choice4)
                                        {

                                            case 1:
                                                {
                                                    Helper.SetIDAndAddToList(flightSystem.Airplanes, new Boeing737(Console.ReadLine()));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Helper.SetIDAndAddToList(flightSystem.Airplanes, new AirbusA300(Console.ReadLine()));
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    Helper.SetIDAndAddToList(flightSystem.Airplanes, new Tu_134(Console.ReadLine()));
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        Helper.RemoveFromList(flightSystem.Airplanes);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("1.Dodaj trase.");
                            Console.WriteLine("2.Usun trase");
                            int choice4 = Int32.Parse(Console.ReadLine());
                            switch (choice4)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Podaj id startowego lotniska oraz id koncowego lotniska");
                                        try
                                        {
                                            Helper.SetIDAndAddToList(flightSystem.Routes, 
                                                new Route(flightSystem.Airports[Int32.Parse(Console.ReadLine())],
                                                flightSystem.Airports[Int32.Parse(Console.ReadLine())]));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        Helper.RemoveFromList(flightSystem.Routes);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("1.Dodaj klienta.");
                            Console.WriteLine("2.Usun klienta");
                            int choice5 = Int32.Parse(Console.ReadLine());
                            switch (choice5)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("1.Firma posredniczaca");
                                        Console.WriteLine("2.Osoba prywatna");
                                        int choice6 = Int32.Parse(Console.ReadLine());
                                        switch(choice6)
                                        {
                                            case 1:
                                                {
                                                    Console.WriteLine("Podaj nazwe firmy");
                                                    Helper.SetIDAndAddToList(flightSystem.Clients, new Agent(Console.ReadLine()));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Console.WriteLine("Podaj date urodzenia ( format: DD.MM.RRRR ) , imie i nazwisko");
                                                    DateTime userDateTime;
                                                    //Próba konwersji daty, jeżeli się nie uda, ustawia domyślną
                                                    DateTime.TryParse(Console.ReadLine(), out userDateTime);
                                                    Helper.SetIDAndAddToList(flightSystem.Clients,
                                                        new PrivatePerson(Console.ReadLine(), Console.ReadLine(), userDateTime));
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        Helper.RemoveFromList(flightSystem.Clients);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("1.Dodaj nowy lot");
                            Console.WriteLine("2.Usuń lot");
                            Console.WriteLine("3.Powiel lot");
                            Console.WriteLine("4.Wyswietl liste biletów dla konkretnego lotu");
                            int choice6 = Int32.Parse(Console.ReadLine());
                            switch (choice6)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Podaj date i godzine wylotu ( format: RRRR.MM.DD GG:mm:SS ) oraz id trasy");
                                        DateTime FlightDateTime;
                                        //Próba konwersji daty, jeżeli się nie uda, ustawia domyślną
                                        DateTime.TryParse(Console.ReadLine(), out FlightDateTime);
                                        int idOfRoute = Int32.Parse(Console.ReadLine());
                                        //Możliwość wystąpienia wyjątku ( indeks spoza zakresu )
                                        try
                                        {
                                            Flight flight = new Flight(flightSystem, flightSystem.Routes[idOfRoute], FlightDateTime);
                                            //Jeżeli istnieje samolot pasujacy do tego lotu to doda lot
                                            if (flight.Airplane != null)
                                            {
                                                Helper.SetIDAndAddToList(flightSystem.Flights, flight);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        
                                        break;
                                    }
                                case 2:
                                    {
                                        var currentPlane = Helper.RemoveFromList(flightSystem.Flights);
                                        //Usuwanie lotu spotyka się z trudnością, którą powodują dwie listy samolotów
                                        //powracające i te w bazie, wybieramy lot który chcemy usunąć, jeżeli usuwamy lot
                                        //to logicznym jest, że samolot wraca do bazy
                                        if (currentPlane != null)
                                        {
                                            //Samolot nie moze wrocic do bazy jezeli jest juz w bazie
                                            if (!flightSystem.Airplanes.Any(x=>x.Id == currentPlane.Id))
                                            {
                                                flightSystem.Airplanes.Add(currentPlane);
                                            }
                                            //Samolot nie moze zostac dodany do bazy, jezeli ma byc wykorzystany w innych lotach
                                            if (flightSystem.Flights.Any(x => x.Airplane.Id == currentPlane.Id))
                                            {
                                                flightSystem.Airplanes.Remove(currentPlane);
                                            }
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("Podaj id lotu ktory chcesz powielic: ");
                                        int IdToMultiple = Int32.Parse(Console.ReadLine());
                                        if(flightSystem.Flights.ElementAtOrDefault(IdToMultiple) == null)
                                        {
                                            Console.WriteLine("Nie ma takiego lotu!");
                                            break;
                                        }
                                        Console.WriteLine("1.Powiel na nastepnych X dni");
                                        Console.WriteLine("2.Powiel na ten sam dzien w X tygodniach");
                                        int choice8 = Int32.Parse(Console.ReadLine());
                                        switch(choice8)
                                        {
                                            case 1:
                                                {
                                                    Console.WriteLine("Podaj X - liczbe dni:");
                                                    int HowManyDays = Int32.Parse(Console.ReadLine());
                                                    for (int i = 1; i < HowManyDays+1; i++)
                                                    {
                                                        //Pobranie typu samolotu
                                                        if(flightSystem.Airplanes.Any(x=>x.GetType() == (flightSystem.Flights[IdToMultiple].Airplane.GetType())))
                                                        {
                                                            //Dodanie do listy skopiowanego lotu na X kolejnych dni
                                                            Helper.SetIDAndAddToList(flightSystem.Flights, new Flight(flightSystem,flightSystem.Flights[IdToMultiple], i));
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Brak samolotów żeby zaplanować taki lot!");
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Console.WriteLine("Podaj X - liczbe tygodni:");
                                                    int HowManyWeeks = Int32.Parse(Console.ReadLine());
                                                    for (int i = 1; i < HowManyWeeks+1; i++)
                                                    {
                                                        //Pobranie typu samolotu
                                                        if (flightSystem.Airplanes.Any(x => x.GetType() == (flightSystem.Flights[IdToMultiple].Airplane.GetType())))
                                                        {
                                                            //Dodanie do listy skopiowanego lotu na X kolejnych tygodni
                                                            Helper.SetIDAndAddToList(flightSystem.Flights, new Flight(flightSystem, flightSystem.Flights[IdToMultiple], 7 * i));
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Brak samolotów żeby zaplanować taki lot!");
                                                        }

                                                    }
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("Podaj numer lotu");
                                        int whichFlight = Int32.Parse(Console.ReadLine());
                                        //Wyłapywanie wyjątków
                                        try
                                        {
                                            //Wyświetlanie listy biletów dla konkretnego lotu
                                            {
                                                Console.WriteLine("{0}. {1} - {2} = {3}km ({4}) Odlot: {5}" +
                                                    " Przylot: {6} Ilość zajetych miejsc: {7}/{8}", 
                                                    flightSystem.Flights[whichFlight].Id,
                                                    flightSystem.Flights[whichFlight].Route.Start.Name,
                                                    flightSystem.Flights[whichFlight].Route.End.Name,
                                                    Math.Round(flightSystem.Flights[whichFlight].Route.Distance),
                                                    flightSystem.Flights[whichFlight].Airplane.Name,
                                                    flightSystem.Flights[whichFlight].DepartureTime.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    flightSystem.Flights[whichFlight].ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    flightSystem.Flights[whichFlight].CountTickets(),
                                                    flightSystem.Flights[whichFlight].Airplane.NumberOfSeats);

                                                foreach (Client client in flightSystem.Flights[whichFlight].Clients)
                                                {
                                                    //Wypisywanie w przypadku firmy posredniczacej
                                                    if(client is Agent agent)
                                                    {
                                                        Console.WriteLine("Nazwa firmy: {0} Ilość biletów: {1}", 
                                                            agent.Name, agent.FlightTickets.Count);
                                                        //Wszystkie bilety dla tej firmy
                                                        foreach (FlightTicket flightTicket in agent.FlightTickets)
                                                        {
                                                            //Wypisywanie w przypadku biletu VIP
                                                            if(flightTicket is VIPFlightTicket vip)
                                                            {
                                                                Console.WriteLine("Bilet: {0} Darmowy alkohol: {1} Przekąska {2} ",
                                                                    vip.GetType().Name, vip.FreeDrinking, vip.RandomSnack);
                                                            }
                                                            //Wypisywanie w przypadku biletu zwyklego
                                                            else
                                                            {
                                                                Console.WriteLine("Bilet: {0} Darmowy alkohol: {1}",
                                                                    flightTicket.GetType().Name, flightTicket.FreeDrinking);
                                                            }
                                                        }
                                                    }
                                                    //Wypisywanie w przypadku prywatnej osoby
                                                    else if (client is PrivatePerson privatePerson)
                                                    {
                                                        Console.WriteLine("Imie i nazwisko: {0} {1}",
                                                            privatePerson.Name, privatePerson.Surname);
                                                        //Bilet Vip
                                                        if (privatePerson.FlightTicket is VIPFlightTicket vip)
                                                        {
                                                            Console.WriteLine("Bilet: {0} Darmowy alkohol: {1} Przekąska {2} ",
                                                                vip.GetType().Name, vip.FreeDrinking, vip.RandomSnack);
                                                        }
                                                        //Zwykly bilet
                                                        else
                                                        {
                                                            Console.WriteLine("Bilet: {0} Darmowy alkohol: {1}",
                                                                privatePerson.FlightTicket.GetType().Name, privatePerson.FlightTicket.FreeDrinking);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }

                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("1.Rezerwacja");
                            Console.WriteLine("2.Lot premium");
                            int choice9 = Int32.Parse(Console.ReadLine());
                            switch(choice9)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Podaj indeks klienta:");
                                        int clientIndex = Int32.Parse(Console.ReadLine());
                                        Console.WriteLine("Podaj na ktory lot chcesz zarezerwowac bilet");
                                        int whichFlight = Int32.Parse(Console.ReadLine());
                                        Console.WriteLine("Bilet VIP ( 0 ) czy zwykly (inna liczba)?");
                                        int whichTicket = Int32.Parse(Console.ReadLine());
                                        int Tickets = 0;
                                        //Zliczanie ilości zarezerwowanych biletów na lot, wyłapywanie wyjątków
                                        try
                                        {
                                            foreach (Client client in flightSystem.Flights[whichFlight].Clients)
                                            {
                                                if (client is Agent agent1)
                                                {
                                                    foreach (FlightTicket ticket in agent1.FlightTickets)
                                                    {
                                                        Tickets++;
                                                    }
                                                }
                                                else if (client is PrivatePerson)
                                                {
                                                    Tickets++;
                                                }
                                            }
                                            if (flightSystem.Clients[clientIndex] is Agent agent)
                                            {
                                                Console.WriteLine("Ile biletów?");
                                                int HowManyTickets = Int32.Parse(Console.ReadLine());
                                                if (flightSystem.Flights[whichFlight].Airplane.NumberOfSeats >= Tickets + HowManyTickets)
                                                {
                                                    for (int i = 0; i < HowManyTickets; i++)
                                                    {
                                                        if (whichTicket == 0)
                                                        {
                                                            agent.FlightTickets.Add(new VIPFlightTicket(whichFlight));
                                                        }
                                                        else
                                                        {
                                                            agent.FlightTickets.Add(new FlightTicket(whichFlight));
                                                        }
                                                    }
                                                    flightSystem.Flights[whichFlight].Clients.Add(agent);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Brak tylu wolnych miejsc na ten lot!");
                                                }
                                                Tickets = 0;
                                            }
                                            else if (flightSystem.Clients[clientIndex] is PrivatePerson privatePerson)
                                            {
                                                if (flightSystem.Flights[whichFlight].Airplane.NumberOfSeats >= Tickets + 1)
                                                {
                                                    if (whichTicket == 0)
                                                    {
                                                        privatePerson.FlightTicket = new VIPFlightTicket(whichFlight);
                                                    }
                                                    else
                                                    {
                                                        privatePerson.FlightTicket = new FlightTicket(whichFlight);
                                                    }
                                                    flightSystem.Flights[whichFlight].Clients.Add(privatePerson);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Brak wolnych miejsc na ten lot!");
                                                }
                                                Tickets = 0;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        //Funkcjonalność ulepszania biletów zaimplementowana za pomocą metod wirtualnych
                                        Console.WriteLine("Podaj numer lotu, w ktorym chcesz ulepszyc bilety:");
                                        int whichFlight = Int32.Parse(Console.ReadLine());
                                        try
                                        {
                                            foreach  (Client client in flightSystem.Flights[whichFlight].Clients)
                                            {
                                                if(client is Agent agent)
                                                {
                                                    foreach (FlightTicket ticket in agent.FlightTickets)
                                                    {
                                                        ValidateIsUprgaded(ticket);
                                                        ticket.UprgadeTicket();
                                                    }
                                                }
                                                else if(client is PrivatePerson privatePerson)
                                                {
                                                    ValidateIsUprgaded(privatePerson.FlightTicket);
                                                    privatePerson.FlightTicket.UprgadeTicket();
                                                }
                                            }
                                        }
                                        catch (TicketUprgadeException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    }

                                default:
                                    break;
                            }
                            break;

                        }
                    case 7:
                        {
                            //Zapis do pliku
                            Helper.Print(flightSystem,typeof(StreamWriter));
                            Console.WriteLine("Zapisano.");
                            break;
                        }
                    default:
                        break;
                }
                Console.WriteLine("Wciśnij cokolwiek, aby kontynuować ...");
                Console.ReadKey();
            }
        }
        public static void TestDataBase(FlightSystem flightSystem)
        {
            //Przykładowe wartości do systemu
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Boeing737("BOEING_pierwszy"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new AirbusA300("AIRBUSA300_pierwszy"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Tu_134("TU-134_pierwszy"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Boeing737("BOEINGdrugi"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new AirbusA300("AIRBUSA300_drugi"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Tu_134("TU-134_drugi"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Boeing737("BOEINGtrzeci"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new AirbusA300("AIRBUSA300_trzeci"));
            Helper.SetIDAndAddToList(flightSystem.Airplanes, new Tu_134("TU-134_trzeci"));

            Helper.SetIDAndAddToList(flightSystem.Airports, new Airport("Warszawa", 52.2297700, 21.0117800));
            Helper.SetIDAndAddToList(flightSystem.Airports, new Airport("Paryż", 48.8534100, 2.3488000));
            Helper.SetIDAndAddToList(flightSystem.Airports, new Airport("Moskwa", 55.7522200, 37.6155600));
            Helper.SetIDAndAddToList(flightSystem.Airports, new Airport("Tokio", 35.6895000, 139.6917100));

            Helper.SetIDAndAddToList(flightSystem.Routes, new Route(new Airport("Warszawa", 52.2297700, 21.0117800), new Airport("Paryż", 48.8534100, 2.3488000)));
            Helper.SetIDAndAddToList(flightSystem.Flights, new Flight(flightSystem, flightSystem.Routes[0], new DateTime(2019, 12, 10, 12, 20, 00)));
            Helper.SetIDAndAddToList(flightSystem.Clients, new Agent("RAINBOW"));
            Helper.SetIDAndAddToList(flightSystem.Clients, new PrivatePerson("Paweł", "Paszkowski", new DateTime(1998,11,14)));
            Helper.SetIDAndAddToList(flightSystem.Clients, new PrivatePerson("Alina", "Paszkowska", new DateTime(1965,2,14)));

        }
        //Metoda sprawdzająca czy bilet został już ulepszony
        public static void ValidateIsUprgaded(FlightTicket ticket)
        {
            if (ticket.FreeDrinking == true)
            throw new TicketUprgadeException("Bilet jest już ulepszony!");
        }
    }
}

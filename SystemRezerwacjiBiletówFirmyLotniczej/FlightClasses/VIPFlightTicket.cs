using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class VIPFlightTicket : FlightTicket
    {
        //Klasa dziedzicząca po zwykłym bilecie, bilet VIP, różni się dodatkową przekąską na czas lotu
        public string RandomSnack { get; set; }
        //Przekąski są przechowywane na liście
        public List<string> Snacks { get; set; }
        public VIPFlightTicket(int flightId) : base(flightId)
        {
            //Losowanie przekąski
            this.Snacks = new List<string>() { "Chocolate", "Bar", "Croissant", "Peanuts" };
            Random rand = new Random();
            this.RandomSnack = Snacks[rand.Next(0,4)] ;
            
        }
        //Przesłonięcie metody wirtualnej z klasy po której dziedziczy ta klasa ( FlightTicket ),
        //dodatkowa operacja podwajania przekąski
        public override void UprgadeTicket()
        {
            this.FreeDrinking = true;
            this.RandomSnack = this.RandomSnack + "x2";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class PrivatePerson : Client
    {
        //Klasa osoba prywatna, zawiera jeden bilet
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public FlightTicket FlightTicket { get; set; }
        public PrivatePerson(string name, string surname, DateTime DateOfBirth)
        {
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = DateOfBirth;
        }
    }
}

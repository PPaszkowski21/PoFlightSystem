using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class FlightTicket
    {
        //Klasa bilet lotniczy, posiada ID, dzięki któremu można przypisać bilety do konkretnego lotu
        public int FlightId { get; set; }
        //Opcja darmowego alkoholu, domyślnie wyłączona
        public bool FreeDrinking { get; set; }
        public FlightTicket(int flightId)
        {
            this.FlightId = flightId;
            this.FreeDrinking = false;
        }
        //Metoda wirtualna ( użyte rozwiązanie z powodu klasy dziedziczącej VIPFlightTicket), zezwalająca na picie darmowego alkoholu
        public virtual void UprgadeTicket()
        {
            this.FreeDrinking = true;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Agent : Client
    {
        //Firma pośrednicząca, która może zarezerwować wiele biletów, przechowuje je na liście
        public string Name { get; set; }
        public List<FlightTicket> FlightTickets = new List<FlightTicket>();
        public Agent(string name)
        {
            this.Name = name;
        }
    }
}

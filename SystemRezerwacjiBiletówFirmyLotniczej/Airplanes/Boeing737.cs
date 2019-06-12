using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Boeing737 : Airplane
    {
        // Jeden z typów samolotu, w konstruktorze od razu definiowane są jego atrybuty, samolot na średnie trasy.
        public Boeing737(string name) :base(name)
        {
            this.NumberOfSeats = 200;
            this.Range = 5000;
            this.Speed = 850;
        }
    }
}

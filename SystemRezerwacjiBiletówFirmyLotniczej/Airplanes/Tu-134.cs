using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Tu_134 : Airplane
    {
        // Jeden z typów samolotu, w konstruktorze od razu definiowane są jego atrybuty, samolot na krótkie trasy.
        public Tu_134(string name) : base(name)
        {
            this.NumberOfSeats = 100;
            this.Range = 2000;
            this.Speed = 750;
        }
    }
}

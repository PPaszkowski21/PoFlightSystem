using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class AirbusA300 : Airplane
    {
        // Jeden z typów samolotu, w konstruktorze od razu definiowane są jego atrybuty, samolot na długie trasy.
        public AirbusA300(string name) : base(name)
        {
            this.NumberOfSeats = 250;
            this.Range = 15000;
            this.Speed = 900;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Route : ObjectID
    {
        //Klasa trasa składająca się z dwóch lotnisk i dystansu między nimi
        public Airport Start { get; set; }
        public Airport End { get; set; }
        public double Distance { get; set; }
        public Route(Airport start, Airport end)
        {
            this.Start = start;
            this.End = end;
            //Obliczanie dystansu za pomocą metody z biblioteki GeoCordinate, wynik w kilometrach
            this.Distance = start.Coordinate.GetDistanceTo(end.Coordinate)/1000;
        }
    }
}

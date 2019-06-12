using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class Airport : ObjectID
    {
        //Klasa lotnisko, zawiera klasę GeoCoordinate, która jest reprezentacją współrzędnych geograficznych
        // i umożliwia wytyczanie tras między lotniskami.
        public string Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public Airport(string name, double x, double y)
        {
            this.Name = name;
            this.Coordinate = new GeoCoordinate(x, y);
        }
    }
}

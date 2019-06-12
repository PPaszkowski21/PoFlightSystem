using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public class ReturningAirplane
    {
        //Klasa samolotu, którego lot trwa, potrzebna do dodawania lotów odległych w przyszłości ( gdzie trzeba uwzględnić, że
        //niektóre samoloty już wrócą ze swoich lotów i będzie można je wykorzystać ), składa się danego samolotu i czasu powrotu
        public Airplane Airplane { get; set; }
        public DateTime ReturnTime { get; set; }
        public ReturningAirplane(Airplane airplane, DateTime returnTime)
        {
            this.Airplane = airplane;
            this.ReturnTime = returnTime;

        }
    }
}

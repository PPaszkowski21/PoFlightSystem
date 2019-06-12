using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej.CustomException
{
    //Niestandardowy wyjątek, który jest wyrzucany podczas próby ulepszania biletu już ulepszonego
    public class TicketUprgadeException : Exception
    {
        public TicketUprgadeException()
        {
         
        }
        public TicketUprgadeException(string message) : base (message)
        {

        }
    }
}

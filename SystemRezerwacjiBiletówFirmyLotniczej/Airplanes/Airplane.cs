using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiBiletówFirmyLotniczej
{
    public abstract class Airplane : ObjectID
    {
        //Klasa samolot wraz z dwoma konstruktorami, jeden normalny, który jest wykorzystywany
        //tylko przy konstruowaniu obiektów klas pochodnych, drugi kopiujący potrzebny do powrotów samolotów z lotów.
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public double Range { get; set; }
        public double Speed { get; set; }
        public Airplane(string name)
        {
            this.Name = name;
        }
        public Airplane(Airplane airplane)
        {
            this.Name = airplane.Name;
            this.NumberOfSeats = airplane.NumberOfSeats;
            this.Range = airplane.Range;
            this.Speed = airplane.Speed;
        }
    }
}

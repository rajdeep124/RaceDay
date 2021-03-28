using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDay
{
    public abstract class Punter
    {
        //***********FIELDS

        //***********PROPERTIES
        public string PunterName { get; set; }
        public double CashOnHand { get; set; }
        public double BetPlaced { get; set; }
        public string Alias { get; set; }
        public string PunterContenderChoice { get; set; }
        


        //***********CONSTRUCTOR
        protected Punter()
        {
            PunterName = " ";
            CashOnHand = 0;
            PunterContenderChoice = "";
        }

    }
}

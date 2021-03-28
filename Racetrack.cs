using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDay
{
    class Racetrack
    {
                //*****************FIELDS*********************
        readonly Random _random = new Random();

        //*****************CONSTRUCTOR****************
        public Racetrack()
        {
            RacetrackLength = 875;

        }

        //*****************PROPERTIES*****************

        public int RacetrackLength { get; set; }
        public int MyRandomNumber { get; set; }
        public string WinningContender { get; set; }


        //*****************METHOD*********************
        //generates my random numbers for contenders moves and place assignment
        public int RandomNumberGenerator(int num1, int num2)
        {
            MyRandomNumber = _random.Next(num1, num2);
            return MyRandomNumber;
        }
    }
}

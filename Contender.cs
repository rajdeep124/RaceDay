using System.Drawing;
using System.Windows.Forms;

namespace RaceDay
{
    public abstract class Contender
    {

        //*****************PROPERTIES*****************

        public string ContenderName { get; set; }
        public Image ContenderPicture { get; set; }
        public PictureBox PictuerBox { get; set; }


    }

    public class Student : Contender
    {

    }
}

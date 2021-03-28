using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using res = RaceDay.Properties.Resources; //shortcut to link to resource class


namespace RaceDay
{
    public partial class Form1 : Form
    {
        //Instantiate a list of my contenders from the contender class
        readonly Contender[] _myContenderList = new Contender[4];
        Punter[] _myPunters = new Punter[3]; //instantiate punter list
        readonly List<int> _myUniqueContenderList = new List<int>();
        readonly Racetrack _contender = new Racetrack();
        PictureBox[] _myBoxs = new PictureBox[4];


        public Form1()
        {
            InitializeComponent();
        }
        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            //loads my punters to myPunters list from the factory add to the combobox
            AddPuntersToList();


        }
        private void btnSet_Click(object sender, EventArgs e)
        {


            ResetRace();
            //Load contenders from myContender array
            ContenderList();

            //Calls method to add contender the pictureBox assign picturebox with contender
            AssignUniqueContenderToPictureBox();

            AddContendersToCombobox(_myUniqueContenderList); //Method to add contenders to combobox 

            //open betting panel    
            panel1.Visible = true;
            btnSet.Text = "Set Up Race";


        }
        private void btnRace_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer_Tick(object sender, EventArgs e) //While timer is running, each picture box get a random number to move forward
        {
            foreach (PictureBox box in _myBoxs)
            {
                //creates a random for each picture box using contender class
                MovePictureBoxLocation(box, _contender.RandomNumberGenerator(1, 50));

                //Works out which picturebox is the winning box and pass it to the winning property under contenders
                GetWinningContender(box);
            }



        }
        private void cbPunters_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Display punter details
            DisplayPunterDetails();
        }
        private void btnPlaceBet_Click(object sender, EventArgs e)
        {

            var myChosenPunter = _myPunters[cbPunters.SelectedIndex];



            //sets bet placed value for each punter
            myChosenPunter.BetPlaced = Convert.ToDouble(numericUDBet.Value);

            //Choose contender to bet on
            myChosenPunter.PunterContenderChoice = Convert.ToString(cbContenders.SelectedItem);

            //adds to text box which does not contain a value
            lbxResult.Items.Add(string.Format("{0} has placed ${1} on {2}", myChosenPunter.Alias,
                 myChosenPunter.BetPlaced, myChosenPunter.PunterContenderChoice));

            //Set to the default text values after each bet has been placed
            cbPunters.Text = "Please select a punter:";
            cbContenders.Text = "Choose your contender:";

            btnRace.Visible = true;
            btnSet.Visible = false;



        }
        #endregion

        //====================
        //  PICTURE BOX
        //====================
        private void AssignUniqueContenderToPictureBox()
        {
            int counter = 0;

            while (_myUniqueContenderList.Count != 4)
            {
                int number = _contender.RandomNumberGenerator(0, _myContenderList.Length);

                if (!_myUniqueContenderList.Contains(number))
                {
                    //Adds unique random number to the pictureBoxNumbersInts list
                    _myUniqueContenderList.Add(number);

                    counter += 1;

                }
            }

            //Select unique random contender image for each picturebox
            int uniqueContederValue = 0;
            foreach (var pictureBox in _myBoxs)
            {
                pictureBox.Image = _myContenderList[_myUniqueContenderList[uniqueContederValue]].ContenderPicture;
                uniqueContederValue += 1; //add to array value to of the unique contender list
            }


        }
        private void MovePictureBoxLocation(PictureBox myPictureBox, int raceAHead) //sets up the race by adding the random number to the picture box x
        {

            //Stop the timer when picture box reach end location
            if (myPictureBox.Left < _contender.RacetrackLength)
            {
                //move picture boxes with random sent in from 
                myPictureBox.Left += raceAHead;
            }
            else
            {
                timer1.Stop();
                //set winning picture box and send to Winner property
                myPictureBox.Left = _contender.RacetrackLength;
                lbxResult.Items.Clear();
                lblProfile.Text = "";

            }



        }
        private void GetWinningContender(PictureBox box)
        {
            //Determine winner picture box and pass value to contender class
            if (box.Left == _contender.RacetrackLength)
            {
                switch (Convert.ToString(box.Name)) //use picture box name value as case switch
                {
                    case "pictureBox1":
                        _contender.WinningContender = _myContenderList[_myUniqueContenderList[0]].ContenderName;
                        break;
                    case "pictureBox2":
                        _contender.WinningContender = _myContenderList[_myUniqueContenderList[1]].ContenderName;

                        break;
                    case "pictureBox3":
                        _contender.WinningContender = _myContenderList[_myUniqueContenderList[2]].ContenderName;

                        break;
                    case "pictureBox4":
                        _contender.WinningContender = _myContenderList[_myUniqueContenderList[3]].ContenderName;
                        break;
                }
                MessageBox.Show("The Winner is " + Environment.NewLine + _contender.WinningContender.ToUpper());


                //Changes how much punter has left after each race
                CalculatePunterBetTotal();

            }


        }

        private void ResetRace()
        {

            _myUniqueContenderList.Clear();

            _myBoxs = new[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };//add my pictureboxes to array 

            //reset boxes to starting point
            foreach (PictureBox pictureBox in _myBoxs)
            {
                pictureBox.Left = 12;
            }

            //Clear listbox results
            lbxResult.Items.Clear();
            lbxResult.ForeColor = Color.Black;

            //clear winner message
            lblWinner.Text = "";



        }


        //====================
        //  CONTENDERS
        //====================
        private void ContenderList() //Add contender names and photos to contender array from my factory class
        {
            //adding names  to contenders, using the factory class to create a new instance of contender
            string[] Names = new[] { "Wolf", "Horse", "Dog", "Donkey" };
            Bitmap[] BitmapImages = new[] { res.animal_3, res.animal_2, res.animal_4, res.animal_1 };
            for (int i = 0; i < _myContenderList.Length; i++)
            {
                //add my contenders through my factory class passing in name and bitmap
                _myContenderList[i] = Factory.CreateContender(Names[i], BitmapImages[i]);
            }

        }

        private void AddContendersToCombobox(List<int> pictureboxAssign) //assign names for beting option on checkboxes/comboxes
        {
            cbContenders.Items.Clear();

            foreach (var i in pictureboxAssign)
            {
                cbContenders.Items.Add(_myContenderList[i].ContenderName);
            }
        }

        //====================
        //  PUNTERS
        //====================

        public string[] AddPuntersToList()
        {
            string[] result = new string[3];  //array to use for my unit test
            for (int i = 0; i < _myPunters.Length; i++)
            {
                //used factory class to set punter details for all punters
                _myPunters[i] = Factory.GetPunterInfo(i);
                cbPunters.Items.Add(_myPunters[i].PunterName);
                result[i] = _myPunters[i].PunterName;
            }
            return result;
        }
        private void DisplayPunterDetails()
        {
            var myChosenPunter = _myPunters[cbPunters.SelectedIndex];

            lblProfile.ForeColor = Color.Black;
            lblProfile.Text = string.Format("Max bet {0:C}", myChosenPunter.CashOnHand);

            //load cash on hand into numericUpDown box for betting option
            numericUDBet.Maximum = Convert.ToDecimal(myChosenPunter.CashOnHand);
            numericUDBet.Increment = 5;
            //display the total the punter has to bet with
            numericUDBet.Value = Convert.ToDecimal(myChosenPunter.CashOnHand);

            if (myChosenPunter.CashOnHand <= 0)
            {
                lblProfile.Text = "BUSTED";
                lblProfile.ForeColor = Color.Red;
            }



        }
        public void CalculatePunterBetTotal()
        {
            var bustedCount = 0;
            for (int i = 0; i < _myPunters.Length; i++)
            {
                string result;
                //Show wether a punter has won or lost money on the race
                if (_contender.WinningContender == _myPunters[i].PunterContenderChoice)
                {
                    _myPunters[i].CashOnHand += _myPunters[i].BetPlaced;
                    result = "Won";
                }
                else
                {
                    _myPunters[i].CashOnHand -= _myPunters[i].BetPlaced;
                    result = "Lost";
                }
                //Shows busted if a punter has lost all his money
                if (_myPunters[i].CashOnHand > 0)
                {
                    lbxResult.Items.Add(string.Format("{0} {1} and now has {2:C}", _myPunters[i].Alias, result, _myPunters[i].CashOnHand));
                }
                else
                {
                    lbxResult.Items.Add("BUSTED");
                    bustedCount += 1;
                }
            }
            btnSet.Visible = true;
            btnRace.Visible = false;


            //check if game over when all punters have gone busted
            if (bustedCount >= 3)
            {
                MessageBox.Show("GAME OVER");
                panel1.Visible = false;
                btnSet.Text = "Reset";
            }
        }


    }

}

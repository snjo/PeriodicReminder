using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace PeriodicReminder
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        System.Timers.Timer blinkTimer2;
        bool acknowledgedReminder = true;
        List<Color> BlinkColors = new List<Color> { Color.Red, Color.Yellow, Color.Orange };
        int blinkCounter = 0;
        int blinkFrequency = 5000;

        //System.Timers.Timer debugTimer;
        //float minutes = 56f;
        //float resetMinutes = 60f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBlinkTimer();
            //StartTimer();

            //debugTimer = new System.Timers.Timer(minutes * 60000);
            //debugTimer.Elapsed += DebugTimerEvent;
            //debugTimer.AutoReset = true;
            //debugTimer.Enabled = true;
        }

        private void CreateBlinkTimer()
        {
            blinkTimer2 = new System.Timers.Timer(blinkFrequency);

            blinkTimer2.Elapsed += blinkTick;
            blinkTimer2.AutoReset = true;
            blinkTimer2.Enabled = true;
        }

        private void blinkTick(object sender, ElapsedEventArgs e)
        {
            if (acknowledgedReminder == false)
            {
                PerformedTaskButton.BackColor = BlinkColors[blinkCounter % BlinkColors.Count];
                //PerformedTaskButton.Text = "" + blinkCounter % BlinkColors.Count;
                blinkCounter++;
            }
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)numericUpDown1.Value * 60000;

            //timer.Interval = 5000;  //---------------------------------------TEST---------------------------------

            timer.Tick += new EventHandler(ElapsedTimer);
            //timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        private void DebugTimerEvent(object sender, ElapsedEventArgs e)
        {
            //label1.Text = "test";
        }

        private void ElapsedTimer(object sender, EventArgs e)
        {
            //PerformedTaskButton.BackColor = Color.Blue; // test
            //blinkTimer.Interval = blinkFrequency;
            //blinkTimer.Enabled = true;
            //blinkTimer.Start();
            timer.Interval = (int)numericUpDown2.Value *60000;
            timer.Enabled = true;
            timer.Start();
            acknowledgedReminder = false;
            System.Media.SystemSounds.Beep.Play();
            FlashWindow.Flash(this);
        }

        private void PerformedTask_Click(object sender, EventArgs e)
        {
            acknowledgedReminder = true;
            PerformedTaskButton.BackColor = Color.LightGray;            
        }

        /*private void label1_Click(object sender, EventArgs e)
        {

        }*/

        private void StartTimer_Click(object sender, EventArgs e)
        {
            StartTimer();
            PerformedTaskButton.BackColor = Color.White;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            //blinkTick();
        }
    }
}

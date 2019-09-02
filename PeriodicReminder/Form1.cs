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
        System.Timers.Timer timer;
        //System.Timers.Timer debugTimer;
        //float minutes = 56f;
        //float resetMinutes = 60f;

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //StartTimer();

            //debugTimer = new System.Timers.Timer(minutes * 60000);
            //debugTimer.Elapsed += DebugTimerEvent;
            //debugTimer.AutoReset = true;
            //debugTimer.Enabled = true;
        }

        private void StartTimer()
        {
            timer = new System.Timers.Timer((Double)numericUpDown1.Value * 60000d);
            timer.Elapsed += ElapsedTimer;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void DebugTimerEvent(object sender, ElapsedEventArgs e)
        {
            //label1.Text = "test";
        }

        private void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            PerformedTask.BackColor = Color.Red;
            timer.Interval = (Double)numericUpDown2.Value *60000d;
            timer.Enabled = true;
        }

        private void PerformedTask_Click(object sender, EventArgs e)
        {
            PerformedTask.BackColor = Color.LightGray;
        }

        /*private void label1_Click(object sender, EventArgs e)
        {

        }*/

        private void StartTimer_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

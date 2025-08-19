using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        //System.Timers.Timer blinkTimer2;
        bool acknowledgedReminder = true;
        List<Color> BlinkColors = new List<Color> { Color.Red, Color.Yellow, Color.Orange };
        List<Icon> BlinkIcons;
        int blinkCounter = 0;
        int blinkFrequency = 5000;
        string reminderText = "I did the thing";
        string windowTitle = "Periodic Reminder";
        string buttonTitle = "I did the thing";        
        DateTime nextAlert;
        Icon bellIcon1;
        Icon bellIcon2;

        //System.Timers.Timer debugTimer;
        //float minutes = 56f;
        //float resetMinutes = 60f;

        public Form1()
        {
            InitializeComponent();
            BlinkIcons = new List<Icon> { this.Icon, notifyIcon1.Icon };
            bool doStartTimer = false;
            Icon = notifyIcon1.Icon;

            if (Program.commandLineArguments.Length > 0)
            {
                reminderText = "";

                // handle command line arguments. any non-slashed text gets set as the button and window text.
                //  Examples:
                //  PeriodicReminder.exe /m 00 /start /i 120 Take your medication
                //  PeriodicReminder.exe Take your medication
                //  PeriodicReminder.exe Go to bed /t 23:30 /i 1440

                int arguments = Program.commandLineArguments.Length;
                for (int i = 0; i < arguments; i++)
                {
                    string newArgument = Program.commandLineArguments[i];

                    if (newArgument.Substring(0, 1) == "/")
                    {
                        if (i + 1 < arguments)
                        {
                            if (newArgument == "/m") //     /m - preform first task at the H:m mark, e.g. if it's 11:50, /m 34 happens at 12:34
                            {
                                dateTimePicker1.Value = DateTime.Today.AddHours(DateTime.Now.Hour);
                                dateTimePicker1.Value = dateTimePicker1.Value.AddMinutes(double.Parse(Program.commandLineArguments[i + 1]));
                                if (dateTimePicker1.Value < DateTime.Now)
                                {
                                    dateTimePicker1.Value = dateTimePicker1.Value.AddHours(1d);
                                }
                                i++;
                            }
                            else if (newArgument == "/t") //     /t - preform first task at the specified time
                            {
                                TimeSpan firstTime = TimeSpan.Parse(Program.commandLineArguments[i + 1]);
                                dateTimePicker1.Value = DateTime.Today.Add(firstTime);
                                if (dateTimePicker1.Value < DateTime.Now)
                                {
                                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1d);
                                }
                                i++;
                            }
                            else if (newArgument == "/i") //    /i - set interval between alerts
                            {
                                numericUpDown2.Value = decimal.Parse( Program.commandLineArguments[i + 1] );
                                i++;
                            }
                        }
                        if (newArgument == "/start") // auto start the timer when the program starts
                        {
                            doStartTimer = true;
                        }
                    }

                    else
                    {
                        reminderText += Program.commandLineArguments[i] + " ";
                    }
                }
                this.Text = reminderText + " - Periodic Reminder";
                buttonTitle = reminderText;                
            }
            
            PerformedTaskButton.Text = buttonTitle + "\n Reminder not set, click Start!";            


            if (doStartTimer) StartTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CreateBlinkTimer();
            //StartTimer();

            //debugTimer = new System.Timers.Timer(minutes * 60000);
            //debugTimer.Elapsed += DebugTimerEvent;
            //debugTimer.AutoReset = true;
            //debugTimer.Enabled = true;
        }

        //private void CreateBlinkTimer()
        //{
        //    blinkTimer2 = new System.Timers.Timer(blinkFrequency);
            
        //    blinkTimer2.Elapsed += blinkTick;
        //    blinkTimer2.AutoReset = true;
        //    blinkTimer2.Enabled = true;
        //}

        private void blinkTick(object sender, ElapsedEventArgs e)
        {
            if (acknowledgedReminder == false)
            {
                PerformedTaskButton.BackColor = BlinkColors[blinkCounter % BlinkColors.Count];
                this.Icon = BlinkIcons[blinkCounter % BlinkIcons.Count];
                //PerformedTaskButton.Text = "" + blinkCounter % BlinkColors.Count;
                blinkCounter++;
            }
        }

        private void StartTimer()
        {            
            timer = new System.Windows.Forms.Timer();
            //double nextTimeMS = (double)numericUpDown1.Value * 60000d;
            double nextTimeMS = dateTimePicker1.Value.Subtract(DateTime.Now).TotalMilliseconds;
            if (nextTimeMS < 1000d) nextTimeMS = 1000d;
            timer.Interval = (int)nextTimeMS;

            //timer.Interval = 5000;  //---------------------------------------TEST---------------------------------

            timer.Tick += new EventHandler(ElapsedTimer);
            //timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();

            nextAlert = System.DateTime.Now.AddMilliseconds(nextTimeMS);
            PerformedTaskButton.Text = buttonTitle + "\n \n Next: " + nextAlert.ToString("HH:mm\nyyyy-MM-dd");                     
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

            double nextTimeMS = (double)numericUpDown2.Value * 60000d;
            timer.Interval = (int)nextTimeMS;
            
            timer.Enabled = true;
            timer.Start();
            acknowledgedReminder = false;
            System.Media.SystemSounds.Beep.Play();
            FlashWindow.Flash(this);

            nextAlert = System.DateTime.Now.AddMilliseconds(nextTimeMS);
            PerformedTaskButton.Text = buttonTitle + "\n \n Next: " + nextAlert.ToString("HH:mm\nyyyy-MM-dd");
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
            blinkTick(sender,null);
        }
    }
}

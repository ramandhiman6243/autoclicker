using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        private AutoPlayer playThread;

        bool isRunning = false;

        int timerInterval = 50;

        Key startHotkey = Key.A;
        Key stopHotkey = Key.S;

        public Form1()
        {
            InitializeComponent();

            this.Text = "AutoClicker";

            initTimer();

            timer1.Enabled = true;
            timer1.Interval = timerInterval;
            timer1.Start();

            startHotkey = (Key)Enum.Parse(typeof(Key), Configuration.GetConfig().startHotkey);
            stopHotkey = (Key)Enum.Parse(typeof(Key), Configuration.GetConfig().stopHotkey);

            button1.BackColor = isRunning ? Color.Red : Color.LightGray;
            label2.Text = $"Click below button \nor press {startHotkey} to start and {stopHotkey} to stop.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switchstate();
        }

        void initTimer()
        {
            if (playThread == null)
                playThread = new AutoPlayer(Configuration.GetConfig());
        }

        void switchstate()
        {
            isRunning = !isRunning;
            button1.Text = isRunning ? "Stop" : "Start";
            button1.BackColor = isRunning ? Color.Red : Color.LightGray;

            if (!isRunning)
            {
                StopPlayTimer();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(stopHotkey))
            {
                if (isRunning)
                    switchstate();
            }
            else if (Keyboard.IsKeyDown(startHotkey))
            {
                if (!isRunning)
                    switchstate();
            }

            RecordTimer();

            if (isRunning)
                PlayTimer(timerInterval);
        }

        void RecordTimer()
        {
            var currentMousePos = MouseUtils.GetCursorPosition();
            label1.Text = currentMousePos.x + ", " + currentMousePos.y;
        }

        void PlayTimer(int interval)
        {
            initTimer();
            playThread.Play(interval);
        }

        void StopPlayTimer()
        {
            if (playThread != null)
                playThread.Reset();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreAudioApi;

namespace sleeper
{
    public partial class Form1 : Form
    {

        float oneStep = 0F;
        int tickCounter = 0;
        int tickMax = 0;
        MMDevice defaultDevice;

        public Form1()
        {
            InitializeComponent();
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            defaultDevice = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            label5.Text = (defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar*100).ToString() + "%";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                button1.Text = "Start";
                timer1.Enabled = false;
                numericUpDown1.Enabled = true;
            }
            else
            {
                button1.Text = "Stop";
                numericUpDown1.Enabled = false;
                float startVolume = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
                float finishVolume = startVolume / 2;
                oneStep = Math.Abs((finishVolume - startVolume) / (float)numericUpDown1.Value);
                tickMax = (int)numericUpDown1.Value;
                timer1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tickCounter >= tickMax)
            {
                button1.Text = "Start";
                timer1.Enabled = false;
                return;
            }
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar - oneStep;
            label3.Text = (numericUpDown1.Value - tickCounter).ToString();
            tickCounter++;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = numericUpDown1.Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

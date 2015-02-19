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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            defaultDevice = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            float startVolume = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
            float finishVolume = startVolume / 2;
            oneStep = Math.Abs((finishVolume - startVolume) / (float)numericUpDown1.Value);
            tickMax = (int)numericUpDown1.Value;
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tickCounter >= tickMax)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                timer1.Enabled = false;
                return;
            }
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar - oneStep;
            tickCounter++;
        }
    }
}

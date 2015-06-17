using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;
using Service;
using System.Runtime.CompilerServices;
using System.IO;

namespace WinFormHost
{
    public partial class Form1 : Form
    {
        private PictureBox[] _pictureBoxes;
        private SynchronizationContext _synchronizationContext = null;
        private int _index = 0;
        private ServiceHost _serviceHost = null;

        public Form1()
        {
            InitializeComponent();

            _pictureBoxes = new[] { 
                this.pictureBox11,this.pictureBox12,this.pictureBox13,this.pictureBox14,this.pictureBox15 ,
                this.pictureBox21,this.pictureBox22,this.pictureBox23,this.pictureBox24,this.pictureBox25 ,
                this.pictureBox31,this.pictureBox32,this.pictureBox33,this.pictureBox34,this.pictureBox35 ,
                this.pictureBox41,this.pictureBox42,this.pictureBox43,this.pictureBox44,this.pictureBox45 ,
                this.pictureBox51,this.pictureBox52,this.pictureBox53,this.pictureBox54,this.pictureBox55 
            };

            ImageAssembler.ImageSliceReceived += ImageAssembler_ImageSliceReceived;
            ImageAssembler.ImageErasing += ImageAssembler_ImageErasing;
            this.Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            _synchronizationContext = SynchronizationContext.Current;
            _serviceHost = new ServiceHost(typeof(ImageTransferService));
            _serviceHost.Open();
        }

        private void ImageAssembler_ImageErasing(object sender, EventArgs e)
        {
            _index = 0;
            foreach (var pictureBox in _pictureBoxes)
            {
                pictureBox.Image = null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ImageAssembler_ImageSliceReceived(object sender, ImageReceivedEventArgs e)
        {
            Bitmap bitmap = null;
            using (MemoryStream stream = new MemoryStream(e.ImageSlice))
            {
                bitmap = new Bitmap(stream);
                _synchronizationContext.Send(state => _pictureBoxes[_index++].Image = bitmap, null);
            }
        }
    }
}

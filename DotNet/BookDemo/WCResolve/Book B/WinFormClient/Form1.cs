using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        private string _imageSource = String.Empty;
        private IImageTransfer _nonReliableSessionProxy = null;
        private IImageTransfer _reliableSessionProxy = null;
        private IImageTransfer _orderDeliveryProxy = null;

        private ChannelFactory<IImageTransfer> _nonReliableSesscionFactory =
            new ChannelFactory<IImageTransfer>("nonReliableSession");

        private ChannelFactory<IImageTransfer> _reliableSessionFactory =
          new ChannelFactory<IImageTransfer>("reliableSession");

        private ChannelFactory<IImageTransfer> _orderDeliveryFactory =
          new ChannelFactory<IImageTransfer>("orderedDelivery");

        public Form1()
        {
            InitializeComponent();
            SubcribeForEvents();
        }

        private void SubcribeForEvents()
        {
            button_Browse.Click += button_Browse_Click;
            button_Send.Click += button_Send_Click;
        }

        void button_Send_Click(object sender, EventArgs e)
        {
            this.button_Send.Enabled = false;
            IList<byte[]> imageSlices = new List<byte[]>();
            Bitmap bmp = new Bitmap(this._imageSource);

            // 将图片横竖5等分
            double width = (double)bmp.Width / 5;
            double height = (double)bmp.Height / 5;

            // 拷贝像素
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Rectangle rect = new Rectangle(
                        Convert.ToInt32(x * width),
                        Convert.ToInt32(y * height),
                        Convert.ToInt32(width),
                        Convert.ToInt32(height));

                    byte[] data = BitmapToBytes(bmp.Clone(rect, PixelFormat.DontCare));
                    imageSlices.Add(data);
                }
            }
            IImageTransfer proxy = GetProxy();
            proxy.Erase();
            for (int i = 0; i < imageSlices.Count; i++)
            {
                proxy.Transfer(imageSlices[i]);
            }
            this.button_Send.Enabled = true;
        }

        void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _imageSource = openFileDialog.FileName;
                this.pictureBox1.Load(_imageSource);
            }
            this.button_Send.Enabled = true;
        }

        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                byte[] data = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(data, 0, Convert.ToInt32(ms.Length));
                return data;
            }
        }

        private IImageTransfer GetProxy()
        {
            if (null != _nonReliableSessionProxy)
            {
                (_nonReliableSessionProxy as ICommunicationObject).Close();
            }
            if (null != _reliableSessionProxy)
            {
                (_reliableSessionProxy as ICommunicationObject).Close();
            }
            if (null != _orderDeliveryProxy)
            {
                (_orderDeliveryProxy as ICommunicationObject).Close();
            }
            if (!this.checkBox_rs.Checked)
            {
                _nonReliableSessionProxy =
                    _nonReliableSesscionFactory.CreateChannel();
                (_nonReliableSessionProxy as ICommunicationObject).Open();
                return _nonReliableSessionProxy;
            }
            else if (!this.checkBox_od.Checked)
            {
                _reliableSessionProxy = _reliableSessionFactory.CreateChannel();
                (_reliableSessionProxy as ICommunicationObject).Open();
                return _reliableSessionProxy;
            }
            else
            {
                _orderDeliveryProxy = _orderDeliveryFactory.CreateChannel();
                (_orderDeliveryProxy as ICommunicationObject).Open();
                return _orderDeliveryProxy;
            }
        }
    }
}

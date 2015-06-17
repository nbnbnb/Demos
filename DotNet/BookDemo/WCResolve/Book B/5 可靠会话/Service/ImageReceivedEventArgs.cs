using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ImageReceivedEventArgs : EventArgs
    {
        public byte[] ImageSlice { get; private set; }

        public ImageReceivedEventArgs(byte[] imageSlice)
        {
            this.ImageSlice = imageSlice;
        }
    }
}

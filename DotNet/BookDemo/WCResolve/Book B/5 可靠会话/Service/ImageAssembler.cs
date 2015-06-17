using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Service
{
    public static class ImageAssembler
    {
        public static void ReceiveImageSlice(byte[] imageSlice)
        {
            var temp = Interlocked.CompareExchange(ref ImageSliceReceived, null, null);

            if (null != temp)
            {
                temp(null, new ImageReceivedEventArgs(imageSlice));
            }
        }

        public static void Erase()
        {
            var temp = Interlocked.CompareExchange(ref ImageErasing, null, null);

            if (null != temp)
            {
                temp(null, EventArgs.Empty);
            }
        }

        public static event EventHandler<ImageReceivedEventArgs> ImageSliceReceived;

        public static event EventHandler ImageErasing;
    }
}

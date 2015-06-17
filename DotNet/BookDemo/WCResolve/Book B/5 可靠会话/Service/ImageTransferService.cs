using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ImageTransferService : IImageTransfer
    {
        public void Transfer(byte[] imageSlice)
        {
            ImageAssembler.ReceiveImageSlice(imageSlice);
        }

        public void Erase()
        {
            ImageAssembler.Erase();
        }
    }
}

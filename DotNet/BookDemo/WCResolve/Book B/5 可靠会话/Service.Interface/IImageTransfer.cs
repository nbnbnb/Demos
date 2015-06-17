using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me/interface")]
    public interface IImageTransfer
    {
        [OperationContract(IsOneWay = true)]
        void Transfer(byte[] imageSlice);

        [OperationContract(IsOneWay = true)]
        void Erase();
    }
}

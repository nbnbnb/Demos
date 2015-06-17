using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me")]
    public interface IFileReader
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginReader(string fileName, AsyncCallback callback, object stateObject);
        string EndReader(IAsyncResult asynResult);
    }
}

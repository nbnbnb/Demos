using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    [ServiceContract(Name = "IHttpGetMetadata",
        Namespace = "http://www.zhangjin.me/interface")]
    public interface IHttpGetMetadata
    {
        // 此方法处理任何形式的消息请求
        // 因为它的输入参数和返回类型均为 Message
        // 并且 Action 和 ReplyAction 为 *
        [OperationContract(Action = "*", ReplyAction = "*")]
        Message Get(Message message);
    }
}

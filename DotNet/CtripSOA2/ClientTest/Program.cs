using Car.OAT.Client;
using ConsoleHelloWorldClient.Client;
using CServiceStack.Common.Types;
using GSA.Settlement.Api.Client;
using GSA.Settlement.SettlementProcess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_HelloService_WebClient();
            //Test_HelloService_HttpWebRequest();
            Console.ReadKey();
        }

        #region Test
        private static void Test_SettlementOpenAPI()
        {
            var webHost = "http://ws.settlement.ttd.fat6.qa.nt.ctripcorp.com/ticket-settlement-openapi/api";

            var client = SettlementOpenAPIClient.GetInstance(webHost);

            var response = client.SettlementApply(new GSA.Settlement.Api.Client.SettlementApplyRequestType
            {
                BussinessId = "1",
                Cost = 23,
                Currency = "USD",
                CustomerName = "测测cvs吃",
                EndDate = DateTime.Now,
                OrderDate = DateTime.Now.AddDays(4),
                OrderId = 12345,
                PayMode = "M",
                Price = 122,
                ProviderId = 9000,
                PurchaseOrderId = 10000,
                Quantity = 10,
                UnitQuantity = 111,
                UseDate = DateTime.Now.AddYears(1)
            });

            Console.WriteLine("Error : {0}", response.ErrorMessage);
        }

        private static void Test_SettlementService()
        {
            var webHost = "http://ws.settlement.ttd.fat6.qa.nt.ctripcorp.com/ticket-settlement-service/api";

            var client = SettlementServiceClient.GetInstance(webHost);

            var response = client.SettlementApply(new GSA.Settlement.SettlementProcess.Client.SettlementApplyRequestType
            {
                BussinessId = "1",
                Cost = 23,
                Currency = "USD",
                CustomerName = "测测cvs吃",
                EndDate = DateTime.Now,
                OrderDate = DateTime.Now.AddDays(4),
                OrderId = 12345,
                PayMode = "M",
                Price = 122,
                ProviderId = 9000,
                PurchaseOrderId = 10000,
                Quantity = 10,
                UnitQuantity = 111,
                UseDate = DateTime.Now.AddYears(1)
            });

            Console.WriteLine("Error: " + response.ErrorMessage);

        }

        private static void Test_HelloService()
        {
            HelloWorldServiceClient client = HelloWorldServiceClient.GetInstance("http://localhost:10000");
            var res = client.Hello(new HelloRequestType
            {
                EndDate = DateTime.Now,
                OrderId = 123456,
                Price = 99.99m,
                UnitQuantity = 4
            });
            Console.WriteLine("Error " + res.ErrorMessage);
        }

        private static void Test_HelloService_WebClient()
        {
            Console.WriteLine("-----");
            WebClient client = new WebClient();
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(data);
            client.Headers.Set(HttpRequestHeader.ContentType, "application/xml; charset=utf-8");
            string url = "http://www.zhangjin.me:9636/Hello.xml";  // IIS
            //string url = "http://localhost:59127/Hello.xml";  // IIS Express
            byte[] resBuf = client.UploadData(url, buf);
            string res_1 = Encoding.UTF8.GetString(resBuf, 0, resBuf.Length);
            string res_2 = String.Empty;
            Console.WriteLine(res_1);  // 头 3 个字节为 BOM 头，输出时将会显示为"?"
            Console.WriteLine("-----");
            using (MemoryStream ms = new MemoryStream(resBuf))
            {
                using (StreamReader sr = new StreamReader(ms))  // 让 StreamReader 来自动处理 BOM 头
                {
                    res_2 = sr.ReadToEnd();
                    Console.WriteLine(res_2);
                }
            }
            Console.WriteLine(res_1 == res_2);
            Console.WriteLine(res_1.Length);
            Console.WriteLine(res_2.Length);
        }

        private static void Test_HelloService_HttpWebRequest()
        {
            // 发送 HTTP 底层请求
            Console.WriteLine("-----");
            string url = "http://www.zhangjin.me:9636/Hello.xml";  // IIS
            //string url = "http://localhost:59127/Hello.xml";  // IIS Express
            WebRequest request = HttpWebRequest.Create(url);
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buf.Length;  // 这些参数都必须设置
            request.Method = "POST"; // 这些参数都必须设置
            request.ContentType = "application/xml; charset=utf-8";
            using (Stream stream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    int size = 100;
                    byte[] btking = new byte[size];
                    List<byte> res = new List<byte>();
                    int endsize = 0;
                    while ((endsize = stream.Read(btking, 0, size)) == size)
                    {
                        res.AddRange(btking);
                    }
                    byte[] endbuf = new byte[endsize];
                    Array.Copy(btking, endbuf, endsize);
                    res.AddRange(endbuf);
                    Console.WriteLine(Encoding.UTF8.GetString(res.ToArray()));

                    /*
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                    */
                }
            }
        }

        private static void Test_HelloService_HttpWebRequest_Proxy()
        {
            /*
            IsBypassed 方法用于确定在访问 Internet 资源时是否不使用代理服务器。
            BypassProxyOnLocal 和 BypassList 属性控制 IsBypassed 方法的返回值。
            在以下任何条件下，IsBypassed 均返回 true：
            如果 BypassProxyOnLocal 为 true 且 host 是本地 URI。通过在 URI 中省略句点(.) 来标识本地请求，如“http://webserver/”。
                        如果 host 与 BypassList 中的正则表达式匹配。
            如果 Address 为 空引用（在 Visual Basic 中为 Nothing）。
            所有其他条件返回 false。
            */
            Console.WriteLine("-----");
            string url = "http://www.zhangjin.me:9636/Hello.xml";  // IIS
            //string url = "http://localhost:59127/Hello.xml";  // IIS Express
            Uri uri = new Uri(url);
            //WebProxy proxy = new WebProxy("http://localhost:8888", false); // Fiddler 会自动添加一个 8888 端口的本地代理
            //WebRequest.DefaultWebProxy = proxy; // 后续创建的 WebRequest 对象都会使用这个代理
            //Console.WriteLine(proxy.IsBypassed(uri)); // 用来判断请求是否使用代理，只有返回 False 的请求才会使用代理；所以 URL 一定要设置为非本地的才会被 Fiddler 捕获到
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buf.Length;  // 这些参数都必须设置
            request.Method = "POST"; // 这些参数都必须设置
            request.ContentType = "application/xml; charset=utf-8";
            using (Stream stream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
            }
        }

        private static void Test_HelloService_HttpClient()
        {
            Console.WriteLine("-----");
            HttpClient client = new HttpClient();
            string url = "http://www.zhangjin.me:9636/Hello.xml";  // IIS
            //string url = "http://localhost:59127/Hello.xml";  // IIS Express
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = Encoding.UTF8.GetBytes(data);
            StringContent conent = new StringContent(data);
            Task<HttpResponseMessage> message = client.PostAsync(url, conent);
            message.Wait();
            HttpResponseMessage result = message.Result;
            Task<Stream> btking = result.Content.ReadAsStreamAsync();
            btking.Wait();
            using (Stream stream = btking.Result)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }
        #endregion
    }
}

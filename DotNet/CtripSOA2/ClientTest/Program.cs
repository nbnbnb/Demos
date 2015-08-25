using Car.OAT.Client;
using ConsoleHelloWorldClient.Client;
using CServiceStack.Common.Types;
using CTI.Email.CommonService.Entity;
using Ctrip.SOA.Comm;
using Ctrip.SOA.Comm.Agent;
using Ctrip.SOA.Comm.Cache;
using Ctrip.SOA.Comm.Entity;
using Ctrip.SOA.Comm.Log;
using Ctrip.SOA.Comm.Metrics;
using Ctrip.SOA.Comm.Utility;
using Freeway.Tracing;
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
using System.Xml;
using System.Timers;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimpleRequest();
            //SimpleRequest();
            //Test_HelloService_WebClient();
            //Test_HelloService_HttpWebRequest();
            //Test_HelloService_HttpClient();

            Test_SOA_1();
            Console.ReadKey();
        }
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
            WebProxy proxy = new WebProxy("http://127.0.0.1:8888", false); // 设置 Fiddler 的代理
            proxy.BypassProxyOnLocal = false;
            WebRequest.DefaultWebProxy = proxy;
            client.Proxy = proxy;
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(data);
            client.Headers.Set(HttpRequestHeader.ContentType, "application/xml; charset=utf-8");
            string url = "http://localhost:9636/Hello.xml";  // IIS

            byte[] resBuf = client.UploadData(url, buf);

            Console.WriteLine(Encoding.UTF8.GetString(resBuf));
        }

        private static void Test_HelloService_HttpWebRequest()
        {
            // 这个发送 HTTP 请求最底
            Console.WriteLine("-----");
            string url = "http://localhost:9636/Hello.xml";  // IIS
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
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string url = "http://localhost:9636/Hello.xml";  // IIS

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

        private static void Test_SOA_1()
        {
            var mailRequest = new NewMailRequest();
            mailRequest.SendCode = "12000";
            mailRequest.Importance = "1"; //优先级
            mailRequest.SourceID = 0;
            mailRequest.Uid = "E00025341";
            mailRequest.Recipient = "jinzhanga@ctrip.com";
            mailRequest.RecipientName = "ZJ";
            mailRequest.Cc = "";
            mailRequest.Bcc = "";
            mailRequest.Sender = "vip@ctrip.com"; //发送人地址必须为邮箱地址格式
            mailRequest.SenderName = "用车订单系统";
            mailRequest.DeadlineTime = DateTime.Now.AddHours(1);
            mailRequest.Charset = "gb2312";
            mailRequest.ContentType = "text/html";
            mailRequest.BodyTemplateID = 4;//邮件模板ID
            mailRequest.Subject = " 请尽快答复订单号(No.1705594457)供应商单号(),请核实供应商是否有订单";

            var request = new Request
            {
                Header = new RequestHead
                {
                    UserID = "E00025341",
                    RequestType = "CTI.EMail.CommonService.SendNewMail",
                    Environment = "fws"
                },
                RequestBody = mailRequest
            };

            string requestXml= XMLSerializer.Serialize(request);

            string responseXml = WSAgent.Request(requestXml);

        }

    }
}

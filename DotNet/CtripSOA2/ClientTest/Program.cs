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
            //SimpleRequest();
            //SimpleRequest();
            Test_HelloService_WebClient();
            Test_HelloService_HttpWebRequest();
            Test_HelloService_HttpClient();
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
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(data);
            client.Headers.Set(HttpRequestHeader.ContentType, "application/xml; charset=utf-8");
            string url = "http://localhost:9636/Hello.xml";  // IIS
            byte[] resBuf = client.UploadData(url, "POST", buf);
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
            HttpClient client = new HttpClient();
            string url = "http://localhost:9636/Hello.xml";  // IIS

            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><HelloRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://soa.ctrip.com/thingstodo/order/settlementopenapi/v1""><OrderId>123456</OrderId><UnitQuantity>4</UnitQuantity><EndDate>2015-08-04T17:56:43.5959493+08:00</EndDate><Price>99.99</Price></HelloRequest>";
            byte[] buf = Encoding.UTF8.GetBytes(data);
            StringContent conent = new StringContent(data);
            Task<HttpResponseMessage> message = client.PostAsync(url, conent);
            message.Wait();
            HttpResponseMessage result = message.Result;
            Task<Stream> btking = result.Content.ReadAsStreamAsync();
            btking.Wait();
            using (Stream stream=btking.Result)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }
    }
}

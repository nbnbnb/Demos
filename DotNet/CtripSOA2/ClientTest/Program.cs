using Car.OAT.Client;
using CServiceStack.Common.Types;
using GSA.Settlement.Api.Client;
using GSA.Settlement.SettlementProcess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CarOTATest();

            Console.ReadKey();
        }
        private static void Test_OpenAPI()
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

        private static void Test_Service()
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

        private static void LocalTest()
        {
            var webHost = "http://localhost/api";

            var client = SettlementOpenAPIClient.GetInstance(webHost);

            var response = client.SettlementApply(new GSA.Settlement.Api.Client.SettlementApplyRequestType
            {
                BussinessId = "1",
                Cost = 23,
                Currency = "KKKING",
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

        private static void CarOTATest()
        {
            OATOrderServiceClient client = OATOrderServiceClient.GetInstance("http://localhost:9598");
            SendOrderRequestType request = new SendOrderRequestType
            {
                OrderID = 1705586935                  
            };
            client.SendOrder(request);
        }
        
    }
}

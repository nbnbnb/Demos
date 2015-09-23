using Car.Settlement.SettlementProcess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    public static class CarSettlementServiceTest
    {
        public static void Test_Car_SettlementService_Local()
        {
            var webHost = "http://localhost/settlementservice/api";

            var client = SettlementServiceClient.GetInstance(webHost);

            var request = new SettlementApplyRequestType()
            {
                SettlementApplyDetailInfoList = new List<SettlementApplyDetailInfo>()
            };

            request.SettlementApplyDetailInfoList.Add(new SettlementApplyDetailInfo
            {
                BussinessId = "1",
                Cost = 12.34m,
                CtripOrderId = "0245",
                Currency = "RMB",
                CustomerName = "For Test User",
                EndDate = DateTime.Now.AddMonths(1),
                OrderDate = DateTime.Now.AddMonths(-1),
                OrderId = 123456789,
                OrderItemId = 987654321,
                PayMode = "P",
                Price = 20.50m,
                ProviderId = 9787, // 一嗨租车
                PurchaseOrderId = 8888,
                Quantity = 1,
                ResourceName = "Test Resource",
                ThirdPartId = "AAAABBB",
                UnitQuantity = 111,
                UseDate = DateTime.Now.AddDays(5)
            });


            var response = client.SettlementApply(request);

            Console.WriteLine("Error : {0}", response.ErrorMessage);
        }

        public static void Test_Car_SettlementService_FAT()
        {
            string webHost = "http://ws.order.car.fat75.qa.nt.ctripcorp.com/settlementservice/api";

            var client = SettlementServiceClient.GetInstance();

            var request = new SettlementApplyRequestType()
            {
                SettlementApplyDetailInfoList = new List<SettlementApplyDetailInfo>()
            };

            request.SettlementApplyDetailInfoList.Add(new SettlementApplyDetailInfo
            {
                BussinessId = "1",
                Cost = 12.34m,
                CtripOrderId = "0245",
                Currency = "RMB",
                CustomerName = "For Test User",
                EndDate = DateTime.Now.AddMonths(1),
                OrderDate = DateTime.Now.AddMonths(-1),
                OrderId = 123456789,
                OrderItemId = 987654321,
                PayMode = "P",
                Price = 20.50m,
                ProviderId = 9787, // 一嗨租车
                PurchaseOrderId = 8888,
                Quantity = 1,
                ResourceName = "Test Resource",
                ThirdPartId = "AAAABBB",
                UnitQuantity = 111,
                UseDate = DateTime.Now.AddDays(5)
            });

            var response = client.SettlementApply(request);

            Console.WriteLine("Error : {0}", response.ErrorMessage);
        }
    }
}

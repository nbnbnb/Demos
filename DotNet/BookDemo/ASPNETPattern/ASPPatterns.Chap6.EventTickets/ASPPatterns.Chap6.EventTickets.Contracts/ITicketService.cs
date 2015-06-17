using ASPPatterns.Chap6.EventTickets.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ASPPatterns.Chap6.EventTickets.Contracts
{
    [ServiceContract(Namespace = "ASPPatterns.Chap6.EventTickets/")]
    public interface ITicketService
    {
        [OperationContract()]
        ReserveTicketResponse ReserveTicket(ReserveTicketRequest reserveTicketRequest);

        [OperationContract()]
        PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest PurchaseTicketRequest);
    }
}

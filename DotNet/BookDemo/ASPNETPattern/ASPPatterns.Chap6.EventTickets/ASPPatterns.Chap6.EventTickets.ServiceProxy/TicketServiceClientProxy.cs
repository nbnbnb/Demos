using ASPPatterns.Chap6.EventTickets.Contracts;
using ASPPatterns.Chap6.EventTickets.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ASPPatterns.Chap6.EventTickets.ServiceProxy
{
    public class TicketServiceClientProxy : ClientBase<ITicketService>, ITicketService
    {
        public ReserveTicketResponse ReserveTicket(ReserveTicketRequest reserveTicketRequest)
        {
            return base.Channel.ReserveTicket(reserveTicketRequest);
        }
        public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest)
        {
            return base.Channel.PurchaseTicket(purchaseTicketRequest);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap6.EventTickets.Model
{
    public class TicketReservationFactory
    {
        public static TicketReservation CreateReservation(Event @event, int tktQty)
        {
            TicketReservation reservation = new TicketReservation();
            reservation.Id = Guid.NewGuid();
            reservation.Event = @event;
            reservation.ExpiryTime = DateTime.Now.AddMinutes(1);
            reservation.TicketQuantity = tktQty;
            return reservation;
        }
    }
}
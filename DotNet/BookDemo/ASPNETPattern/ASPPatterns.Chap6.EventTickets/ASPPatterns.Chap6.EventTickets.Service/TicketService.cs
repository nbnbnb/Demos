using ASPPatterns.Chap6.EventTickets.Contracts;
using ASPPatterns.Chap6.EventTickets.DataContract;
using ASPPatterns.Chap6.EventTickets.Model;
using ASPPatterns.Chap6.EventTickets.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;

namespace ASPPatterns.Chap6.EventTickets.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TicketService : ITicketService
    {
        private IEventRepository _eventRepository;
        private static MessageResponseHistory<PurchaseTicketResponse>
            _reservationResponse = new MessageResponseHistory<PurchaseTicketResponse>();
        public TicketService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public TicketService()
            : this(new EventRepository())
        { }
        public ReserveTicketResponse ReserveTicket(ReserveTicketRequest reserveTicketRequest)
        {
            ReserveTicketResponse response = new ReserveTicketResponse();
            try
            {
                Event Event = _eventRepository.FindBy(new Guid(reserveTicketRequest.EventId));
                TicketReservation reservation;
                if (Event.CanReserveTicket(reserveTicketRequest.TicketQuantity))
                {
                    reservation = Event.ReserveTicket(reserveTicketRequest.TicketQuantity);
                    _eventRepository.Save(Event);
                    response = reservation.ConvertToReserveTicketResponse();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = String.Format("There are {0} ticket(s) available.", Event.AvailableAllocation());
                }
            }
            catch (Exception ex)
            {
                // Shield exceptions
                response.Message = ErrorLog.GenerateErrorRefMessageAndLog(ex);
                response.Success = false;
            }
            return response;
        }
        public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest)
        {
            PurchaseTicketResponse response = new PurchaseTicketResponse();
            try
            {
                // Check for a duplicate transaction using the Idempotent pattern;
                // the Domain Logic could cope but you can’t besure.
                if (_reservationResponse.IsAUniqueRequest(purchaseTicketRequest.CorrelationId))
                {
                    TicketPurchase ticket;
                    Event Event = _eventRepository.FindBy(new Guid(purchaseTicketRequest.EventId));
                    if (Event.CanPurchaseTicketWith(new Guid(purchaseTicketRequest.ReservationId)))
                    {
                        ticket = Event.PurchaseTicketWith(
                        new Guid(purchaseTicketRequest.ReservationId));
                        _eventRepository.Save(Event);
                        response = ticket.ConvertToPurchaseTicketResponse();
                        response.Success = true;
                    }
                    else
                    {
                        response.Message =
                        Event.DetermineWhyATicketCannotbePurchasedWith(
                        new Guid(purchaseTicketRequest.ReservationId));
                        response.Success = false;
                    }
                    _reservationResponse.LogResponse(purchaseTicketRequest.CorrelationId, response);
                }
                else
                {
                    response = _reservationResponse.RetrievePreviousResponseFor(purchaseTicketRequest.CorrelationId);
                }
            }
            catch (Exception ex)
            {
                // Shield exceptions
                response.Message = ErrorLog.GenerateErrorRefMessageAndLog(ex);
                response.Success = false;
            }
            return response;
        }
    }
}


using System;

namespace TicketsApps.Models
{
    public class TicketModel : ClientModel
    {
        public int TicketNumber { get; set; }
        public DateTime TicketDate { get; set; }
    }
}

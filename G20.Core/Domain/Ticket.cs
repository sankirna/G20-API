using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class Ticket : BaseEntityWithTacking
    {
        public int ProductId { get; set; }
        public int TicketCategoryId { get; set; }
        public int TicketsIssue { get; set; }
        public int Available { get; set; }
        public int Blocked { get; set; }
        public int Sold { get; set; }
        public decimal TicketPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial  class StandCategory : BaseEntityWithTacking
    {
        public string StandName{ get; set; }
        public int VenueId { get; set; }
        public int Capacity { get; set; }
        public string EntryGate { get; set; }
        public string Facilities { get; set; }
    }
}

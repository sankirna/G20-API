using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class Product : BaseEntityWithTacking
    {
        public int ProductTypeId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int VenueId { get; set; }
        public DateTime MatchDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Description { get; set; }

        public int? FileId { get; set; }
        public virtual File? File { get; set; }
    }
}

using G20.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Core.Domain
{
    public partial class Product : BaseEntityWithTacking
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? VenueId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? ScheduleDateTime { get; set; }
        public string Description { get; set; }
        public int? FileId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Team? Team1 { get; set; }
        public virtual Team? Team2 { get; set; }
        public virtual Venue? Venue { get; set; }
        public virtual File? File { get; set; }
    }
}


namespace G20.Core.Domain
{
    public partial class Venues : BaseEntity
    {
        public string StadiumName { get; set; } 
        public string Location { get; set; }
        public string Country { get; set; }
        public string Capacity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual AspNetUser? UpdatedByNavigation { get; set; }
    }
}

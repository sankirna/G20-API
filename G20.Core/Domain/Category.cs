
namespace G20.Core.Domain
{
    public partial class Category : BaseEntity
    {
        public string Name { get; set; } 
        public string Description  { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual AspNetUser? UpdatedByNavigation { get; set; }
    }
}

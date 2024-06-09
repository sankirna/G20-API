
namespace Matrimony.Core.Domain
{
    public partial class ProfileFile : BaseEntity
    {
        public int ProfileId { get; set; }

        public int FileId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual AspNetUser? CreatedByNavigation { get; set; }

        public virtual File File { get; set; } = null!;

        public virtual AspNetUser? UpdatedByNavigation { get; set; }
    }
}

namespace G20.Core.Domain
{
    public partial class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User? User { get; set; }
        public virtual User? Role { get; set; }
    }
}

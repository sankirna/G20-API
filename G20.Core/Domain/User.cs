
namespace G20.Core.Domain
{
    public partial class User : BaseEntityWithTacking
    {
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}

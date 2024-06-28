using G20.Framework.Models;

namespace G20.API.Models.Users
{
    public partial record UserModel : BaseNopEntityModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        //public int UserTypeId { get; set; }
        public List<int> RoleIds { get; set; }

    }
}

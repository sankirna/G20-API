using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.EmailAccounts
{
    public partial record EmailAccountModel : BaseNopEntityModel
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public int MaxNumberOfEmails { get; set; }
        public int EmailAuthenticationMethodId { get; set; }
        public EmailAuthenticationMethod EmailAuthenticationMethod
        {
            get => (EmailAuthenticationMethod)EmailAuthenticationMethodId;
            set => EmailAuthenticationMethodId = (int)value;
        }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}

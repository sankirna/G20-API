using FluentValidation;
using G20.API.Models.EmailAccounts;

namespace G20.API.Validators.EmailAccounts
{
    public class EmailAccountModelValidator : AbstractValidator<EmailAccountModel>
    {
        public EmailAccountModelValidator() { }
    }
}

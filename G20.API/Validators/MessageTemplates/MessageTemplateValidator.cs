using FluentValidation;
using G20.API.Models.EmailAccounts;
using G20.API.Models.MessageTemplates;

namespace G20.API.Validators.MessageTemplates
{
    public class MessageTemplateValidator : AbstractValidator<MessageTemplateModel>
    {
        public MessageTemplateValidator() { }
    }
}


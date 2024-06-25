using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.EmailAccounts;
using G20.Service.Messages;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.EmailAccounts
{
    public class EmailAccountFactoryModel : IEmailAccountFactoryModel
    {
        protected readonly IEmailAccountService _emailAccountService;

        public EmailAccountFactoryModel(IEmailAccountService emailAccountService)
        {
            _emailAccountService = emailAccountService;
        }

        public virtual async Task<EmailAccountListModel> PrepareEmailAccountListModelAsync(EmailAccountSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var emailAccounts = await _emailAccountService.GetEmailAccountsAsync(email: searchModel.Email,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new EmailAccountListModel().PrepareToGridAsync(searchModel, emailAccounts, () =>
            {
                return emailAccounts.SelectAwait(async emailAccount =>
                {
                    var emailAccountModel = emailAccount.ToModel<EmailAccountModel>();
                    return emailAccountModel;
                });
            });

            return model;
        }
    }
}

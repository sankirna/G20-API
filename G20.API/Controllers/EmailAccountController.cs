using G20.API.Factories.EmailAccounts;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.EmailAccounts;
using G20.Core.Domain;
using G20.Service.Messages;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class EmailAccountController : BaseController
    {
        protected readonly IEmailAccountFactoryModel _emailAccountFactoryModel;
        protected readonly IEmailAccountService _emailAccountService;

        public EmailAccountController(IEmailAccountFactoryModel emailAccountFactoryModel, IEmailAccountService emailAccountService)
        {
            _emailAccountFactoryModel = emailAccountFactoryModel;
            _emailAccountService = emailAccountService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(EmailAccountSearchModel searchModel)
        {
            var model = await _emailAccountFactoryModel.PrepareEmailAccountListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var emailAccount = await _emailAccountService.GetEmailAccountByIdAsync(id);
            if (emailAccount == null)
                return Error("not found");
            return Success(emailAccount.ToModel<EmailAccountModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(EmailAccountModel model)
        {
            var emailAccount = model.ToEntity<EmailAccount>();
            await _emailAccountService.InsertEmailAccountAsync(emailAccount);
            return Success(emailAccount.ToModel<EmailAccountModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(EmailAccountModel model)
        {
            var emailAccount = await _emailAccountService.GetEmailAccountByIdAsync(model.Id);
            if (emailAccount == null)
                return Error("not found");

            emailAccount = model.ToEntity(emailAccount);
            await _emailAccountService.UpdateEmailAccountAsync(emailAccount);
            return Success(emailAccount.ToModel<EmailAccountModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var emailAccount = await _emailAccountService.GetEmailAccountByIdAsync(id);
            if (emailAccount == null)
                return Error("not found");
            await _emailAccountService.DeleteEmailAccountAsync(emailAccount);
            return Success(id);
        }
    }
}

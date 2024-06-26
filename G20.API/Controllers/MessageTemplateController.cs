using G20.API.Models.MessageTemplates;
using G20.API.Factories.MessageTemplates;
using G20.Core;
using G20.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.Service.Messages;

namespace G20.API.Controllers
{
    public class MessageTemplateController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IMessageTemplateFactoryModel _messageTemplateFactoryModel;
        protected readonly IMessageTemplateService _messageTemplateService;

        public MessageTemplateController(IWorkContext workContext,
            IMessageTemplateFactoryModel messageTemplateFactoryModel,
            IMessageTemplateService messageTemplateService)
        {
            _workContext = workContext;
            _messageTemplateFactoryModel = messageTemplateFactoryModel;
            _messageTemplateService = messageTemplateService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(MessageTemplateSearchModel searchModel)
        {
            var model = await _messageTemplateFactoryModel.PrepareMessageTemplateListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var messageTemplate = await _messageTemplateService.GetMessageTemplateByIdAsync(id);
            if (messageTemplate == null)
                return Error("not found");
            return Success(messageTemplate.ToModel<MessageTemplateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(MessageTemplateModel model)
        {
            var messageTemplate = model.ToEntity<MessageTemplate>();
            await _messageTemplateService.InsertMessageTemplateAsync(messageTemplate);
            return Success(messageTemplate.ToModel<MessageTemplateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(MessageTemplateModel model)
        {
            var messageTemplate = await _messageTemplateService.GetMessageTemplateByIdAsync(model.Id);
            if (messageTemplate == null)
                return Error("not found");

            messageTemplate = model.ToEntity(messageTemplate);
            await _messageTemplateService.UpdateMessageTemplateAsync(messageTemplate);
            return Success(messageTemplate.ToModel<MessageTemplateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var messageTemplate = await _messageTemplateService.GetMessageTemplateByIdAsync(id);
            if (messageTemplate == null)
                return Error("not found");
            await _messageTemplateService.DeleteMessageTemplateAsync(messageTemplate);
            return Success(id);
        }
    }
}

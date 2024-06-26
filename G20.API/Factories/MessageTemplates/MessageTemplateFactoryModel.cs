using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.MessageTemplates;
using G20.Service.Messages;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.MessageTemplates
{
    public class MessageTemplateFactoryModel : IMessageTemplateFactoryModel
    {
        protected readonly IMessageTemplateService _messageTemplateService;
        public MessageTemplateFactoryModel(IMessageTemplateService messageTemplateService)
        {
            _messageTemplateService = messageTemplateService;
        }

        public virtual async Task<MessageTemplateListModel> PrepareMessageTemplateListModelAsync(MessageTemplateSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var messageTemplates = await _messageTemplateService.GetMessageTemplatesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new MessageTemplateListModel().PrepareToGridAsync(searchModel, messageTemplates, () =>
            {
                return messageTemplates.SelectAwait(async messageTemplate =>
                {
                    var messageTemplateModel = messageTemplate.ToModel<MessageTemplateModel>();
                    return messageTemplateModel;
                });
            });

            return model;
        }
    }
}

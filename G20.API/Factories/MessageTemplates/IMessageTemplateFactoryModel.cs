using G20.API.Models.MessageTemplates;

namespace G20.API.Factories.MessageTemplates
{
    public interface IMessageTemplateFactoryModel
    {
        Task<MessageTemplateListModel> PrepareMessageTemplateListModelAsync(MessageTemplateSearchModel searchModel);
    }
}

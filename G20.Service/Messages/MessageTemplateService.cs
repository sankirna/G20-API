using G20.Core.Domain;
using G20.Data;

namespace G20.Service.Messages;

/// <summary>
/// Message template service
/// </summary>
public partial class MessageTemplateService : IMessageTemplateService
{
    #region Fields

    protected readonly IRepository<MessageTemplate> _messageTemplateRepository;

    #endregion

    #region Ctor

    public MessageTemplateService(
        
        IRepository<MessageTemplate> messageTemplateRepository)
    {
        _messageTemplateRepository = messageTemplateRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Delete a message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task DeleteMessageTemplateAsync(MessageTemplate messageTemplate)
    {
        await _messageTemplateRepository.DeleteAsync(messageTemplate);
    }

    /// <summary>
    /// Inserts a message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task InsertMessageTemplateAsync(MessageTemplate messageTemplate)
    {
        await _messageTemplateRepository.InsertAsync(messageTemplate);
    }

    /// <summary>
    /// Updates a message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task UpdateMessageTemplateAsync(MessageTemplate messageTemplate)
    {
        await _messageTemplateRepository.UpdateAsync(messageTemplate);
    }

    /// <summary>
    /// Gets a message template
    /// </summary>
    /// <param name="messageTemplateId">Message template identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the message template
    /// </returns>
    public virtual async Task<MessageTemplate> GetMessageTemplateByIdAsync(int messageTemplateId)
    {
        return await _messageTemplateRepository.GetByIdAsync(messageTemplateId);
    }

    /// <summary>
    /// Gets message templates by the name
    /// </summary>
    /// <param name="messageTemplateName">Message template name</param>
    /// <param name="storeId">Store identifier; pass null to load all records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of message templates
    /// </returns>
    public virtual async Task<IList<MessageTemplate>> GetMessageTemplatesByNameAsync(string messageTemplateName, int? storeId = null)
    {
        if (string.IsNullOrWhiteSpace(messageTemplateName))
            throw new ArgumentException(nameof(messageTemplateName));

        //get message templates with the passed name
        var templates = await _messageTemplateRepository.Table
            .Where(messageTemplate => messageTemplate.Name.Equals(messageTemplateName))
            .OrderBy(messageTemplate => messageTemplate.Id)
            .ToListAsync();

       
        return templates;
    }

    /// <summary>
    /// Gets all message templates
    /// </summary>
    /// <param name="storeId">Store identifier; pass 0 to load all records</param>
    /// <param name="keywords">Keywords to search by name, body, or subject</param>
    /// <param name="isActive">A value indicating whether to get active records; "null" to load all records; "false" to load only inactive records; "true" to load only active records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the message template list
    /// </returns>
    public virtual async Task<IList<MessageTemplate>> GetAllMessageTemplatesAsync(int storeId, string keywords = null, bool? isActive = null)
    {
        var messageTemplates = await _messageTemplateRepository.GetAllAsync(async query =>
        {

            if (isActive.HasValue)
                query = query.Where(mt => mt.IsActive == isActive);

            return query.OrderBy(t => t.Name);
        });

        if (!string.IsNullOrWhiteSpace(keywords))
        {
            messageTemplates = messageTemplates.Where(x => (x.Subject?.Contains(keywords, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                                           || (x.Body?.Contains(keywords, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                                           || (x.Name?.Contains(keywords, StringComparison.InvariantCultureIgnoreCase) ?? false)).ToList();
        }

        return messageTemplates;
    }

    /// <summary>
    /// Create a copy of message template with all depended data
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the message template copy
    /// </returns>
    public virtual async Task<MessageTemplate> CopyMessageTemplateAsync(MessageTemplate messageTemplate)
    {
        ArgumentNullException.ThrowIfNull(messageTemplate);

        var mtCopy = new MessageTemplate
        {
            Name = messageTemplate.Name,
            BccEmailAddresses = messageTemplate.BccEmailAddresses,
            Subject = messageTemplate.Subject,
            Body = messageTemplate.Body,
            IsActive = messageTemplate.IsActive,
            AttachedDownloadId = messageTemplate.AttachedDownloadId,
            EmailAccountId = messageTemplate.EmailAccountId,
            LimitedToStores = messageTemplate.LimitedToStores,
            DelayBeforeSend = messageTemplate.DelayBeforeSend,
            DelayPeriod = messageTemplate.DelayPeriod
        };

        await InsertMessageTemplateAsync(mtCopy);

        return mtCopy;
    }

    #endregion
}
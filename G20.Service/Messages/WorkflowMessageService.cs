using G20.Core.Domain;
using G20.Core.Enums;
using Nop.Core;


namespace G20.Service.Messages;

/// <summary>
/// Workflow message service
/// </summary>
public partial class WorkflowMessageService : IWorkflowMessageService
{
    #region Fields

    protected readonly IEmailAccountService _emailAccountService;
    protected readonly IMessageTemplateService _messageTemplateService;
    protected readonly IMessageTokenProvider _messageTokenProvider;
    protected readonly IQueuedEmailService _queuedEmailService;
    protected readonly ITokenizer _tokenizer;

    #endregion

    #region Ctor

    public WorkflowMessageService(
        
        IEmailAccountService emailAccountService,
        IMessageTemplateService messageTemplateService,
        IMessageTokenProvider messageTokenProvider,
        IQueuedEmailService queuedEmailService,
        ITokenizer tokenizer)
    {
        
        _emailAccountService = emailAccountService;
        _messageTemplateService = messageTemplateService;
        _messageTokenProvider = messageTokenProvider;
        _queuedEmailService = queuedEmailService;
        _tokenizer = tokenizer;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Get active message templates by the name
    /// </summary>
    /// <param name="messageTemplateName">Message template name</param>
    /// <param name="storeId">Store identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of message templates
    /// </returns>
    protected virtual async Task<IList<MessageTemplate>> GetActiveMessageTemplatesAsync(string messageTemplateName)
    {
        //get message templates by the name
        var messageTemplates = await _messageTemplateService.GetMessageTemplatesByNameAsync(messageTemplateName);

        //no template found
        if (!messageTemplates?.Any() ?? true)
            return new List<MessageTemplate>();

        //filter active templates
        messageTemplates = messageTemplates.Where(messageTemplate => messageTemplate.IsActive).ToList();

        return messageTemplates;
    }

    /// <summary>
    /// Get EmailAccount to use with a message templates
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="languageId">Language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the emailAccount
    /// </returns>
    protected virtual async Task<EmailAccount> GetEmailAccountOfMessageTemplateAsync(MessageTemplate messageTemplate)
    {
        var emailAccountId = messageTemplate.EmailAccountId;
        ////some 0 validation (for localizable "Email account" dropdownlist which saves 0 if "Standard" value is chosen)
        //if (emailAccountId == 0)
        //    emailAccountId = messageTemplate.EmailAccountId;

        var emailAccount = (await _emailAccountService.GetEmailAccountByIdAsync(emailAccountId) ?? await _emailAccountService.GetEmailAccountByIdAsync(emailAccountId)) ??
                           (await _emailAccountService.GetAllEmailAccountsAsync()).FirstOrDefault();
        return emailAccount;
    }

    /// <summary>
    /// Get email and name to send email for store owner
    /// </summary>
    /// <param name="messageTemplateEmailAccount">Message template email account</param>
    /// <returns>Email address and name to send email fore store owner</returns>
    protected virtual async Task<(string email, string name)> GetStoreOwnerNameAndEmailAsync(EmailAccount messageTemplateEmailAccount)
    {
        var storeOwnerEmailAccount =  messageTemplateEmailAccount;

        return (storeOwnerEmailAccount.Email, storeOwnerEmailAccount.DisplayName);
    }

    /// <summary>
    /// Get email and name to set ReplyTo property of email from customer 
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="customer">Customer</param>
    /// <returns>Email address and name when reply to email</returns>
    protected virtual async Task<(string email, string name)> GetCustomerReplyToNameAndEmailAsync(MessageTemplate messageTemplate)
    {
        if (!messageTemplate.AllowDirectReply)
            return (null, null);

        var replyToEmail = string.Empty;

        var replyToName = string.Empty;
  

        return (replyToEmail, replyToName);
    }



    #endregion

    #region Methods

    #region Common

    /// <summary>
    /// Send notification
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="emailAccount">Email account</param>
    /// <param name="languageId">Language identifier</param>
    /// <param name="tokens">Tokens</param>
    /// <param name="toEmailAddress">Recipient email address</param>
    /// <param name="toName">Recipient name</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name</param>
    /// <param name="replyToEmailAddress">"Reply to" email</param>
    /// <param name="replyToName">"Reply to" name</param>
    /// <param name="fromEmail">Sender email. If specified, then it overrides passed "emailAccount" details</param>
    /// <param name="fromName">Sender name. If specified, then it overrides passed "emailAccount" details</param>
    /// <param name="subject">Subject. If specified, then it overrides subject of a message template</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    public virtual async Task<int> SendNotificationAsync(MessageTemplate messageTemplate,
        EmailAccount emailAccount, IList<Token> tokens,
        string toEmailAddress, string toName,
        string attachmentFilePath = null, string attachmentFileName = null,
        string replyToEmailAddress = null, string replyToName = null,
        string fromEmail = null, string fromName = null, string subject = null)
    {
        ArgumentNullException.ThrowIfNull(messageTemplate);

        ArgumentNullException.ThrowIfNull(emailAccount);

        //retrieve localized message template data
        var bcc = messageTemplate.BccEmailAddresses;// await _localizationService.GetLocalizedAsync(messageTemplate, mt => messageTemplate.BccEmailAddresses, languageId);
        if (string.IsNullOrEmpty(subject))
            subject = messageTemplate.Subject;
        var body = messageTemplate.Body;

        //Replace subject and body tokens 
        var subjectReplaced = _tokenizer.Replace(subject, tokens, false);
        var bodyReplaced = _tokenizer.Replace(body, tokens, true);

        //limit name length
        toName = CommonHelper.EnsureMaximumLength(toName, 300);

        var email = new QueuedEmail
        {
            Priority = QueuedEmailPriority.High,
            From = !string.IsNullOrEmpty(fromEmail) ? fromEmail : emailAccount.Email,
            FromName = !string.IsNullOrEmpty(fromName) ? fromName : emailAccount.DisplayName,
            To = toEmailAddress,
            ToName = toName,
            ReplyTo = replyToEmailAddress,
            ReplyToName = replyToName,
            CC = string.Empty,
            Bcc = bcc,
            Subject = subjectReplaced,
            Body = bodyReplaced,
            AttachmentFilePath = attachmentFilePath,
            AttachmentFileName = attachmentFileName,
            AttachedDownloadId = messageTemplate.AttachedDownloadId,
            CreatedOnUtc = DateTime.UtcNow,
            EmailAccountId = emailAccount.Id,
            DontSendBeforeDateUtc = !messageTemplate.DelayBeforeSend.HasValue ? null
                : (DateTime?)(DateTime.UtcNow + TimeSpan.FromHours(messageTemplate.DelayPeriod.ToHours(messageTemplate.DelayBeforeSend.Value)))
        };

        await _queuedEmailService.InsertQueuedEmailAsync(email);
        return email.Id;
    }

    #endregion

    /// <summary>
    /// Sends 'New customer' notification message to a store owner
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    public virtual async Task<IList<int>> SendTestNotificationMessageAsync()
    {
     

        var messageTemplates = await GetActiveMessageTemplatesAsync(MessageTemplateSystemNames.TEST_NOTIFICATION);
        if (!messageTemplates.Any())
            return new List<int>();

        //tokens
        var commonTokens = new List<Token>();
        await _messageTokenProvider.AddSampleTokensAsync(commonTokens);

        return await messageTemplates.SelectAwait(async messageTemplate =>
        {
            //email account
            var emailAccount = await GetEmailAccountOfMessageTemplateAsync(messageTemplate);

            var tokens = new List<Token>(commonTokens);
            await _messageTokenProvider.AddSampleTokensAsync(tokens);

            //event notification

            var (toEmail, toName) = await GetStoreOwnerNameAndEmailAsync(emailAccount);

            var (replyToEmail, replyToName) = await GetCustomerReplyToNameAndEmailAsync(messageTemplate);

            return await SendNotificationAsync(messageTemplate, emailAccount, tokens, toEmail, toName,
                replyToEmailAddress: replyToEmail, replyToName: replyToName);
        }).ToListAsync();
    }

    #endregion
}
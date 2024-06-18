
using G20.Core.Domain;

namespace G20.Service.Messages;

/// <summary>
/// Workflow message service
/// </summary>
public partial interface IWorkflowMessageService
{
    Task<int> SendNotificationAsync(MessageTemplate messageTemplate,
        EmailAccount emailAccount, IList<Token> tokens,
        string toEmailAddress, string toName,
        string attachmentFilePath = null, string attachmentFileName = null,
        string replyToEmailAddress = null, string replyToName = null,
        string fromEmail = null, string fromName = null, string subject = null);

    Task<IList<int>> SendTestNotificationMessageAsync();
}
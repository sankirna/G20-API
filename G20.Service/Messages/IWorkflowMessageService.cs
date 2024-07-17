
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

    Task<IList<int>> SendOrderNotificationMessageAsync(
        Venue venue,
        Product product,
        TicketCategory ticketCategory,
        Order order,
        OrderProductItem orderProductItem,
        OrderProductItemDetail orderProductItemDetail,
        Core.Domain.File orderProductItemDetailORCodeFile
        );

    Task<IList<int>> SendResetPasswordNotificationMessageAsync(User user);
    Task<IList<int>> SendUserRegistrationNotificationMessageAsync(User user);
}
using G20.Core.Domain;

namespace G20.Service.Messages;

/// <summary>
/// Message token provider
/// </summary>
public partial interface IMessageTokenProvider
{
    /// <summary>
    /// Add store tokens
    /// </summary>
    /// <param name="tokens">List of already added tokens</param>
    /// <param name="store">Store</param>
    /// <param name="emailAccount">Email account</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task AddSampleTokensAsync(IList<Token> tokens);
    Task AddTickectCategoryTokensAsync(IList<Token> tokens, TicketCategory ticketCategory);
    Task AddOrderTokensAsync(IList<Token> tokens, Order order);
    Task AddProductTokensAsync(IList<Token> tokens, Product product);
    Task AddVanueTokensAsync(IList<Token> tokens, Venue venue);
    Task AddOrderProductItemTokensAsync(IList<Token> tokens, OrderProductItem orderProductItem);
    Task AddOrderProductItemDetailTokensAsync(IList<Token> tokens, OrderProductItemDetail orderProductItemDetail);
    Task AddOrderProductItemDetailQRCodeTokensAsync(IList<Token> tokens, G20.Core.Domain.File file);

    /// <summary>
    /// Get token groups of message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>Collection of token group names</returns>
    IEnumerable<string> GetTokenGroups(MessageTemplate messageTemplate);
}
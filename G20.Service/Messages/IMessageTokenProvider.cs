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

    /// <summary>
    /// Get token groups of message template
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <returns>Collection of token group names</returns>
    IEnumerable<string> GetTokenGroups(MessageTemplate messageTemplate);
}
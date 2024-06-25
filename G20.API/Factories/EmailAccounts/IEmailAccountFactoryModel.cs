using G20.API.Models.EmailAccounts;
using System.Threading.Tasks;

namespace G20.API.Factories.EmailAccounts
{
    public interface IEmailAccountFactoryModel
    {
        Task<EmailAccountListModel> PrepareEmailAccountListModelAsync(EmailAccountSearchModel searchModel);
    }
}

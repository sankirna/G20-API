using G20.API.Models.Sponsors;
using System.Threading.Tasks;

namespace G20.API.Factories.Sponsors
{
    public interface ISponsorsFactoryModel
    {
        Task<SponsorListModel> PrepareSponsorListModelAsync(SponsorSearchModel searchModel);
    }
}

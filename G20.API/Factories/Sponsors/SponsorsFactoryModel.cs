using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Sponsors;
using G20.Service.Sponsors;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace G20.API.Factories.Sponsors
{
    public class SponsorFactoryModel : ISponsorsFactoryModel
    {
        protected readonly ISponsorService _SponsorService;

        public SponsorFactoryModel(ISponsorService SponsorService)
        {
            _SponsorService = SponsorService;
        }

        /// <summary>
        /// Prepare paged Sponsor list model
        /// </summary>
        /// <param name="searchModel">Sponsor search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the Sponsor list model
        /// </returns>
        public virtual async Task<SponsorListModel> PrepareSponsorListModelAsync(SponsorSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            //get Sponsors
            var Sponsors = await _SponsorService.GetSponsorsAsync(name: searchModel.Name,
                countryId: searchModel.CounryId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new SponsorListModel().PrepareToGridAsync(searchModel, Sponsors, () =>
            {
                return Sponsors.SelectAwait(async Sponsor =>
                {
                    //fill in model values from the entity
                    var SponsorModel = Sponsor.ToModel<SponsorModel>();
                    return SponsorModel;
                });
            });

            return model;
        }
    }
}
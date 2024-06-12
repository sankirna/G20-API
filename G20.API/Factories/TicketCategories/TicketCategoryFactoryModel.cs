using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Media;
using G20.API.Models.TicketCategories;
using G20.Service.TicketCategories;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Factories.TicketCategory
{
    public class TicketCategoryFactoryModel : ITicketCategoryFactoryModel
    {
        protected readonly ITicketCategoryService _ticketCategoryService;

        public TicketCategoryFactoryModel(ITicketCategoryService ticketCategoryService)
        {
            _ticketCategoryService = ticketCategoryService;
        }

        public virtual async Task<TicketCategoryListModel> PrepareTicketCategoryListModelAsync(TicketCategorySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            var ticketCategorys = await _ticketCategoryService.GetTicketCategoryAsync(name: searchModel.StandName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new TicketCategoryListModel().PrepareToGridAsync(searchModel, ticketCategorys, () =>
            {
                return ticketCategorys.SelectAwait(async ticketCategory =>
                {
                    var ticketCategoryModel = ticketCategory.ToModel<TicketCategoryModel>();
                    return ticketCategoryModel;
                });
            });

            return model;
        }

        
    }
}

using G20.API.Factories.Media;
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
        protected readonly IMediaFactoryModel _mediaFactoryModel;

        public TicketCategoryFactoryModel(ITicketCategoryService ticketCategoryService
            , IMediaFactoryModel mediaFactoryModel)
        {
            _ticketCategoryService = ticketCategoryService;
            _mediaFactoryModel = mediaFactoryModel;

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
                    ticketCategoryModel.File = await _mediaFactoryModel.GetRequestModelAsync(ticketCategoryModel.FileId);
                    return ticketCategoryModel;
                });
            });

            return model;
        }

        
    }
}

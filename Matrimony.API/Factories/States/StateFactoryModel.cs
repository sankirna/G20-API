using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.States;
using Matrimony.Service.States;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Matrimony.API.Factories.States
{
    public class StateFactoryModel : IStateFactoryModel
    {
        protected readonly IStateService _stateService;

        public StateFactoryModel(IStateService stateService)
        {
            _stateService = stateService;
        }

        /// <summary>
        /// Prepare paged state list model
        /// </summary>
        /// <param name="searchModel">State search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the state list model
        /// </returns>
        public virtual async Task<StateListModel> PrepareStateListModelAsync(StateSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            //get states
            var states = await _stateService.GetStatesAsync(name: searchModel.Name,
                countryId: searchModel.CounryId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new StateListModel().PrepareToGridAsync(searchModel, states, () =>
            {
                return states.SelectAwait(async state =>
                {
                    //fill in model values from the entity
                    var stateModel = state.ToModel<StateModel>();
                    return stateModel;
                });
            });

            return model;
        }
    }
}
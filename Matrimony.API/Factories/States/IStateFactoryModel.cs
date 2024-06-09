using Matrimony.API.Models.States;
using System.Threading.Tasks;

namespace Matrimony.API.Factories.States
{
    public interface IStateFactoryModel
    {
        Task<StateListModel> PrepareStateListModelAsync(StateSearchModel searchModel);
    }
}

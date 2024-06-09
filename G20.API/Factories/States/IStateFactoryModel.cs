using G20.API.Models.States;
using System.Threading.Tasks;

namespace G20.API.Factories.States
{
    public interface IStateFactoryModel
    {
        Task<StateListModel> PrepareStateListModelAsync(StateSearchModel searchModel);
    }
}

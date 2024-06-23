using Matrimony.API.Models.BoardingDetails;
using System.Threading.Tasks;

namespace Matrimony.API.Factories.BoardingDetails
{
    public interface IBoardingDetailFactoryModel
    {
        Task<BoardingDetailListModel> PrepareBoardingDetailListModelAsync(BoardingDetailSearchModel searchModel);
    }
}

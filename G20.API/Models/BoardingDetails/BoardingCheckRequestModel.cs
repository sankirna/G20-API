using G20.Framework.Models;

namespace G20.API.Models.BoardingDetails
{
    public partial record BoardingCheckRequestModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        //public int ProductTicketCategoryMapId { get; set; }
        public string ValidatePayload  { get; set; }
    }
}

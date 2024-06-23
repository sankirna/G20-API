

namespace G20.API.Models.BoardingDetails
{
    public partial record BoardingDetailRequestModel : BoardingDetailModel
    {
        public int UserId { get; set; }
        public int OrderProductItemDetailId { get; set; }
        public DateTime EntryDateTime { get; set; }

    }
}

namespace G20.API.Models.BoardingDetails
{
    public partial record BoardingCheckDetailModel
    {
        public int ProductId { get; set; }
        public int ProductTicketCategoryMapId { get; set; }
        public int OrderProductItemDetailId { get; set; }
        public int Quantity { get; set; }
    }
}

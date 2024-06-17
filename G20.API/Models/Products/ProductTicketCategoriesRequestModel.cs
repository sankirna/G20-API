namespace G20.API.Models.Products
{
    public partial record ProductTicketCategoriesRequestModel
    {
        public int? ProductId { get; set; }
        public List<int> ProductMapIds { get; set; }
    }
}

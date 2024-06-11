namespace G20.Core.Domain
{
    public partial class Team : BaseEntityWithTacking
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public virtual Country Country { get; set; }
        public virtual State? State { get; set; }
        public virtual City? City { get; set; }

    }
}

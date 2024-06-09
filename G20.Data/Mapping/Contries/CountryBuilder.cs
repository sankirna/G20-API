using FluentMigrator.Builders.Create.Table;
using G20.Core.Domain;
using Nop.Data.Mapping.Builders;

namespace G20.Data.Mapping.Contries
{
    public partial class CountryBuilder : NopEntityBuilder<Country>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            //table
            //    .WithColumn(nameof(Affiliate.AddressId)).AsInt32().ForeignKey<Address>().OnDelete(Rule.None);
        }

        #endregion
    }
}

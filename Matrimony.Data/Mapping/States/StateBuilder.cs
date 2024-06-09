using FluentMigrator.Builders.Create.Table;
using Matrimony.Core.Domain;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using System.Data;

namespace Matrimony.Data.Mapping.States
{
    public partial class StateBuilder : NopEntityBuilder<State>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                //.WithColumn(nameof(State.Id)).AsInt32().PrimaryKey().Identity()
                //.WithColumn(nameof(State.Name)).AsString(255).NotNullable()
                .WithColumn(nameof(State.CountryId)).AsInt32().ForeignKey<Country>(nameof(Country.Id)).OnDelete(Rule.Cascade);
        }

        #endregion
    }
}

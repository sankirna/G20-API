using G20.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nop.Data.Mapping.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;

namespace G20.Data.Mapping.Cities
{
    public partial class CityBuilder : NopEntityBuilder<City>
    {
        //public override void Configure(EntityTypeBuilder<City> builder)
        //{
        //    //builder.ToTable("City");
        //    //builder.HasKey(c => c.Id);

        //    //builder.Property(c => c.Name).IsRequired().HasMaxLength(400);

        //    //builder.HasOne(c => c.State)
        //    //    .WithMany(s => s.Cities)
        //    //    .HasForeignKey(c => c.StateId)
        //    //    .OnDelete(DeleteBehavior.Cascade); // Optional: configure delete behavior
        //}

        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            //throw new NotImplementedException();
        }
    }
}

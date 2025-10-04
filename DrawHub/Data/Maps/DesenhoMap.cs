using DrawHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrawHub.Data.Maps
{
    public class DesenhoMap : IEntityTypeConfiguration<Desenho>
    {
        public void Configure(EntityTypeBuilder<Desenho> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
            builder.HasMany(x => x.Categorias);
        }
    }
}

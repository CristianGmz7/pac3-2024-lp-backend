using BlogUNAH.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogUNAH.API.Database.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        //cuando se este creando
        builder.HasOne((e) => e.CreatedByUser)      //propiedad virtual
            .WithMany()
            .HasForeignKey((e) => e.CreatedBy)       //CreatedBy es la llave foranea
            .HasPrincipalKey((e) => e.Id)           //esto representa la tabla de usuarios
            .IsRequired();

        //con esto se crea la relacion uno a muchos de categorias con usuario

        //cuando se este editando
        builder.HasOne((e) => e.UpdatedByUser)      //propiedad virtual
            .WithMany()
            .HasForeignKey((e) => e.UpdatedBy)       //CreatedBy es la llave foranea
            .HasPrincipalKey((e) => e.Id)           //esto representa la tabla de usuarios
            .IsRequired();

        //AQUI SE PODRIA PONER DATA ANNOTATIONS Y DEJAR LAS ENTIDADES VACIAS
            //HasColumnName seria la data annotation [Column("category_id")]
        //builder.Property(x => x.CategoryId).HasColumnName("Este_es_el_id_de_categoria");
    }
}

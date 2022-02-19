using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data
{
    public class RecetarioDbContext : DbContext
    {
        public RecetarioDbContext() 
            : base("name=DefaultConnection")
        {

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Publicacion> Publicaciones { get; set; }
        public virtual DbSet<Receta> Recetas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Valoracion> Valoraciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Nombre de las tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Receta>().ToTable("Recetas");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Publicacion>().ToTable("Publicaciones");
            modelBuilder.Entity<Comentario>().ToTable("Comentarios");
            modelBuilder.Entity<Valoracion>().ToTable("Valoraciones");

            //Evitar pluralizar los nombres de las tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Una receta puede tener muchas categorias
            modelBuilder.Entity<Receta>().HasMany(x => x.CategoriasDeReceta).WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("IdReceta");
                    m.MapRightKey("IdCategoria");
                    m.ToTable("RecetaCategorias");

                });

            //Una publicacion requiere una receta
            modelBuilder.Entity<Publicacion>().HasRequired(x => x.Receta).WithMany().HasForeignKey(x => x.IdReceta);
            
            //Una publicacion tiene muchos comentarios y un comentario pertenece a una publicacion
            modelBuilder.Entity<Publicacion>().HasMany(x => x.Comentarios).WithRequired().HasForeignKey(x=>x.IdPublicacion);

            //Una receta tiene muchas valoraciones y una valoracion pertenece a una receta
            modelBuilder.Entity<Receta>().HasMany(x => x.ValoracionesReceta).WithRequired().HasForeignKey(x => x.IdReceta);

        }

    }
}

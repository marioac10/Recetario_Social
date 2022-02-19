using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class PublicacionRepository
    {
        private RecetarioDbContext _context;

        public PublicacionRepository()
        {
            _context = new RecetarioDbContext();
        }

        public PublicacionRepository(RecetarioDbContext context)
        {
            this._context = context;
        }

        public List<Publicacion> Get(int? idPublicacion, DateTime? from, DateTime? to,int? idReceta)
        {
            return this._context.Publicaciones.Where(c => (idPublicacion == null || c.Id == idPublicacion) 
            && (from == null || c.FechaDePublicacion >= from) 
            && (to == null || c.FechaDePublicacion <= to)
            && (idReceta ==null || c.IdReceta == idReceta)).ToList();
        }

        public void Insert(Publicacion publicacion)
        {
            this._context.Publicaciones.Add(publicacion);
            this._context.SaveChanges();
        }

        public void Update(Publicacion publicacion)
        {
            var pub = this._context.Publicaciones.Find(publicacion.Id);
            pub.Descripcion = publicacion.Descripcion;
            this._context.Entry(pub).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var publicacion = this._context.Publicaciones.Find(id);
            this._context.Publicaciones.Remove(publicacion);
            this._context.SaveChanges();
        }

        /************Método para obtener comentarios***************/
        public List<Comentario> GetComentarios(int idPublicacion)
        {
            var publicacion = this._context.Publicaciones.FirstOrDefault(x => x.Id == idPublicacion);
            var comentarios = publicacion.Comentarios.ToList();
            return comentarios;
        }//Desde el metodo del GET//Del servicio
    }
}

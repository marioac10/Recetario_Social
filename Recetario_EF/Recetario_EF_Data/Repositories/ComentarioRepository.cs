using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class ComentarioRepository
    {
        private RecetarioDbContext _context;
        public ComentarioRepository()
        {
            this._context = new RecetarioDbContext();  
        }

        public ComentarioRepository(RecetarioDbContext context)
        {
            this._context = context;
        }

        public void Insert(Comentario comentario)
        {
            this._context.Comentarios.Add(comentario);
            this._context.SaveChanges();
        }
        public void Update(Comentario comentario)
        {
            var entity = this._context.Comentarios.Find(comentario.Id);
            entity.CuerpoDelComentario = comentario.CuerpoDelComentario;
            this._context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = this._context.Comentarios.Find(id);
            this._context.Comentarios.Remove(entity);
            this._context.SaveChanges();
        }


    }
}

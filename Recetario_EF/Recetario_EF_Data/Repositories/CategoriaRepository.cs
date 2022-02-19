using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class CategoriaRepository
    {
        private RecetarioDbContext _context;
        public CategoriaRepository()
        {
            this._context = new RecetarioDbContext();
        }

        public CategoriaRepository(RecetarioDbContext context)
        {
            this._context = context;
        }

        public List<Categoria> Get(int? id, string nombre)
        {
            var categorias = this._context.Categorias.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrEmpty(nombre))
                categorias = categorias.Where(x => x.Nombre.Contains(nombre));
            if (id != null)
                categorias = categorias.Where(x => x.Id == id);

            return categorias.ToList();
        }

        public void Insert(Categoria categoria)
        {
            this._context.Categorias.Add(categoria);
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var categoria = this._context.Categorias.Find(id);
            this._context.Categorias.Remove(categoria);
            this._context.SaveChanges();
        }
    }
}

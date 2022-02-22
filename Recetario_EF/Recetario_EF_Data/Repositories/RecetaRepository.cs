using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class RecetaRepository
    {
        private RecetarioDbContext _context;
        public RecetaRepository()
        {
            this._context = new RecetarioDbContext();
        }
        public RecetaRepository(RecetarioDbContext context)
        {
            this._context = context;
        }

        public List<Receta> Get(int? idReceta, string nombre, DateTime? from, DateTime? to,List<int> listaCategorias,int? idUsuario,bool? oculto)
        {
            var Recetas = this._context.Recetas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrEmpty(nombre))
                Recetas = Recetas.Where(x => x.Titulo.Contains(nombre));
            if (idReceta != null)
                Recetas = Recetas.Where(x => x.Id == idReceta);
            if (from != null)
                Recetas = Recetas.Where(x => x.FechaDeCreacion >= from);
            if (to != null)
                Recetas = Recetas.Where(x => x.FechaDeCreacion <= to);
            if (listaCategorias != null)
                Recetas = Recetas.Where(x => x.CategoriasDeReceta.Any(y=>listaCategorias.Contains(y.Id)));
            if(idUsuario != null)
                Recetas = Recetas.Where(x => x.IdUsuario == idUsuario);
            if (oculto != null)
                Recetas = Recetas.Where(x => x.Oculto == oculto);
                
            return Recetas.ToList();
        }

        //Insertar una receta
        public void Insert(Receta receta)
        {
            this._context.Recetas.Add(receta);
            this._context.SaveChanges();
        }

        //Actualizar una receta
        public void Update(Receta receta)
        {
            var rec = this._context.Recetas.Find(receta.Id);
            rec.Titulo = receta.Titulo;
            rec.Imagen = receta.Imagen;
            rec.Ingredientes = receta.Ingredientes;
            rec.Procedimiento = receta.Procedimiento;
            rec.Oculto = receta.Oculto;
            rec.CategoriasDeReceta.Clear();
            rec.CategoriasDeReceta = receta.CategoriasDeReceta;

            this._context.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        //Ocultar una receta
        public void UpdateOculto(Receta receta)
        {
            var rec = this._context.Recetas.Find(receta.Id);
            rec.Oculto = receta.Oculto;
            this._context.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        //Eliminar una receta
        public void Delete(int id)
        {
            var entity = this._context.Recetas.Find(id);
            this._context.Recetas.Remove(entity);
            this._context.SaveChanges();
        }
        
    }
}

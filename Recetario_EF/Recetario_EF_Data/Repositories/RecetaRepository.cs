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

        public List<Receta> Get(int? idReceta, string nombre, DateTime? from, DateTime? to,List<int> listaCategorias,int? idUsuario)
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
                //lista = Recetas.ToList();
                /*foreach (var r in lista)
                {
                    var receta = r.CategoriasDeReceta.Where(x => x.Id == idCategoria);
                    if (receta.Count() > 0)
                    {
                        listaRecetas.Add(r);
                    }
                }
                Recetas = listaRecetas.AsQueryable();*/
            if(idUsuario != null)
                Recetas = Recetas.Where(x => x.IdUsuario == idUsuario);
            
                
            return Recetas.ToList();
        }

        public void Insert(Receta receta)
        {
            this._context.Recetas.Add(receta);
            this._context.SaveChanges();
        }

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

        /*public void UpdateCategoriasReceta(Receta receta)
        {
            var rec = this._context.Recetas.Find(receta.Id);
            rec.CategoriasDeReceta.Clear();
            rec.CategoriasDeReceta = receta.CategoriasDeReceta;
            this._context.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }*/

        public void UpdateOculto(Receta receta)
        {
            var rec = this._context.Recetas.Find(receta.Id);
            rec.Oculto = receta.Oculto;
            this._context.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this._context.Recetas.Find(id);
            this._context.Recetas.Remove(entity);
            this._context.SaveChanges();
        }

        /***********Método para obtener las calificaciones**************/

        /*public List<Valoracion> RecetaValoraciones(int idReceta)
        {
            var receta = this._context.Recetas.FirstOrDefault(x => x.Id == idReceta);
            var valores = receta.ValoracionesReceta.ToList();
            return valores;
        }*/

        /*public List<Receta> GetRecetasPublicadas()
        {
            var recetas = this._context.Recetas.Join(this._context.Publicaciones, r => r.Id, p => p.IdReceta, (r, p) => new { r, p }).ToList();
            return recetas;
        }*/
    }
}

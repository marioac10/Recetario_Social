using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class ValoracionRepository
    {
        private RecetarioDbContext _context;
        public ValoracionRepository()
        {
            this._context = new RecetarioDbContext();
        }
        public ValoracionRepository(RecetarioDbContext context)
        {
            this._context = context;
        }

        public List<Valoracion> Get(int? idReceta,int? idUsuario)
        {
            var valoraciones = this._context.Valoraciones.AsQueryable();

            if (idReceta != null || idUsuario != null)
                valoraciones = valoraciones.Where(x=> x.IdReceta == idReceta || x.IdUsuario == idUsuario);
            if (idReceta != null && idUsuario != null)
                valoraciones = valoraciones.Where(x=>x.IdReceta == idReceta && x.IdUsuario == idUsuario);

            return valoraciones.ToList();
        }

        public void Insert(Valoracion valoracion)
        {
            /*var entity = new Valoracion();
            entity.IdUsuario = valoracion.IdUsuario;
            entity.IdReceta = valoracion.IdReceta;
            entity.Valor = valoracion.Valor;*/
            this._context.Valoraciones.Add(valoracion);
            this._context.SaveChanges();
        }

    }
}

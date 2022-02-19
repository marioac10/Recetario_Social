using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Data.Repositories
{
    public class UsuarioRepository
    {
        private RecetarioDbContext _context;

        public UsuarioRepository()
        {
            _context = new RecetarioDbContext();
        }

        public List<Usuario> Get(int? id, DateTime? from, DateTime? to)
        {
            return this._context.Usuarios.Where(c => id == null || c.Id == id).ToList();
        }


        public void Insert(Usuario user)
        {
            this._context.Usuarios.Add(user);
            this._context.SaveChanges();
        }
    }
}

using Recetario_EF_Data.Repositories;
using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Services
{
    public class UsuarioCtrl
    {
        private readonly UsuarioRepository _repository;
        public UsuarioCtrl()
        {
            _repository = new UsuarioRepository();
        }

        public List<Usuario> Get(DateTime? from = null, DateTime? to = null)
        {
            return this._repository.Get(null, from, to).ToList();
        }
        public Usuario GetById(int id)
        {
            var result = this._repository.Get(id, null, null);
            if (result.Count != 1)
                return null;
            return result.First();
        }

        public Usuario Insert(string nombre,string email, string password)
        {
            var entity = new Usuario();
            entity.Nombre = nombre;
            entity.Email = email;
            entity.Contraseña = password;
           
            this._repository.Insert(entity);
         
            return entity;
        }
    }
}

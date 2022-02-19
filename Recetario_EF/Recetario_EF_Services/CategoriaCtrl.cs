using Recetario_EF_Data.Repositories;
using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Services
{
    public class CategoriaCtrl
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaCtrl(CategoriaRepository _categoriaRepository)
        {
            this._categoriaRepository = _categoriaRepository;
        }
        /*public CategoriaCtrl()
        {
            this._repository = new CategoriaRepository();
        }*/

        public List<Categoria> Get() 
        {
            return this._categoriaRepository.Get(null,null).ToList();
        }

        public Categoria GetById(int id)
        {
            return this._categoriaRepository.Get(id, null).ToList().First();
        }

        public Categoria InsertCategoria(string nombre) 
        {
            var entity = new Categoria();
            entity.Nombre = nombre;
            this._categoriaRepository.Insert(entity);
          
            return entity;
        }

        public void DeleteCategoria(int idCategoria)
        {
            this._categoriaRepository.Delete(idCategoria);
        }

    }
}

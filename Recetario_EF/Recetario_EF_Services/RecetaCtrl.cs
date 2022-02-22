using Recetario_EF_Data;
using Recetario_EF_Data.Repositories;
using Recetario_EF_Domain.Entities;
using Recetario_EF_Domain.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Services
{
    public class RecetaCtrl
    {
        private readonly RecetaRepository _recetaRepository;
        private readonly CategoriaRepository _categoriaRepository;
        private readonly ValoracionRepository _valoracionRepository;

        public RecetaCtrl(RecetaRepository _recetaRepository,CategoriaRepository _categoriaRepository, ValoracionRepository _valoracionRepository)
        {
            this._recetaRepository = _recetaRepository;
            this._categoriaRepository = _categoriaRepository;
            this._valoracionRepository = _valoracionRepository;
        }

        /*public RecetaCtrl(RecetarioDbContext dbContext)
        {
            this._recetaRepository = new RecetaRepository(dbContext);
            this._categoriaRepository = new CategoriaRepository(dbContext);
            this._valoracionRepository = new ValoracionRepository(dbContext);
        }*/

        public List<Receta> Get(string titulo,DateTime? from, DateTime? to,List<int> listaCategorias,int? idUsuario,bool? oculto)
        {
            return this._recetaRepository.Get(null,titulo,from,to,listaCategorias,idUsuario,oculto).ToList();
        }

        public Receta GetById(int id)
        {
            var receta = this._recetaRepository.Get(id, null, null, null, null, null,null);
            if (receta.Count != 1)
                return null;
            return receta.First();
        }

        //Insertar una receta
        public Receta Insert(string titulo,byte[] imagen,string ingredientes, string procedimiento,int idUser,List<int> categorias)
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            foreach (var id in categorias)
            {
                var categoria = this._categoriaRepository.Get(id,null).First();
                listaCategorias.Add(categoria);
            }

            var receta = new Receta()
            {
                Titulo = titulo,
                Imagen = imagen,
                Ingredientes = ingredientes,
                Procedimiento = procedimiento,
                FechaDeCreacion = DateTime.Now,
                Oculto = false,
                IdUsuario = idUser,
                CategoriasDeReceta = listaCategorias
            };      
            this._recetaRepository.Insert(receta);
            return receta;
        }

        //Actualizar una receta
        public Receta Update(int id,string titulo,byte[] imagen,string ingredientes,string procedimiento,bool oculto,List<int> categorias)
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            foreach (var idC in categorias)
            {
                var categoria = this._categoriaRepository.Get(idC, null).First();
                listaCategorias.Add(categoria);
            }
            
            var receta = new Receta()
            {
                Id = id,
                Titulo = titulo,
                Imagen = imagen,
                Ingredientes = ingredientes,
                Procedimiento = procedimiento,
                Oculto = oculto,
                CategoriasDeReceta = listaCategorias
            };
                 
            this._recetaRepository.Update(receta);
            return receta;
        }

        //Eliminar una receta, se hace en cascada
        public void Delete(int id)
        {
            this._recetaRepository.Delete(id);
        }

        //Ocultar una receta
        public Receta OcultarReceta(int id, bool oculto)
        {
            var receta = new Receta()
            {
                Oculto = oculto
            };
            this._recetaRepository.UpdateOculto(receta);
            return receta;
        }

        /*public void UpdateCategoriasDeReceta(int idReceta,List<int> categorias)
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            foreach (var id in categorias)
            {
                var categoria = this._categoriaRepository.Get(id, null).First();
                listaCategorias.Add(categoria);
            }
            var receta = new Receta() { 
                Id = idReceta,
                CategoriasDeReceta=listaCategorias
            };
            this._repository.UpdateCategoriasReceta(receta);
        }*/

        /***************MÉTODOS PARA LA VALORACIÓN DE UNA RECETA*******************/
        //Calificar una receta
        public Valoracion InsertValoracion(int idReceta,int idUsuario, double valor) 
        {
            var valoraciones = this._valoracionRepository.Get(idReceta, idUsuario).FirstOrDefault();
            if (valoraciones == null)
            {
                var entity = new Valoracion();
                entity.IdReceta = idReceta;
                entity.IdUsuario = idUsuario;
                entity.Valor = valor;
                this._valoracionRepository.Insert(entity);
                return entity;
            }
            return null;
        }

        //Actualizar una calificación
        public Valoracion UpdateValoracion()
        {
            return null;
        }

    }
}

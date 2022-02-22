using Recetario_EF_Data.Repositories;
using Recetario_EF_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Services
{
    public class PublicacionCtrl
    {
        public readonly PublicacionRepository _publicacionRepository;
        public readonly ComentarioRepository _repositoryComentario;

        public PublicacionCtrl(PublicacionRepository _publicacionRepository, ComentarioRepository _comentarioRepository)
        {
            this._publicacionRepository = _publicacionRepository;
            this._repositoryComentario = _comentarioRepository;
        }

        /*public PublicacionCtrl()
        {
            this._publicacionRepository = new PublicacionRepository();
            this._repositoryComentario = new ComentarioRepository();
        }*/

        public List<Publicacion> Get(DateTime? from, DateTime? to,int? idReceta,bool? oculto)
        {
            return this._publicacionRepository.Get(null, from, to,idReceta,oculto).OrderByDescending(p => p.FechaDePublicacion).ToList();
        }

        public Publicacion GetById(int id)
        {
            var result = this._publicacionRepository.Get(id,null,null,null,null);
            if (result.Count != 1)
                return null;
            return result.First();
        }

        //Insertar una publicación
        public Publicacion Insert(int idReceta,string descripcion)
        {
            var entity = new Publicacion();
            entity.IdReceta = idReceta;
            entity.Descripcion = descripcion;
            entity.FechaDePublicacion = DateTime.Now;

            this._publicacionRepository.Insert(entity);
            return entity;
        }

        //Actualizar una publicación
        public Publicacion Update(int idPublicacion,string description)
        {
            var entity = new Publicacion();
            entity.Id = idPublicacion;
            entity.Descripcion = description;

            this._publicacionRepository.Update(entity);
            return entity;
        }

        //Eliminar un publicación
        public void Delete(int id)
        {
            this._publicacionRepository.Delete(id);
        }

        /**********************MÉTODOS DE LOS COMENTARIOS**********************/
        //Obtener los comentarios de una publicación
        public List<Comentario> GetComentarios(int idPublicacion)
        {
            var publicacion = this.GetById(idPublicacion);
            var listaComentarios = publicacion.Comentarios.ToList();

            return listaComentarios;
        }

        //Insertar un comentario a una publicación
        public Comentario InsertComentario(string comentario, int idPublicacion, int idUsuario)
        {
            var entity = new Comentario();
            entity.CuerpoDelComentario = comentario;
            entity.FechaDelComentario = DateTime.Now;
            entity.IdPublicacion = idPublicacion;
            entity.IdUsuario = idUsuario;
            this._repositoryComentario.Insert(entity); 

            return entity;
        }

        //Actualizar un comentario de una publicación
        public Comentario UpdateComentario(int idComentario, string description, int idPublicacion)
        {
            var comentario = new Comentario()
            {
                Id = idComentario,
                CuerpoDelComentario = description
            };

            this._repositoryComentario.Update(comentario);
            return comentario;
        }

        //Eliminar un comentario de una publicación
        public void DeleteComentario(int id)
        {
            this._repositoryComentario.Delete(id);
        }

    }
}

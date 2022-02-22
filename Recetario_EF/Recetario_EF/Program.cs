using Recetario_EF_Data;
using Recetario_EF_Data.Repositories;
using Recetario_EF_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            bool bandera = false;
           
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("Seleccione una option: \n");
                Console.WriteLine("0: Salir");
                Console.WriteLine("1: Categorias");
                Console.WriteLine("2: Recetas");
                Console.WriteLine("3: Publicaciones");
                Console.WriteLine("4: Valoraciones");

                Console.Write("Ingrese un numero: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0://Salir
                        bandera = true;
                        break;
                    case 1://Categorias
                        Console.WriteLine("\n");
                        Categorias();
                        break;
                    case 2://Recetas
                        Console.WriteLine("\n");
                        Recetas();
                        break;
                    case 3://Publicaciones
                        Console.WriteLine("\n");
                        Publicaciones();
                        break;
                    case 4://Valoraciones
                        Console.WriteLine("\n");
                        Valoraciones();
                        break;
                    default:
                        //bandera=true;
                        break;
                }
            } while (bandera != true);

            Console.WriteLine("See you later");
            Console.ReadKey();
        }


        static void Categorias()
        {
            //Inicializar los repositorios con el mismo contexto
            var dbContext = new RecetarioDbContext();
            var categoriaRepository = new CategoriaRepository(dbContext);
            var categoriaCtrl = new CategoriaCtrl(categoriaRepository); 
            //Variables
            bool bandera = false;
            int idCategoria;
            
            do
            {
                Console.WriteLine("CATEGORIAS");
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Ver todas las categorías");
                Console.WriteLine("2: Insertar una categoría");
                Console.WriteLine("3: Eliminar una categoría");
                Console.Write("Seleccione una opción: ");
                int numero = int.Parse(Console.ReadLine());
                switch (numero)
                {
                    case 0:
                        bandera = true;
                        break;
                    case 1://Ver todas las Categorias
                        var categorias = categoriaCtrl.Get();
                        Console.WriteLine("------------ All Categories ------------");
                        foreach (var c in categorias)
                        {
                            var s = string.Format("\t{0} {1}", c.Id, c.Nombre);
                            Console.WriteLine(s);
                        }
                        Console.WriteLine("\t");
                        break;

                    case 2://Insertar una Categoria
                        Console.WriteLine("------------ Category data ------------");
                        Console.Write("Category Name: ");
                        string cat = Console.ReadLine();
                        var success = categoriaCtrl.InsertCategoria(cat);
                        Console.WriteLine("");
                        if (success != null)
                        {
                            Console.WriteLine("------------ Registered Category ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Failed to Register ------------");
                        }

                        Console.WriteLine("\n");
                        break;
                    case 3://Eliminar una categoria
                        Console.WriteLine("\n");
                        Console.WriteLine("Eliminar una categoría");
                        Console.WriteLine("Ingrese los siguientes datos:");
                        Console.Write("Id de la categoría: ");
                        idCategoria = int.Parse(Console.ReadLine());
                        categoriaCtrl.DeleteCategoria(idCategoria);
                        Console.WriteLine("------------ Eliminación exitosa ------------");
                        break;
                }
            }while (bandera != true);
        }
        static void Recetas()
        {
            //Inicializar repositorios con el mismo Contexto
            var dbContext = new RecetarioDbContext();
            var recetaRepository  = new RecetaRepository(dbContext);
            var categoriaRepository = new CategoriaRepository(dbContext);
            var valoracionRepository = new ValoracionRepository(dbContext);

            var recetaCtrl = new RecetaCtrl(recetaRepository,categoriaRepository,valoracionRepository);
            //Variables
            int idReceta, idUsuario,idCategoria;
            string titulo, ingredientes, procedimiento, idCategorias;
            bool oculto;

            bool bandera = false;
            
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("RECETAS");
 
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Ver todas las recetas");
                Console.WriteLine("2: Encontrar una receta por Id");
                Console.WriteLine("3: Encontrar recetas por nombre");
                Console.WriteLine("4: Encontrar recetas por intervalo de fechas");
                Console.WriteLine("5: Encontrar recetas por categoria");
                Console.WriteLine("6: Encontrar recetas por Id del usuario");
                Console.WriteLine("7: Insertar una receta");
                Console.WriteLine("8: Actualizar una receta");
                Console.WriteLine("9: Ocultar una receta");
                Console.WriteLine("10: Eliminar una receta");
                Console.WriteLine("11: Ver recetas que están ocultas o visibles");

                Console.Write("Seleccione un opción: ");
                int numero = int.Parse(Console.ReadLine());

                switch (numero)
                {
                    case 0: 
                        bandera = true;
                        break;
                    case 1://Ver todas las Recetas
                        Console.WriteLine("\n");
                        var recetas = recetaCtrl.Get(null, null, null, null, null,null);
                        Console.WriteLine("------------ Todas las Recetas ------------");
                        foreach (var rec in recetas)
                        {
                            var s = string.Format("Titulo: {0}\nIngredientes: \n{1}\n Procedimiento:\n{2}\n Oculto: {3}", rec.Titulo, rec.Ingredientes, rec.Procedimiento, rec.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 2://Encontrar una receta por id
                        Console.WriteLine("------------ Encontrar una Receta por Id ------------");
                        Console.Write("Enter the Id: ");
                        int id_receta = int.Parse(Console.ReadLine());
                        var receta = recetaCtrl.GetById(id_receta);

                       if(receta != null)
                        {
                            Console.WriteLine("Titulo: {0}", receta.Titulo);
                            Console.WriteLine("Ingredientes: {0}", receta.Ingredientes);
                            Console.WriteLine("Procedimiento: {0}", receta.Procedimiento);
                            Console.WriteLine("------------ Receta encontrada ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Receta no encontrada ------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 3://Encontrar Recetas por coincidencia de nombre
                        Console.WriteLine("\n");
                        Console.WriteLine("------------ Encontrar recetas por nombre ------------");
                        Console.Write("Ingresa el nombre: ");
                        string nombreR = Console.ReadLine();
                        var rece = recetaCtrl.Get(nombreR,null,null,null,null,null);
                        foreach (var rec in rece)
                        {

                            var s = string.Format("Titulo: {0}\nIngredientes: \n{1}\n Procedimiento:\n{2}\n Oculto: {3}", rec.Titulo, rec.Ingredientes, rec.Procedimiento, rec.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                            
                        }
                        Console.WriteLine("\n");
                        break;
                    case 4://Encontrar recetas por intervalo de fechas
                        Console.WriteLine("\n");
                        Console.WriteLine("Buscar recetas por intervalo de fechas");
                        Console.WriteLine("Ingrese los siguientes datos: ");
                        Console.Write("Desde (dd/mm/yyyy):");
                        var desde = DateTime.Parse(Console.ReadLine());
                        Console.Write("Hasta (dd/mm/yyyy):");
                        var hasta = DateTime.Parse(Console.ReadLine());
                        var listaRecetas = recetaCtrl.Get(null,desde,hasta,null,null,null);
                        Console.WriteLine("");
                        foreach (var r in listaRecetas)
                        {
                            var s = string.Format("Fecha: {0}\nTitulo: {1}\nIngredientes: \n{2}\n Procedimiento:\n{3}\n Oculto: {4}",r.FechaDeCreacion,r.Titulo, r.Ingredientes, r.Procedimiento, r.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 5://Encontrar recetas por id de categorías
                        Console.WriteLine("");
                        Console.WriteLine("Recetas por Categoría");
                        Console.Write("Lista de categorias por las que desea buscar (1,2,3): ");
                        idCategorias = Console.ReadLine();
                        Console.WriteLine("");
                        List<int> listaCatego = new List<int>();
                        string[] listaC = idCategorias.Split(',');
                        foreach (var c in listaC)
                        {
                            listaCatego.Add(int.Parse(c));
                        }
                        var recetasCategoria = recetaCtrl.Get(null, null, null,listaCatego,null,null);

                        foreach (var rec in recetasCategoria)
                        {
                            var s = string.Format("Fecha: {0}\nTitulo: {1}\nIngredientes: \n{2}\n Procedimiento:\n{3}\n Oculto: {4}", rec.FechaDeCreacion, rec.Titulo, rec.Ingredientes, rec.Procedimiento, rec.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                        }
                        //Console.WriteLine("\n");
                        break;
                    case 6://Encontrar recetas por id de Usuario
                        Console.WriteLine("");
                        Console.WriteLine("Recetas por Usuario");
                        Console.Write("Ingrese el id del Usuario: ");
                        idUsuario = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        //var from = DateTime.Parse("15-02-2022");
                        //var to = DateTime.Parse("13-02-2022");
                        var recetaPorUsuario = recetaCtrl.Get(null,null,null,null,idUsuario,null);
                        foreach ( var r in recetaPorUsuario)
                        {
                            var s = string.Format("Fecha: {0}\nTitulo: {1}\nIngredientes: \n{2}\n Procedimiento:\n{3}\n Oculto: {4}", r.FechaDeCreacion, r.Titulo, r.Ingredientes, r.Procedimiento, r.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                        }
                        break;
                    case 7://Insert a Receta
                        Console.WriteLine("");
                        Console.WriteLine("------------ Datos de la Receta ------------");

                        Console.Write("Titulo: ");
                        titulo = Console.ReadLine();
                        Console.Write("Ingredientes: ");
                        ingredientes = Console.ReadLine();
                        Console.Write("Procedimiento: ");
                        procedimiento = Console.ReadLine();
                        Console.Write("IdUsuario: ");
                        idUsuario = int.Parse(Console.ReadLine());
                        Console.Write("Lista de categorias: ");
                        idCategorias = Console.ReadLine();
                        
                        List<int> listaCat = new List<int>();
                        string[] list = idCategorias.Split(',');

                        foreach (var id in list)
                        {
                            listaCat.Add(int.Parse(id.ToString()));
                            //Console.WriteLine("<{0}>",id);
                        }

                        var successs = recetaCtrl.Insert(titulo, null, ingredientes, procedimiento, idUsuario, listaCat);
                        Console.WriteLine("");
                        if (successs != null)
                        {
                            Console.WriteLine("------------ Receta guardada ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Failed to Register ------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 8://Update a Receta
                        Console.WriteLine("");
                        Console.WriteLine("Actualizar una receta");
                        Console.WriteLine("Ingrese los siguientes datos:");
                        Console.Write("Id de la Receta: ");
                        idReceta = int.Parse(Console.ReadLine());
                        Console.Write("Titulo: ");
                        titulo = Console.ReadLine();
                        Console.Write("Ingredientes: ");
                        ingredientes = Console.ReadLine();
                        Console.Write("Procedimiento: ");
                        procedimiento = Console.ReadLine();
                        Console.Write("Oculto (true/false)");
                        oculto = bool.Parse(Console.ReadLine());
                        Console.Write("Lista de categorias: ");
                        idCategorias = Console.ReadLine();

                        List<int> listaCate = new List<int>();
                        string[] lista = idCategorias.Split(',');

                        foreach (var id in lista)
                        {
                            listaCate.Add(int.Parse(id.ToString()));
                        }

                        var recet = recetaCtrl.Update(idReceta, titulo, null, ingredientes, procedimiento, oculto,listaCate);
                        if (recet != null)
                        {
                            Console.WriteLine("------------ Actualización exitosa ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Error al actualizar ------------");
                        }

                        break;
                    case 9://Ocultar una receta
                        Console.WriteLine("");
                        Console.WriteLine("Ocultar una receta");
                        Console.Write("Ingrese el id de la Receta: ");
                        idReceta = int.Parse(Console.ReadLine());
                        Console.Write("Ocultar (true/false): ");
                        oculto = bool.Parse(Console.ReadLine());
                        recetaCtrl.OcultarReceta(idReceta, oculto);
                        Console.WriteLine("------------ Se ha ocultado la receta ------------");
                        Console.WriteLine("\n");
                        break;
                    case 10://Eliminar una Receta
                        Console.WriteLine("");
                        Console.WriteLine("Eliminar una receta");
                        Console.Write("Ingrese el id de la Receta: ");
                        idReceta = int.Parse(Console.ReadLine());
                        recetaCtrl.Delete(idReceta);
                        Console.WriteLine("------------ Eliminación exitosa ------------");
                        Console.WriteLine("\n");
                        break;
                    case 11://Ver recetas que están ocultas o visibles
                        Console.WriteLine("");
                        Console.WriteLine("Ingrese false (para ver las recetas visibles) o true (para ver las recetas ocultas)");
                        Console.Write("Visibles (false), ocultas (true): ");
                        oculto=bool.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        var recetasOcultasoVisibles = recetaCtrl.Get(null,null,null,null,null,oculto);
                        foreach (var r in recetasOcultasoVisibles)
                        {
                            var s = string.Format("Fecha: {0}\nTitulo: {1}\nIngredientes: \n{2}\n Procedimiento:\n{3}\n Oculto: {4}", r.FechaDeCreacion, r.Titulo, r.Ingredientes, r.Procedimiento, r.Oculto);
                            Console.WriteLine(s);
                            Console.WriteLine("---------------------------");
                        }
                        break;
                    default:
                        break;
                }

            } while (bandera != true);
        }
        static void Publicaciones()
        {
            //Inicializar los repositorios con el mismo contexto
            var dbContext = new RecetarioDbContext();
            var publicacionRepository = new PublicacionRepository(dbContext);
            var comentarioRepository = new ComentarioRepository(dbContext);
            var publicacionCtrl = new PublicacionCtrl(publicacionRepository,comentarioRepository);
            //Variables
            bool bandera = false;
            int idPublicacion,idReceta,idUsuario;
            int idComentario;
            string descripcion;
            bool? oculto;
            //Comentario
            string cuerpoDelComentario;

            do
            {
                Console.WriteLine("PUBLICACIONES");
                Console.WriteLine("0: Salir");
                Console.WriteLine("1: Ver todas las publicaciones");
                Console.WriteLine("2: Encontrar publicaciones por intervalo de fechas");
                Console.WriteLine("3: Encontrar una publicación por Id");
                Console.WriteLine("4: Ver publicaciones de una receta");
                Console.WriteLine("5: Realizar una publicación");
                Console.WriteLine("6: Actualizar una publicación");
                Console.WriteLine("7: Eliminar una publicación");
                Console.WriteLine("8: Ver todos los comentarios por Id de publicación");
                Console.WriteLine("9: Insertar un comentario a una publicación");
                Console.WriteLine("10: Actualizar un comentario");
                Console.WriteLine("11: Eliminar un comentario");
                Console.WriteLine("12: Ver las publicaciones que están visibles o ocultas");

                Console.Write("Elige un opción: ");
                int numero = int.Parse(Console.ReadLine());

                switch (numero)
                {
                    case 0://Exit
                        bandera = true;
                        break;
                    case 1://Ver todas las publicaciones
                        Console.WriteLine("");
                        Console.WriteLine("Todas las publicaciones");
                        var publicaciones = publicacionCtrl.Get(null, null,null,null);
                        Console.WriteLine("");
                        foreach (var publ in publicaciones)
                        {
                            
                            var ps = string.Format("Fecha: {0}\nTitulo: {1}\nIngredientes: {2}\nProcedimiento: {3}\nOculto: {4}\nIdReceta: {5}", publ.FechaDePublicacion, publ.Receta.Titulo, publ.Receta.Ingredientes, publ.Receta.Procedimiento,publ.Receta.Oculto,publ.Receta.Id);
                            Console.WriteLine(ps);
                            Console.WriteLine("----------------------------------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 2://Encontrar publicaciones por intervalo de fechas
                        Console.WriteLine("Encontrar publicaciones por intervalo de fechas");
                        Console.Write("Desde (dd/mm/yyyy): ");
                        var from = DateTime.Parse(Console.ReadLine());
                        Console.Write("Hasta (dd/mm/yyyy): ");
                        DateTime? to = DateTime.Parse(Console.ReadLine());
                        var publicacioness = publicacionCtrl.Get(from,to,null,null);
                        Console.WriteLine("");
                        foreach (var p in publicacioness)
                        {
                            var publica = string.Format("Id: {0}\t{1}\t{2}\tIdReceta: {3}", p.Id, p.Descripcion, p.FechaDePublicacion, p.IdReceta);
                            Console.WriteLine(publica);
                        }
                        Console.WriteLine("\n");
                        break;
                    case 3://Encontrar una publicación por id
                        Console.WriteLine("Encontrar una publicación por Id");
                        Console.Write("Id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        var publi = publicacionCtrl.GetById(idPublicacion);
                        Console.WriteLine("");
                        Console.WriteLine("Id: {0}\t{1}\t{2}\tIdReceta: {3}", publi.Id, publi.Descripcion, publi.FechaDePublicacion, publi.IdReceta);
                        Console.WriteLine("\n");
                        break;
                    case 4://Ver publicaciones de una receta
                        Console.WriteLine("Ver publicaciones de una receta");
                        Console.WriteLine("Ingrese los siguientes datos: ");
                        Console.Write("Ingrese el id de la Receta: ");
                        idReceta = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        var listapublicaciones = publicacionCtrl.Get(null, null, idReceta,null);
                        foreach ( var p in listapublicaciones)
                        {
                            var s = string.Format("Id: {0}\t{1}\t{2}\tIdReceta: {3}", p.Id, p.Descripcion, p.FechaDePublicacion, p.IdReceta);
                            Console.WriteLine(s);
                            Console.WriteLine("-----------------");
                        }
                        Console.WriteLine("");
                        break;
                    case 5://Realizar una publicación
                        Console.WriteLine("");
                        Console.WriteLine("Realizar una publicación");
                        Console.WriteLine("Ingrese los siguientes datos:");
                        Console.Write("Id de la receta: ");
                        idReceta = int.Parse(Console.ReadLine());
                        Console.Write("Descripción: ");
                        descripcion = Console.ReadLine();
                        var publicacion = publicacionCtrl.Insert(idReceta, descripcion);
                        Console.WriteLine("\n");
                        if (publicacion != null)
                        {
                            Console.WriteLine("------------ Publicación exitosa ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Error al publicar ------------");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 6://Actualizar una publicación
                        Console.WriteLine("");
                        Console.WriteLine("Actualizar una publicación");
                        Console.WriteLine("Ingrese los siguienes datos: ");
                        Console.Write("Id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        Console.Write("Descripción de la publicación: ");
                        descripcion = Console.ReadLine();
                        var pu = publicacionCtrl.Update(idPublicacion, descripcion);
                        if (pu != null)
                        {
                            Console.WriteLine("------------ Actualización exitosa ------------");

                        }
                        else
                        {
                            Console.WriteLine("------------ Error al actualizar ------------");
                        }
                        break;
                    case 7://Eliminar una publicación
                        Console.WriteLine("");
                        Console.WriteLine("Eliminar una publicación");
                        Console.Write("Id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        publicacionCtrl.Delete(idPublicacion);
                        Console.WriteLine("\n");
                        Console.WriteLine("------------ Eliminación exitosa ------------");
                        Console.WriteLine("\n");
                        break;
                    case 8://Ver todos los comentarios por id de publicación
                        Console.WriteLine("");
                        Console.WriteLine("Ver los comentarios de una publicación por ID");
                        Console.Write("Ingrese el id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        var comentarios = publicacionCtrl.GetComentarios(idPublicacion);
                        foreach (var comentario in comentarios)
                        {
                            var comment = string.Format("Comentario: {0}\tFecha: {1}\tidUsuario: {2}", comentario.CuerpoDelComentario, comentario.FechaDelComentario, comentario.IdUsuario);
                            Console.WriteLine(comment);
                        }
                        Console.WriteLine("");
                        break;
                    case 9://Insertar un comentario a una publicación
                        Console.WriteLine("");
                        Console.WriteLine("Hacer un comentario a una publicación");
                        Console.WriteLine("Ingrese los siguientes datos:");
                        Console.Write("Id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        Console.Write("Id del usuario: ");
                        idUsuario = int.Parse(Console.ReadLine());
                        Console.Write("Comentario: ");
                        cuerpoDelComentario = Console.ReadLine();
                        var com = publicacionCtrl.InsertComentario(cuerpoDelComentario,idPublicacion, idUsuario);
                        Console.WriteLine("");
                        if (com != null)
                        {
                            Console.WriteLine("------------ Comentario exitoso ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Error al comentar ------------");
                        }
                        Console.WriteLine("");
                        break;
                    case 10://Actualizar un comentario
                        Console.WriteLine("Actualizar un comentario");
                        Console.WriteLine("Ingrese los siguientes datos: ");
                        Console.Write("Id del comentario: ");
                        idComentario = int.Parse(Console.ReadLine());
                        Console.Write("Comentario: ");
                        cuerpoDelComentario = Console.ReadLine();
                        Console.Write("Id de la publicación: ");
                        idPublicacion = int.Parse(Console.ReadLine());
                        var co = publicacionCtrl.UpdateComentario(idComentario,cuerpoDelComentario,idPublicacion);
                        if (co != null)
                        {
                            Console.WriteLine("------------ Comentario actualizado ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ Error al actualizar ------------");
                        }
                        Console.WriteLine("");
                        break;
                    case 11://Eliminar un comentario
                        Console.WriteLine("Eliminar un comentario");
                        Console.WriteLine("Ingrese los siguientes datos: ");
                        Console.Write("Ingrese el id del comentario: ");
                        idComentario = int.Parse(Console.ReadLine());
                        publicacionCtrl.DeleteComentario(idComentario);
                        Console.WriteLine("------------ Comentario eliminado ------------");
                        Console.WriteLine("");
                        break;
                    case 12://Ver las publicaciones que están visibles o ocultas
                        Console.WriteLine("");
                        Console.WriteLine("Ver las publicaciones que están visibles o ocultas");
                        Console.WriteLine("Ingrese true (para ver las publicaciones ocultas) o false (para ver las publicaciones visibles)");
                        Console.Write("Visibles (false), ocultas (true): ");
                        oculto = bool.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        var publicacionesVisiblesoOcultas = publicacionCtrl.Get(null,null,null,oculto);
                        foreach (var p in publicacionesVisiblesoOcultas)
                        {
                            var ps = string.Format("Fecha: {0}\n{1}\nTitulo: {2}\nIngredientes: {3}\nProcedimiento: {4}\nOculto: {5}\nIdReceta: {6}", p.FechaDePublicacion,p.Descripcion, p.Receta.Titulo, p.Receta.Ingredientes, p.Receta.Procedimiento, p.Receta.Oculto, p.Receta.Id);
                            Console.WriteLine(ps);
                            Console.WriteLine("----------------------------------------");
                        }

                        break;
                }
            }while (bandera != true);

        }

        static void Valoraciones()
        {
            //Inicializar los repositorios con el mismo contexto
            var dbContext = new RecetarioDbContext();
            var recetaRepositorio = new RecetaRepository(dbContext);
            var categoriaRepositorio = new CategoriaRepository(dbContext);
            var valoracionRepositorio = new ValoracionRepository(dbContext);
            var recetaCtrl = new RecetaCtrl(recetaRepositorio,categoriaRepositorio,valoracionRepositorio);
            //Variables
            bool bandera = false;
            
            do
            {
                Console.WriteLine("VALORACIONES");
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Ver la valoración promedio de una receta");
                Console.WriteLine("2: Realizar una valoración a una receta");
                Console.Write("Seleccione una opción: ");
                int numero = int.Parse(Console.ReadLine());

                switch (numero)
                {
                    case 0://EXIT
                        bandera = true;
                        break;
                    case 1://Ver la valoracion promedio de una receta por id
                        Console.WriteLine("Calificación promedio de la receta");
                        Console.Write("Ingrese el id de la Receta:");
                        int idR = int.Parse(Console.ReadLine());
                        var recetaPromedio = recetaCtrl.GetById(idR);
                        Console.WriteLine("La calificación promedio es de <<{0}>> estrellas", recetaPromedio.ValoracionPromedio);
                        Console.WriteLine("\n");
                        break;
                    case 2://Insertar una valoracion a una receta
                        Console.WriteLine("Calificar una receta");
                        Console.Write("Ingresa el id de la Receta:");
                        int idRe = int.Parse(Console.ReadLine());
                        Console.Write("Ingresa el id del Usuario: ");
                        int idU = int.Parse(Console.ReadLine());
                        Console.Write("Ingresa la calificación(1-5): ");
                        double valor = double.Parse(Console.ReadLine());
                        var valorr = recetaCtrl.InsertValoracion(idRe, idU, valor);
                        if (valorr != null)
                        {
                            //recetaCtrl.InsertValoracion(idRe,idU,valor);
                            Console.WriteLine("------------ Calificación exitosa ------------");
                        }
                        else
                        {
                            Console.WriteLine("------------ No puede calificar la receta más de una vez ------------");
                        }
                        break;
                }
            }while (bandera != true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Domain.Entities
{
    public class Publicacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime FechaDePublicacion { get; set; }

        public string Descripcion { get; set; }

        //Foreing Key
        public int IdReceta { get; set; }

        public virtual Receta Receta { get; set; }

        public virtual List<Comentario> Comentarios { get; set; }
    }
}

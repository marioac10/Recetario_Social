using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Domain.Entities
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CuerpoDelComentario { get; set; }

        [Required]
        public DateTime FechaDelComentario { get; set; }
        
        //Foreign Key
        public int IdPublicacion { get; set; }

        //Foreign Key
        public int IdUsuario { get; set; }//Se hace referencia solo al ID del usuario 
    }
}

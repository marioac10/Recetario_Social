using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Domain.Entities
{
    public class Valoracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Foreign Key
        public int IdUsuario { get; set; }

        //Foreign Key
        public int IdReceta { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}

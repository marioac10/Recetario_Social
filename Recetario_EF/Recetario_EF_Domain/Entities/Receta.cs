using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recetario_EF_Domain.Entities
{
    public class Receta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        public byte[] Imagen { get; set; }

        [Required]
        public string Ingredientes { get; set; }

        [Required]
        public string Procedimiento { get; set; }

        [Required]
        public DateTime FechaDeCreacion { get; set; }

        [Required]
        public bool Oculto { get; set; }

        [NotMapped]//Propiedad calculada no mapeada
        public virtual double? ValoracionPromedio 
        {
            get
            {
                if(this.ValoracionesReceta != null && this.ValoracionesReceta.Count > 0)
                {
                    return this.ValoracionesReceta.Average(x => x.Valor);
                }
                return null;
            }  
        }

        //Foreign Key
        public int IdUsuario { get; set; }//Se hace referencia solo al ID del usuario

        public virtual List<Categoria> CategoriasDeReceta { get; set; }
      
        public virtual List<Valoracion> ValoracionesReceta { get; set; }
        
        
    }
}
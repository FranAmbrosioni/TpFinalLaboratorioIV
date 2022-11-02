using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalLaboratioIV.Models
{
    public class Actividad
    {
        
        public int  Id { get; set; }

        [Display (Name ="Actividad")]
        [Required(ErrorMessage ="El nombre de la actividad es requerido")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        
        [Display(Name = "Fotografia")]
        public string foto { get; set; }
    }
}

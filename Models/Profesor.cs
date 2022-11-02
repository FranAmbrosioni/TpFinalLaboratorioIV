using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TrabajoFinalLaboratioIV.Models
{
    public class Profesor
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime fechaNac { get; set; }

        [Display(Name ="Actividad a Cargo")]
        public int ActividadId { get; set; }

        public Actividad actividad { get; set; }

        [Display(Name ="Fotografia")]
        public string foto { get; set; }

        public int TurnoId { get; set; }

        public Turno turno { get; set; }

    }
}

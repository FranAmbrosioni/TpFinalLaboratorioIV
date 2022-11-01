using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace TrabajoFinalLaboratioIV.Models
{
    public class Alumno
    {

        public int Id { get; set; }

        public string  Nombre { get; set; }

        [Display(Name ="Año de Inscripcion")]
       // [Range(2019,2022,ErrorMessage = "El dato {0} debe ser igual o mayor a {1}")]
        public int  Inscripcion { get; set; }

       [Display(Name ="Correo Electrónico")]
        public string  Email { get; set; }

        [Display(Name ="Activo Si/No")]
        public bool Estado { get; set; }

        public DateTime fechaNac { get; set; }

        public int actividadId { get; set; }

        public Actividad actividad { get; set; }

        [Display(Name ="Turno")]
        public int turnoId { get; set; }

        public  Turno turno { get; set; }

        [Display(Name ="Fotografia")]
        public string foto { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabajoFinalLaboratioIV.Models
{
    public class appDBcontext : DbContext
    {

        public appDBcontext (DbContextOptions <appDBcontext> options) : base(options) 
        {
        
        }

        public DbSet<Alumno> alumnos { get; set; }

        public DbSet<Actividad> actividad { get; set; }

        public DbSet<Turno> turnos { get; set; }

        public DbSet<Profesor> profesores { get; set; }




    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajoFinalLaboratioIV.Models;

namespace TrabajoFinalLaboratioIV.ModelsViews
{
    public class AlumnoViewModel
    {
        public List<Alumno> MyProperty { get; set; }
        public SelectList ListaActividades { get; set; }

        public string busqTurno { get; set; }
        public string busqNombre { get; set; }

        public paginador paginador { get; set; }
    }
}

public class paginador
{
    //cantidad total de registros
    public int  cantReg { get; set; }

    //registros a filtrar por pag
    public int regXpag { get; set; }
    public int pagActual { get; set; }

    public int totalPag => (int)Math.Ceiling((decimal)cantReg / regXpag);
}

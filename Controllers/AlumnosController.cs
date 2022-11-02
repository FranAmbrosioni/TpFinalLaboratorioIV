using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalLaboratioIV.Models;

namespace TrabajoFinalLaboratioIV.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly appDBcontext _context;
        private readonly IWebHostEnvironment _env;

        public AlumnosController(appDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //public FileResult Exportar()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("Id; ")
        //}
        // GET: Alumnoes
        public async Task<IActionResult> Index()
        {
            var appDBcontext = _context.alumnos.Include(a => a.actividad).Include(a => a.turno);
            return View(await appDBcontext.ToListAsync());
        }

        // GET: Alumnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos
                .Include(a => a.actividad)
                .Include(a => a.turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnoes/Create
        public IActionResult Create()
        {
            ViewData["actividadList"] = new SelectList(_context.actividad, "Id", "Nombre");
            ViewData["turnolist"] = new SelectList(_context.turnos, "Id", "Nombre");
            return View();
        }

        // POST: Alumnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Inscripcion,Email,Estado,fechaNac,actividadId,turnoId,foto")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;

                if (archivos != null && archivos.Count > 0)
                {

                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(_env.WebRootPath, "fotos");

                    if (archivoFoto.Length > 0)
                    {

                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);
                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            alumno.foto = archivoDestino;
                        };

                    }

                }

                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["actividadId"] = new SelectList(_context.actividad, "Id", "Nombre", alumno.actividadId);
            //ViewData["turnoId"] = new SelectList(_context.turnos, "Id", "Nombre", alumno.turnoId);
            return View(alumno);
        }

        // GET: Alumnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["actividadList"] = new SelectList(_context.actividad, "Id", "Nombre", alumno.actividadId);
            ViewData["turnoList"] = new SelectList(_context.turnos, "Id", "Nombre", alumno.turnoId);
            return View(alumno);
        }

        // POST: Alumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Inscripcion,Email,Estado,fechaNac,actividadId,turnoId,foto")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos != null && archivos.Count > 0)
                    {
                        var archivoFoto = archivos[0];
                        var pathDestino = Path.Combine(_env.WebRootPath, "fotos");

                        if (archivoFoto.Length > 0)
                        {

                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(alumno.foto))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, alumno.foto);


                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);

                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                alumno.foto = archivoDestino;
                            }

                        }

                    }

                    _context.Update(alumno);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["actividadId"] = new SelectList(_context.actividad, "Id", "Nombre", alumno.actividadId);
            ViewData["turnoId"] = new SelectList(_context.turnos, "Id", "Nombre", alumno.turnoId);
            return View(alumno);
        }

        // GET: Alumnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos
                .Include(a => a.actividad)
                .Include(a => a.turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.alumnos.FindAsync(id);
            _context.alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.alumnos.Any(e => e.Id == id);
        }
    }
}

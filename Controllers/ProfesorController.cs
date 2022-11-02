using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalLaboratioIV.Models;
using System.IO;
namespace TrabajoFinalLaboratioIV.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly appDBcontext _context;
        private readonly IWebHostEnvironment env;
        public ProfesorController(appDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Profesors
        public async Task<IActionResult> Index()
        {
            return View(await _context.profesores.Include(p => p.actividad).Include(p => p.turno).ToListAsync());
        }

        // GET: Profesors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores
                .Include(p => p.actividad)
                .Include(p => p.turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesors/Create
        public IActionResult Create()
        {
            ViewData["ActividadList"] = new SelectList(_context.actividad, "Id", "Nombre");
            ViewData["TurnoList"] = new SelectList(_context.turnos, "Id", "Nombre");
            return View();
        }

        // POST: Profesors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,fechaNac,ActividadId,foto")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos !=null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(env.WebRootPath, "fotos");

                    if (archivoFoto.Length > 0)
                    {

                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);
                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            profesor.foto = archivoDestino;
                        };

                    }

                }

                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            }
            //ViewData["ActividadId"] = new SelectList(_context.actividad, "Id", "Nombre", profesor.ActividadId);
            return View(profesor);
        }

        // GET: Profesors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            ViewData["ActividadId"] = new SelectList(_context.actividad, "Id", "Nombre", profesor.ActividadId);
            return View(profesor);
        }

        // POST: Profesors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,fechaNac,ActividadId,foto")] Profesor profesor)
        {
            if (id != profesor.Id)
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
                        var pathDestino = Path.Combine(env.WebRootPath, "fotos");

                        if (archivoFoto.Length > 0)
                        {

                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(profesor.foto))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, profesor.foto);


                                if (System.IO.File.Exists(fotoAnterior)) 
                                 System.IO.File.Delete(fotoAnterior);
                                
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                profesor.foto = archivoDestino;
                            }

                        }

                    }
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["ActividadId"] = new SelectList(_context.actividad, "Id", "Nombre", profesor.ActividadId);
            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "Nombre", profesor.TurnoId);
            return View(profesor);
        }

        // GET: Profesors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores
                .Include(p => p.actividad)
                .Include(p => p.turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.profesores.FindAsync(id);
            _context.profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.profesores.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaMVC.Models;

namespace PracticaMVC.Controllers
{
    public class EquiposController : Controller
    {
        private readonly equiposDbContext _equiposDbContext;

        public EquiposController(equiposDbContext equiposDbContext)
        {
            _equiposDbContext = equiposDbContext;
        }

        public IActionResult Index()
        {
            try
            {
                // Cargar marcas para el dropdown
                var listaDeMarcas = _equiposDbContext.marcas.ToList();
                ViewData["ListadoDeMarcas"] = new SelectList(listaDeMarcas, "id_marcas", "nombre_marca");

                var listadoDeEquipos = (from e in _equiposDbContext.equipos
                                        join m in _equiposDbContext.marcas on e.marca_id equals m.id_marcas
                                        select new
                                        {
                                            nombre = e.nombre,
                                            descripcion = e.descripcion,
                                            marca_id = e.marca_id,
                                            marca_nombre = m.nombre_marca
                                        }).ToList();

                ViewData["ListadoEquipo"] = listadoDeEquipos;

                return View();
            }
            catch (Exception ex)
            {
                // Manejo de errores - puedes loggear el error aquí
                ViewData["ErrorMessage"] = "Error al cargar los datos: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearEquipos([Bind("nombre,descripcion,marca_id")] equipos nuevoEquipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _equiposDbContext.equipos.Add(nuevoEquipo);
                    _equiposDbContext.SaveChanges();
                    TempData["SuccessMessage"] = "Equipo guardado correctamente";
                    return RedirectToAction(nameof(Index));
                }

                // Si hay errores de validación, recargamos los datos necesarios
                var listaDeMarcas = _equiposDbContext.marcas.ToList();
                ViewData["ListadoDeMarcas"] = new SelectList(listaDeMarcas, "id_marcas", "nombre_marca");

                return View("Index", nuevoEquipo);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                TempData["ErrorMessage"] = "Error al guardar el equipo: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
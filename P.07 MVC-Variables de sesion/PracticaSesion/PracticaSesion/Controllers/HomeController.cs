using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaSesion.Models;

namespace PracticaSesion.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly equiposDbContext _context;
    public HomeController(ILogger<HomeController> logger, equiposDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()

    {
     
        var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
        var nombreUsuario = HttpContext.Session.GetString("Nombre");

        
        ViewBag.nombre = nombreUsuario;
        ViewData["TipoUsuario"] = tipoUsuario;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Autenticar()
    {

        ViewData["ErrorMessage"] = "";
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Autenticar(string txtUsuario, string txtClave)
    {
        var usuario = (from u in _context.usuarios
                       where u.email == txtUsuario
                       && u.contrasenia == txtClave
                       && u.activo == "S"
                       && u.bloqueado == "N"
                       select u).FirstOrDefault();

        if (usuario != null)
        {
            HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);
            HttpContext.Session.SetString("TipoUsuario", usuario.tipo_usuario);
            HttpContext.Session.SetString("Nombre", usuario.nombre);

            return RedirectToAction("Index", "Home");
        }

        ViewData["ErrorMessage"] = "Error, usuario inválido!!!!";
        return View();
    }
}

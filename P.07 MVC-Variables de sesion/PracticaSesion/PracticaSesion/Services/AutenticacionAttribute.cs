using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PracticaSesion.Services
{
    public class AutenticacionAttribute : ActionFilterAttribute
    {
        //metodo ejecutado antes que se ejecuta el controller
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Verifica si la variable de sesion "UsuarioId" existe
            var usuarioId = context.HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                //Sino esta autenticado redirige a la pagina de login
                context.Result = new RedirectToActionResult("Autenticar", "Home", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

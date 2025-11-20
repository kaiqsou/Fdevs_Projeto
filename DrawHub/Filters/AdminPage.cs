using DrawHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DrawHub.Filters
{
    public class AdminPage : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessao = context.HttpContext.Session.GetString("sessaoLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessao);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if (usuario.Tipo != Enums.RoleEnum.Administrador)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restricao" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

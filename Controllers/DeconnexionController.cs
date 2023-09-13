using Microsoft.AspNetCore.Mvc;
using BanqueNET.Extensions;

namespace BanqueNET.Controllers 
{
    public class DeconnexionController : Controller
    {
        [Route("deconnexion")]
        public IActionResult Deconnexion()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
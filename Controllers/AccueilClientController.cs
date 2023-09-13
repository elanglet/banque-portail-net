using Microsoft.AspNetCore.Mvc;
using BanqueNET.Models;
using BanqueNET.Extensions;
using BanqueNET.Service;

namespace BanqueNET.Controllers 
{
    
    public class AccueilClientController : Controller
    {
        [Route("accueilclient")]
        public IActionResult AccueilClient() 
        {
            var client = HttpContext.Session.Get<Client>("leclient");
            if(client == null)     
                return RedirectToAction(actionName: "Authentifier", controllerName: "Authentification");
            return View("ClientAccueilView", client);
        }    
    }
}

using Microsoft.AspNetCore.Mvc;
using BanqueNET.Models;
using BanqueNET.Service;
using BanqueNET.Extensions;

namespace BanqueNET.Controllers 
{
    public class ComptesController : Controller
    {
        private readonly IBanqueService _banqueService;

        public ComptesController(IBanqueService banqueService) 
        {
            _banqueService = banqueService;
        }

        [Route("comptes")]
        public IActionResult ObtenirComptes() 
        {
            var client = HttpContext.Session.Get<Client>("leclient");
            if(client == null)     
                return RedirectToAction(actionName: "Authentifier", controllerName: "Authentification");
            var listeDesComptes = _banqueService.mesComptes(client.Id);
            return View("ComptesView", listeDesComptes);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using BanqueNET.Models;
using BanqueNET.Extensions;
using BanqueNET.Service;

namespace BanqueNET.Controllers 
{
    public class VirementController : Controller
    {
        private readonly IBanqueService _banqueService;

        public VirementController(IBanqueService banqueService) 
        {
            _banqueService = banqueService;
        }

        [Route("virement")]
        public IActionResult FaireUnVirement() 
        {
            var client = HttpContext.Session.Get<Client>("leclient");
            if(client == null)     
                return RedirectToAction(actionName: "Authentifier", controllerName: "Authentification");
            var listeDesComptes = _banqueService.mesComptes(client.Id);
            return View("VirementView", new VirementModel(listeDesComptes));
        }

        [Route("virement")]
        [HttpPost]
        public IActionResult TraiterLeVirement(VirementModel virementModel) 
        {
            var client = HttpContext.Session.Get<Client>("leclient");
            if(client == null)     
                return RedirectToAction(actionName: "Authentifier", controllerName: "Authentification");
            
            _banqueService.virementEntreComptes(
                virementModel.NumeroCompteADebiter, 
                virementModel.NumeroCompteACrediter, 
                virementModel.Montant
            );
            return RedirectToAction(actionName: "ObtenirComptes", controllerName: "Comptes");
        }
    }
}
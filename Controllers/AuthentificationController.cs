using Microsoft.AspNetCore.Mvc;
using BanqueNET.Models;
using BanqueNET.Service;
using BanqueNET.Extensions;

namespace BanqueNET.Controllers 
{
    public class AuthentificationController : Controller
    {
        private readonly IBanqueService _banqueService;

        public AuthentificationController(IBanqueService banqueService) 
        {
            _banqueService = banqueService;
        }

        [Route("authentification")]
        public IActionResult Authentifier([FromQuery(Name = "error")] bool error)
        {
            if(!error)    
                return View("AuthentificationView", new AuthentificationModel());

            return View("AuthentificationView", new AuthentificationModel(true));
        }


        [Route("authentification")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authentifier(AuthentificationModel authentificationModel)
        {
            try
            {
                var client = _banqueService.authentifier(authentificationModel.Identifiant, authentificationModel.MotDePasse);
                HttpContext.Session.Set<Client>("leclient", client);

                return View("../AccueilClient/ClientAccueilView", client);  
            }
            catch(KeyNotFoundException)
            {
                return Redirect("authentification?error=true");
            }

        }
    }
}
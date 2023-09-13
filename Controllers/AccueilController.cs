using Microsoft.AspNetCore.Mvc;
using BanqueNET.Models;
using BanqueNET.Dal;
using BanqueNET.Service;

namespace BanqueNET.Controllers 
{
    public class AccueilController : Controller
    {
        [Route("/")]
        public IActionResult Index() 
        {
            return View("Index");
        }   
    }
}
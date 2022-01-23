using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers
{
    public class PokemonController : Controller
    {
        // GET: PokemonController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PokemonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }        
    }
}

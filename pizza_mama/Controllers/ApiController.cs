using Microsoft.AspNetCore.Mvc;
using pizza_mama.Models;
using pizza_mama.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pizza_mama.Controllers
{
    [Route("[controller]")]
    [ApiController]

    
    public class ApiController : Controller
    {
        DataContext context;

        public ApiController(pizza_mama.Data.DataContext context)
        {
            this.context = context;
        }
        // GET: api/getpizzas

        [HttpGet]
        [Route("GetPizzas")]
        public IActionResult GetPizzas()
        {
            //var pizza = new Pizza() { nom = "pizzaz test", prix = 8, vegetarienne = false, ingredients = "tomate, oignon, oeuf" };

            var pizzas = context.Pizzas.ToList();

            return Json(pizzas);
        }
    }
}

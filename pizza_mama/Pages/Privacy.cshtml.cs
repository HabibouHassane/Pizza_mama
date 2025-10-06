using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pizza_mama.Data;
using pizza_mama.Models;

namespace pizza_mama.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly DataContext _dataContext;

        public PrivacyModel(ILogger<PrivacyModel> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;

        }

        public void OnGet()
        {
            //var pizza = new Pizza() { nom = "Pizza test", prix = 5, vegetarienne = true, ingredients = "" };
            //_dataContext.Pizzas.Add(pizza);
            //_dataContext.SaveChanges();
        }
    }

}

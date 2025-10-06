using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Models;

namespace pizza_mama.Pages
{
    public class MenuPizzaModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext context;
        public IList<Pizza> Pizza { get; set; } = default;

        public MenuPizzaModel(pizza_mama.Data.DataContext context)
        {
            this.context = context;
        }
        public async Task OnGetAsync()
        {
            Pizza = await context.Pizzas.ToListAsync();
            Pizza = Pizza.OrderBy(p => p.prix).ToList();
        }
    }
}

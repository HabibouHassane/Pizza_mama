using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool isCorrect { get; private set; } = true;
        private readonly IConfiguration configuration;

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false) 
            {
                return Redirect("/admin/pizzas");   
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string username, string password, string ReturnUrl) 
        {
            var autSection = configuration.GetSection("Auth");

            string adminLogin = autSection["AdminLogin"];
            string adminPassword = autSection["AdminPassword"];

            if ((username == adminLogin) && (password == adminPassword))
            {
                isCorrect = true;
                var claims = new List<Claim>
                 {
                 new Claim(ClaimTypes.Name, username)
                 };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
               ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/admin/pizzas" : ReturnUrl);
            }
            isCorrect = false;
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/admin");
        }
    }
}

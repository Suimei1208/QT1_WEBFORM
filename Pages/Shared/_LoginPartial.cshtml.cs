using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QT1_WEB.Pages.Shared
{
    public class _LoginPartialModel : PageModel
    {
        public bool IsUserLoggedIn { get; set; }

        public void OnGet()
        {
            IsUserLoggedIn = User.Identity.IsAuthenticated;
        }
    }
}

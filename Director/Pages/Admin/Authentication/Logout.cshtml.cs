using Director.Utillity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Director.Pages.Admin.Authentication
{
    public class LogoutModel : PageModel
    {

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDetails.SessionToken, "");
            return Redirect("/admin/Home/Index");
        }

    }
}

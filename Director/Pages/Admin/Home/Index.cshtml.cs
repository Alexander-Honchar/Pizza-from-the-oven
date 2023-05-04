using Director.Models;
using Director.Services.GenericServiceis;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Director.Pages.Admin.Home
{
    [Authorize(Roles = StaticDetails.DirectorRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Director.Models;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Director.Pages.Admin.Workers
{
    [Authorize(Roles = StaticDetails.DirectorRole)]
    [BindProperties]
    public class IndexModel : PageModel
    {
        readonly IMetods _metods;
        public IEnumerable<WorkerDTO> Workers { get; set; }

		public IndexModel(IMetods metods)
        {
            _metods= metods;
            Workers = new List<WorkerDTO>();
        }

        public async Task OnGet()
        {
            Workers=await _metods.GetAllAsync<WorkerDTO>();
        }
    }
}

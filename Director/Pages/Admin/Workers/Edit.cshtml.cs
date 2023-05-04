using Director.Models;
using Director.Services.GenericServiceis;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Director.Pages.Admin.Workers
{
    [Authorize(Roles = StaticDetails.DirectorRole)]
    [BindProperties]
    public class EditModel : PageModel
    {
        readonly IGenericServices<WorkerDTO> _workerServices;
        readonly IMetods _metods;


        public WorkerDTO Worker { get; set; }


        public EditModel(IMetods metods, IGenericServices<WorkerDTO> workerServices)
        {
            _metods= metods;
            _workerServices= workerServices;    
            Worker= new WorkerDTO();
        }


        public async Task OnGet(string id)
        {
            Worker=await  _metods.GetOneAsync<WorkerDTO>(id);
            Worker.RoleList =await GetAllRolesAsync();
        }



        public async Task<IActionResult> OnPost() 
        {
            
            if (ModelState.IsValid)
            {
                var response = await _workerServices.UpdateAsync<APIResponse>(Worker);
                if (response.IsSuccess)
                {
                    TempData["Success"] = "Изменения добавлены в базу";
                    return RedirectToPage("Index");
                }

                TempData["Error"] = response.ErrorsMessages[0].ToString();
                return RedirectToPage("");
            }
            TempData["Error"] = "Форма заполнена не корекно";
            return RedirectToPage("");
        }


        #region Metods

        private async Task<IEnumerable<SelectListItem>> GetAllRolesAsync()
        {
            IEnumerable<SelectListItem> result = new List<SelectListItem>();

            var lislRoles = await _metods.GetListRolesAsync();
            if (lislRoles != null)
            {

                result = lislRoles.Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                

                return result;
            }

            return null;
        }

        #endregion
    }
}

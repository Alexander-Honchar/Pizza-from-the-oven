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
    public class DeleteModel : PageModel
    {
        readonly IGenericServices<WorkerDTO> _workerServices;
        readonly IMetods _metods;

        
        public WorkerDTO Worker { get; set; }


        public DeleteModel(IMetods metods, IGenericServices<WorkerDTO> workerServices)
        {
            _metods = metods;
            _workerServices = workerServices;
            Worker = new WorkerDTO();
        }


        public async Task OnGet(string id)
        {

            Worker = await GetOneWorker(id);
        }



        public async Task<IActionResult> OnPost()
        {

            if (Worker.Id!=null)
            {
                var response = await _workerServices.DeleteAsync<APIResponse>(Worker.Id);
                if (response.IsSuccess)
                {
                    TempData["Success"] = "Сотрудник удален";
                    return RedirectToPage("Index");
                }

                TempData["Error"] = response.ErrorsMessages[0].ToString();
                return RedirectToPage("");
            }
            TempData["Error"] = "Ошибка.Что то пошло не так";
            return RedirectToPage("");
        }


        #region Metods

        private async Task<WorkerDTO> GetOneWorker(string id)
        {
            if (id!=null)
            {
                var workers = await _metods.GetAllAsync<WorkerDTO>();
                var worker=workers.FirstOrDefault(u=>u.Id==id);
                return worker;
            }
            
            return null;
        }

        #endregion
    }
}

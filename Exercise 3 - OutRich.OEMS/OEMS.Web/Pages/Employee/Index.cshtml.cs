using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using OEMS.Web.Extensions;
using OEMS.Web.Models;
using X.PagedList;
using OEMS.Application.ApplicationServices.Employee;
using OEMS.Application.Models.Employee;
using OEMS.Application;

namespace OEMS.Web.Pages.Employee
{   
    public class IndexModel : BasePageModel
    {
        private readonly EmployeeService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public IndexModel(EmployeeService service, IOptions<OEMSWebConfig> appSetting, 
            ILogger<IndexModel> logger, ICorrelationContextAccessor correlationContext) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public IPagedList<EmployeeModel> EmployeeList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }
        [BindProperty]
        public EmployeeModel Employee { get; set; }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetInitializeListAsync()
        {
            try
            {
                await GetEmployeeListAsync();
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetInitializeListAsync));
            }
            return Partial("_List", this);
        }
    
        public IActionResult OnGetShowCreate()
        {
            return Partial("_Create", new EmployeeModel());
        }

        public async Task<IActionResult> OnGetShowEdit(int id)
        {
            await GetRecordAsync(id);
            return Partial("_Edit", Employee);
        }

        public async Task<IActionResult> OnGetShowView(int id)
        {
            await GetRecordAsync(id);
            return Partial("_View", Employee);
        }

        public async Task<IActionResult> OnGetShowDelete(int id)
        {
            await GetRecordAsync(id);
            return Partial("_Delete", Employee);
        }

        public async Task<IActionResult> OnPostSave()
        {
            try
            {
                this.ValidateModelState();
                await SaveUpdateEmployeeAsync();
                TempData[PromptContainerMessageTempDataName.Success] = Resource.PromptMessageSaveSuccess;
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostSave), Employee);            
            }
            return Partial("_Create", Employee);
        }    

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                this.ValidateModelState();
                await SaveUpdateEmployeeAsync();
                TempData[PromptContainerMessageTempDataName.Success] = Resource.PromptMessageUpdateSuccess;
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostUpdateAsync), Employee);          
            }
            return Partial("_Edit", Employee);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await DeleteEmployeeAsync(id);
                TempData[PromptContainerMessageTempDataName.Success] = Resource.PromptMessageDeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostDeleteAsync), Employee);              
            }
            await GetEmployeeListAsync();
            return Page();
        }

        private async Task GetEmployeeListAsync()
        {
            var employeeList = await _service.GetEmployeeListAsync(SearchKey, OrderBy, SortBy, PageNumber, PageSize);
            EmployeeList = new StaticPagedList<EmployeeModel>(employeeList.Items, 
                employeeList.PagedListMetaData.PageNumber, employeeList.PagedListMetaData.PageSize, 
                employeeList.PagedListMetaData.TotalItemCount);
        }

        private async Task SaveUpdateEmployeeAsync()
        {
            if (Employee.Id == 0)
            {
                Employee = await _service.SaveEmployeeAsync(Employee);
            }
            else
            {
                Employee = await _service.UpdateEmployeeAsync(Employee);
            }          
        }

        private async Task GetEmployeeItemAsync(int id)
        {
            Employee = await _service.GetEmployeeItemAsync(id);
        }              

        private async Task DeleteEmployeeAsync(int id)
        {
            await _service.DeleteEmployeeAsync(id);
        }

        private async Task GetRecordAsync(int id)
        {
            try
            {
                await GetEmployeeItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(GetRecordAsync), Employee);
            }
        }
    }
}

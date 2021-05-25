using OEMS.Application;
using OEMS.Web.Extensions;
using OEMS.Web.Models;
using System.Collections.Generic;

namespace OEMS.Web.Pages.Employee
{
    public static class EmployeeHtmlElementVariables
    {     
        public const string EmployeeForm = "formEmployeeForm";
        public static PromptContainer EmployeeFormPromptContainer = new PromptContainer(name: "promptEmployeeForm", effects: "Blink");
        public static PromptContainer EmployeeListingsPromptContainer = new PromptContainer(name: "promptEmployeeListings", effects: "Blink");
        public const string EmployeeListingsContainer = "containerEmployeeListings";

        public static readonly FormModal EmployeeModal = new FormModal(name: "EmployeeModal", width: 700, isDraggable: true);
        public static readonly PageHandler ShowCreateHandler = new PageHandler("ShowCreate", Resource.LabelAddEmployee);
        public static readonly PageHandler ShowEditHandler = new PageHandler(name: "ShowEdit", description: Resource.LabelEditEmployee, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler ShowViewHandler = new PageHandler(name: "ShowView", description: Resource.LabelDetailsEmployee, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler ShowDeleteHandler = new PageHandler(name: "ShowDelete", description: Resource.LabelDeleteEmployee, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler SaveHandler = new PageHandler(name: "Save", withPromptConfirmation: true);
        public static readonly PageHandler UpdateHandler = new PageHandler(name: "Update", withPromptConfirmation: true);
        public static readonly PageHandler DeleteHandler = new PageHandler(name: "Delete", withPromptConfirmation: true);
        public static readonly PageHandler InitializeListHandler = new PageHandler(name: "InitializeList");
    }
}


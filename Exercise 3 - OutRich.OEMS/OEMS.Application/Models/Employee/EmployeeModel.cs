using System.ComponentModel.DataAnnotations;
using System;

namespace OEMS.Application.Models.Employee
{
    public class EmployeeModel : BaseModel
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PromptMessageFieldIsRequired")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PromptMessageFieldIsRequired")]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PromptMessageFieldIsRequired")]
        public DateTime BirthDate { get; set; }
        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEMS.Web.Filters;
namespace OEMS.Web.Controller
{
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    [AuthorizeApiKey]
    public class BaseController : ControllerBase
    {      
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace OEMS.Application.ApplicationServices
{
    public class BaseApplicationService
    {  
       protected string _userName { get; set; }
        protected string _userId { get; set; }
        protected IMediator _mediator { get; set; }
        protected ClaimsPrincipal _claims { get; set; }
        public BaseApplicationService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext)
        {           
            var user = userManager.GetUserAsync(httpContext.HttpContext.User).Result;
            _userName = user?.UserName;     
            _mediator = mediator;
            _claims = httpContext.HttpContext.User;
            _userId = user?.Id;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using OEMS.Data;
using System;
using System.Linq;
namespace OEMS.Web.Filters
{
    public class AuthorizeApiKeyAttribute : Attribute, IAuthorizationFilter
    { 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<OEMSContext>();          
            var token = dbContext.OEMSApiClient
             .Where(l => "Bearer " + l.Token == (context.HttpContext.Request.Headers["Authorization"]).ToString()).FirstOrDefault();
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }               
        }
    }
}

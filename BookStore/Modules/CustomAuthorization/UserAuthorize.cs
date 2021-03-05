using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BookStore.TokenGenerators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Modules.CustomAuthorization
{
    public class UserAuthorize
    {

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    context.Result = new JsonResult(new { message = "Unauthorized" })
        //    { StatusCode = StatusCodes.Status401Unauthorized };
        //}
        // Attribute, IAuthorizationFilter
    }
}
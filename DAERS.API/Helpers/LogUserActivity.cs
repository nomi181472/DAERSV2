using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DAERS.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace DAERS.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext=await next();
            var userID=int.Parse(resultContext.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo= resultContext.HttpContext.RequestServices.GetService<IDaersRepository>();
            var user= await repo.GetUser(userID);
            user.LastActive=DateTime.Now;
            await repo.SaveAll();


        }
    }
}
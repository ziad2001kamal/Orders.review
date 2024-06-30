using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Validations.Rules;
using Orders.API.Constant;
using Orders.Core.ViewModels;
using System.IdentityModel.Tokens.Jwt;
namespace Orders.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        protected string UserId;
        protected string language;
        protected async Task<APIResponseViewModel> GetResponse(object data )
        {
            var response = new APIResponseViewModel(true, "Done", data);
            return response;
        }

        protected async Task<APIResponseViewModel> GetResponse(object data, string message)
        {
            var response = new APIResponseViewModel(true, message, data);
            return response;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (User.Identity.IsAuthenticated)
            {
                UserId = User.FindFirst(Claims.UserId).Value;
            }
            language = Thread.CurrentThread.CurrentUICulture.Name;
        }
    }
}
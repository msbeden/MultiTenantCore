using Multiple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Multiple.Configurations.Authorization
{
    /*
    public class AuthorizationFilter : IActionFilter
    {
        private readonly IYetkilendirmeFiltreServisi _yetkilendirmeFiltreServisi;
        private readonly ITokenYonetimServisi _tokenYonetimServisi;
        public AuthorizationFilter(IYetkilendirmeFiltreServisi yetkilendirmeFiltreServisi, ITokenYonetimServisi tokenYonetimServisi)
        {
            this._yetkilendirmeFiltreServisi = yetkilendirmeFiltreServisi;
            this._tokenYonetimServisi = tokenYonetimServisi;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string jwtToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            ObjectResult failedResult = new ObjectResult(context.ModelState) { Value = "Not Authorized", StatusCode = StatusCodes.Status401Unauthorized };
            try
            {
                string metodAdi = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                string controllerAdi = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName;

                var tokenResponse = this._tokenYonetimServisi.ValidateToken(jwtToken);

                TimeSpan? tokenExpiryDiff = (tokenResponse.Entity?.GecerlilikTarihi - DateTime.UtcNow);
                if (!tokenExpiryDiff.HasValue)
                {
                    context.Result = failedResult;
                    return;
                }

                bool isExpired = tokenExpiryDiff.Value.TotalSeconds < 1;
                if (!tokenResponse.IsSuccessful || isExpired)
                {
                    context.Result = failedResult;
                    return;
                }

                ServiceResponse<bool> response = this._yetkilendirmeFiltreServisi.UserHasYetki(tokenResponse.Entity.UserId, controllerAdi, metodAdi);
                if (!response.Entity)
                {
                    context.Result = failedResult;
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    */
}

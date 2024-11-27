using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Temp.Components.Extensions;
using Temp.Components.Notifications;
using Temp.Components.Security;
using Temp.Resources;
using Temp.Services;

namespace Temp.Controllers;

[AllowUnauthorized]
public class Home : ServicedController<AccountService>
{
    public Home(AccountService service)
        : base(service)
    {
    }

    [HttpGet]
    public ActionResult Index()
    {
        if (!Service.IsActive(User.Id()))
            return RedirectToAction(nameof(Auth.Logout), nameof(Auth));

        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult Error()
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            Alerts.Add(new Alert
            {
                Id = "SystemError",
                Type = AlertType.Danger,
                Message = Resource.ForString("SystemError", HttpContext.TraceIdentifier)
            });

            return Json(new { alerts = Alerts });
        }

        return View();
    }

    [AllowAnonymous]
    public new ActionResult NotFound()
    {
        if (Service.IsLoggedIn(User) && !Service.IsActive(User.Id()))
            return RedirectToAction(nameof(Auth.Logout), nameof(Auth));

        Response.StatusCode = StatusCodes.Status404NotFound;

        return View();
    }
}

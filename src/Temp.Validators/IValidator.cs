using Microsoft.AspNetCore.Mvc.ModelBinding;
using Temp.Components.Notifications;

namespace Temp.Validators;

public interface IValidator : IDisposable
{
    Alerts Alerts { get; set; }
    ModelStateDictionary ModelState { get; set; }
}

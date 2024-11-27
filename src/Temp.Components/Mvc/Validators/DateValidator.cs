using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Temp.Resources;

namespace Temp.Components.Mvc;

public class DateValidator : IClientModelValidator
{
    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes["data-val-date"] = Validation.For("DateTime", context.ModelMetadata.GetDisplayName());
    }
}

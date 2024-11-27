using Microsoft.AspNetCore.Mvc;
using Temp.Components.Lookups;
using Temp.Components.Security;
using Temp.Data;
using Temp.Objects;
using NonFactors.Mvc.Lookup;

namespace Temp.Controllers;

[AllowUnauthorized]
public class Lookup : AController
{
    private IUnitOfWork UnitOfWork { get; }

    public Lookup(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    [HttpGet]
    public JsonResult Role(LookupFilter filter)
    {
        return Json(new MvcLookup<Role, RoleView>(UnitOfWork) { Filter = filter }.GetData());
    }

    protected override void Dispose(Boolean disposing)
    {
        UnitOfWork.Dispose();

        base.Dispose(disposing);
    }
}

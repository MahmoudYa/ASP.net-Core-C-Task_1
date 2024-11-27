using Temp.Data;

namespace Temp.Services;

public abstract class AService : IService
{
    protected IUnitOfWork UnitOfWork { get; }

    protected AService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public void Dispose()
    {
        UnitOfWork.Dispose();
    }
}

using AutoMapper;
using Temp.Objects.Models;
using Temp.Objects.Views;

namespace Temp.Objects.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductView>();
        Object[] profile = { this };
        Type[] views = GetType()
            .Assembly
            .GetTypes()
            .Where(type =>
                type.BaseType?.IsGenericType == true &&
                type.BaseType?.GetGenericTypeDefinition() == typeof(AView<>))
            .ToArray();

        foreach (Type view in views)
            view.GetMethod(nameof(AView<AModel>.Map), BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(Activator.CreateInstance(view), profile);
    }
}

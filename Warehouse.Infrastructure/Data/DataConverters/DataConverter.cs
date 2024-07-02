using System.Reflection;

namespace Warehouse.Infrastructure.Data.DataConverters;

internal static class DataConverter
{
    private static ArgumentException ConversionException(Type destination) =>
        new($"Problem while converting to type {destination.Name}");

    public static TDomainModel ConvertToDomainModel<TDomainModel>(params object[] ctorParams) where TDomainModel : class
    {
        return Activator.CreateInstance(
                   typeof(TDomainModel),
                   BindingFlags.CreateInstance | BindingFlags.NonPublic,
                   null,
                   ctorParams) as TDomainModel
               ?? throw ConversionException(typeof(TDomainModel));
    }
}
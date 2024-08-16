namespace Warehouse.Infrastructure.Data.Options;

internal sealed class MissingConnectionStringException : ArgumentNullException
{
    public MissingConnectionStringException(string connectionStringName) :
        base($"{connectionStringName} connection string is missing")
    {
            
    }
}
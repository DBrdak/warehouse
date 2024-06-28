using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Warehouse.Infrastructure.Data.Options;

internal sealed class ApplicationDbContextOptionsSetup : IConfigureOptions<ApplicationDbContextOptions>
{
    private readonly IConfiguration _configuration;
    private const string defaultConnectionStringName = "Default";

    public ApplicationDbContextOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ApplicationDbContextOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString(defaultConnectionStringName) 
                                   ?? throw new MissingConnectionStringException(defaultConnectionStringName);
    }
}
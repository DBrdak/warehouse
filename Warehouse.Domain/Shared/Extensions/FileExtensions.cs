using System.Diagnostics;

namespace Warehouse.Domain.Shared.Extensions;

public static class FileExtensions
{
    public static void OpenFile(this string filePath)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true,
        };

        Process.Start(processStartInfo);
    }
}
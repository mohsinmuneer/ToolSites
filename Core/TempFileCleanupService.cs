namespace ToolSite.Core;
// Jo tools temp files banate hain unke liye: 15 minute se purani files delete.
public class TempFileCleanupService : BackgroundService
{
    public static readonly string Dir = Path.Combine(Path.GetTempPath(), "toolsite");
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        Directory.CreateDirectory(Dir);
        while (!ct.IsCancellationRequested)
        {
            try
            {
                foreach (var f in Directory.EnumerateFiles(Dir))
                    if (File.GetLastWriteTimeUtc(f) < DateTime.UtcNow.AddMinutes(-15)) File.Delete(f);
            }
            catch { /* log karna ho to ILogger lagao */ }
            await Task.Delay(TimeSpan.FromMinutes(5), ct);
        }
    }
}

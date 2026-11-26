namespace ToolSite.Core;
public class LimitsOptions
{
    public int DailyFreeQuota { get; set; } = 5;
    public int MaxFileMB { get; set; } = 10;
    public int MaxFiles { get; set; } = 10;
}

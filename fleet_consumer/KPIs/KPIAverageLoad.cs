using DTO.InputType;
using System.Text.Json;


namespace KPI;

public sealed class KpiAverageLoad : KPIBase<ITelemetry>
{
    private long _totalLoad;
    private int _count;
    public override string Name => "AverageLoad";

    public override void Calculate(ITelemetry t)
    {
        var load = t.DeliveryList?.Count ?? 0;
        Interlocked.Add(ref _totalLoad, load);
        Interlocked.Increment(ref _count);
    }

    // KpiAverageLoad
    protected override void CopyFromDeserializedObj(KPIBase<ITelemetry> other)
    {
        if (other is not KpiAverageLoad o) return;
        _totalLoad = o._totalLoad;
        _count = o._count;
    }

    public override async Task WriteMetricToFileAsync(string path)
    {
        var filePath = Path.Combine(path, "Day.txt");
        var avg = _count == 0 ? 0 : (double)_totalLoad / _count;
        var line = $"{Name}: {avg}{Environment.NewLine}";
        await File.AppendAllTextAsync(filePath, line);
    }
}

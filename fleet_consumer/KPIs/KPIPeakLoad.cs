using System.Text.Json;
using DTO.InputType;

namespace KPI;

public sealed class KpiPeakLoad : KPIBase<ITelemetry>
{
    private int _maxLoad;
    public override string Name => "PeakLoad";

    public override void Calculate(ITelemetry t)
    {
        var load = t.DeliveryList?.Count ?? 0;
        int initial, computed;
        do
        {
            initial = Volatile.Read(ref _maxLoad);
            if (load <= initial) return;
            computed = load;
        } while (Interlocked.CompareExchange(ref _maxLoad, computed, initial) != initial);
    }

    // KpiPeakLoad
    protected override void CopyFromDeserializedObj(KPIBase<ITelemetry> other)
    {
        if (other is not KpiPeakLoad o) return;
        _maxLoad = o._maxLoad;
    }

    public override async Task WriteMetricToFileAsync(string path)
    {
        var filePath = Path.Combine(path, "Day.txt");
        var line = $"{Name}: {_maxLoad}{Environment.NewLine}";
        await File.AppendAllTextAsync(filePath, line);
    }
}
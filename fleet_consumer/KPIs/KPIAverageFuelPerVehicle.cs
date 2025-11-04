using DTO.InputType;
using System.Text.Json.Serialization;
namespace KPI;

public sealed class KpiAverageFuelUsedPerVehicle : KPIBase<IGPS>
{
    [JsonInclude]
    private int _totalFuel;
    [JsonInclude]
    private int _count;
    public override string Name => "AverageFuelUsedPerVehicle";

    public override void Calculate(IGPS t)
    {
        // Convert to int once, then atomic add
        var inc = (int)t.FuelPct;
        Interlocked.Add(ref _totalFuel, inc);
        Interlocked.Increment(ref _count);
    }

    protected override void CopyFromDeserializedObj(KPIBase<IGPS> other)
    {
        if (other is not KpiAverageFuelUsedPerVehicle o) return;
        _totalFuel = o._totalFuel;
        _count = o._count;
    }

    public override async Task WriteMetricToFileAsync(string path)
    {
        var filePath = Path.Combine(path, "Day.txt");
        var avg = _count == 0 ? 0 : (double)_totalFuel / _count;
        var line = $"{Name}: {avg}{Environment.NewLine}";
        await File.AppendAllTextAsync(filePath, line);
    }
}

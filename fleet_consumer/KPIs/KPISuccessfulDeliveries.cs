using System.Text.Json;
using DTO.InputType;
using DTO;
namespace KPI;

public sealed class KpiSuccessfulDeliveries : KPIBase<ITelemetry>
{
    private long _successfulDeliveries;
    public override string Name => "SuccessfulDeliveries";

    public override void Calculate(ITelemetry t)
    {
        if (t.DeliveryStatus == DeliveryStatus.Completed)
            Interlocked.Increment(ref _successfulDeliveries);
    }
    // KpiSuccessfulDeliveries
    protected override void CopyFromDeserializedObj(KPIBase<ITelemetry> other)
    {
        if (other is not KpiSuccessfulDeliveries o) return;
        _successfulDeliveries = o._successfulDeliveries;
    }

    public override async Task WriteMetricToFileAsync(string path)
    {
        var filePath = Path.Combine(path, "Day.txt");
        var line = $"{Name}: {_successfulDeliveries}{Environment.NewLine}";
        await File.AppendAllTextAsync(filePath, line);
    }
}
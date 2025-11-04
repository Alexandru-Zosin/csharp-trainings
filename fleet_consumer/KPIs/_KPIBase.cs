using System.Text.Json;
using System.Text.Json.Serialization;
namespace KPI;

public abstract class KPIBase<T> : IKPI<T>
{
    public abstract string Name { get; }

    [JsonIgnore]
    public Type InputType => typeof(T);
    public abstract void Calculate(T telemetry);
    public void CalculateUntyped(object telemetry)
        => Calculate((T)telemetry);

    public virtual async Task SerializeAsync(string directoryPath)
    {   
        if (string.IsNullOrWhiteSpace(directoryPath))
            throw new ArgumentException("directoryPath is required.", nameof(directoryPath));

        Directory.CreateDirectory(directoryPath); // ensures folder exists

        var filePath = Path.Combine(directoryPath, $"{Name}.json");
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        var json = JsonSerializer.Serialize(this, GetType(), options);
        await File.WriteAllTextAsync(filePath, json);
    }

    public virtual async Task DeserializeFromFileAsync(string directoryPath)
    {
        var filePath = Path.Combine(directoryPath, $"{Name}.json");
        if (!File.Exists(filePath))
            return;

        var json = await File.ReadAllTextAsync(filePath);
        var options = new JsonSerializerOptions
        {
            IncludeFields = true
        };
        var obj = (KPIBase<T>?)JsonSerializer.Deserialize(json, GetType(), options);
        // gettype gets the type polymorphically
        // but deserialize returns an object that needs to be changed to something at compile time

        if (obj == null)
            return;

        CopyFromDeserializedObj(obj);
    }

    protected virtual void CopyFromDeserializedObj(KPIBase<T> other)
    {
        // Default does nothing,derived KPIs override to copy fields
    }

    public abstract Task WriteMetricToFileAsync(string path);
}
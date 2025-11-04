using System;
using System.Collections.Concurrent;

namespace KPI;

public interface IKPI
{
    Type InputType { get; }
    string Name { get; }
    void CalculateUntyped(object telemetry);
    Task SerializeAsync(string directoryPath);
    Task DeserializeFromFileAsync(string directoryPath);
    Task WriteMetricToFileAsync(string path);

}

public interface IKPI<in T> : IKPI // contravariance
{
    void Calculate(T telemetry);
}
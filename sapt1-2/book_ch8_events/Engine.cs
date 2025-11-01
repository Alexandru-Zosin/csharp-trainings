namespace Events;
using System;

public enum Status { Started, Stopped }

public sealed class EngineEventArgs : EventArgs
{
    public Status Status { get; }
    public EngineEventArgs(Status status)
    {
        Status = status;
    }
}

public delegate void StatusChange(object sender, EngineEventArgs args);

public class Engine
{
    public event StatusChange StatusChange;

    public void Start() {
        StatusChange?.Invoke(this, new EngineEventArgs(Status.Started));
    }
    public void Stop()
    {
        StatusChange?.Invoke(this, new EngineEventArgs(Status.Stopped));
    }
}
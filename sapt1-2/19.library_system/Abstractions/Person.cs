using System;
namespace LibraryCatalog.Abstractions;

public abstract class Person
{
    public Guid Guid { get; } // immutable after construction
    public string Name { get; }

    protected Person(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Invalid name. A name is required.");

        Guid = Guid.NewGuid();
        Name = name;
    }

    public abstract string GetDescription();
}
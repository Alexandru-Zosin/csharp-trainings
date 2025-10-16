using System;
namespace LibraryCatalog.Abstractions;

public sealed class TestInstantPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod() => TimeSpan.FromSeconds(1);
}
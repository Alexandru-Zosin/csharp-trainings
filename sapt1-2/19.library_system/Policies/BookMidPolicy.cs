using System;
namespace LibraryCatalog.Abstractions;

public sealed class BookMidPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(9);
    }
}
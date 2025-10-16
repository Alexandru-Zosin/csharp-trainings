using System;
namespace LibraryCatalog.Abstractions;

public sealed class BookLongPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(14);
    }
}
using System;
namespace LibraryCatalog.Abstractions;

public sealed class BookShortPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(4);
    }
}
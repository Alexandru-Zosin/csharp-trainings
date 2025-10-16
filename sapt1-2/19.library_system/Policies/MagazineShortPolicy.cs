using System;
namespace LibraryCatalog.Abstractions;

public sealed class MagazineShortPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(1);
    }
}
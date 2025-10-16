using System;
namespace LibraryCatalog.Abstractions;

public sealed class MagazineLongPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(3);
    }
}
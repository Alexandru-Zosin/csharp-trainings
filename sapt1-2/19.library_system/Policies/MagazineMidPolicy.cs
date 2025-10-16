using System;
namespace LibraryCatalog.Abstractions;

public sealed class MagazineMidPolicy : ILoanPolicy
{
    public TimeSpan GetLoanPeriod()
    {
        return TimeSpan.FromDays(2);
    }
}
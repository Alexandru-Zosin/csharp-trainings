using System;
namespace LibraryCatalog.Abstractions;

public interface ILoanPolicy
{
    public TimeSpan GetLoanPeriod();
}
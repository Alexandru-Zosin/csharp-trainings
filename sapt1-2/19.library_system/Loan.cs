namespace LibraryCatalog;
using LibraryCatalog.Abstractions;
using System;

public class Loan
{ 
    public ILoanable LoanedItem { get; }
    public Member Loaner { get; }
    public DateTimeOffset BorrowTime { get; }
    public DateTimeOffset? ReturnTime { get; private set; } = null;

    public Loan(ILoanable loanedItem, Member loaner)
    {
        LoanedItem = loanedItem;
        Loaner = loaner;
        BorrowTime = DateTimeOffset.UtcNow;
    }

    public void MarkReturned()
    {
        if (ReturnTime is not null)
            throw new InvalidOperationException("Loan already returned.");
        ReturnTime = DateTimeOffset.UtcNow;
    }

    public bool IsPastDue()
    {
        if (LoanedItem.DurationOfBorrow is not TimeSpan duration)
            return false;

        var effectiveTime = ReturnTime ?? DateTimeOffset.UtcNow;
        return (effectiveTime - BorrowTime) > duration;
    }
}
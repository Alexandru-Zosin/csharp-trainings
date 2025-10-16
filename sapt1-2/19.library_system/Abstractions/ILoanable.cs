using System;

namespace LibraryCatalog.Abstractions;

public interface ILoanable
{
    bool IsBorrowed { get; }
    TimeSpan? DurationOfBorrow { get; }

    void MarkBorrowed();
    void MarkReturned();
}
using System;

namespace LibraryCatalog;
using LibraryCatalog.Abstractions;

public sealed class Magazine : LibraryItem, ILoanable
{
    public string ISSN { get; }
    public ILoanPolicy Policy { get; set; }
    public bool IsBorrowed { get; private set; } = false;
    public TimeSpan? DurationOfBorrow { get; private set; }

    public Magazine(string title, string issn, ILoanPolicy policy) : base(title)
    {
        if (string.IsNullOrEmpty(issn))
            throw new ArgumentException("Needs a valid ISSN");
        Policy = policy;
        DurationOfBorrow = Policy.GetLoanPeriod();
        ISSN = issn;
    }

    public void MarkBorrowed()
    {
        if (IsBorrowed)
            throw new InvalidOperationException("Book already borrowed.");
        IsBorrowed = true;
    }

    public void MarkReturned()
    {
        if (!IsBorrowed) 
            throw new InvalidOperationException("Not borrowed.");
        IsBorrowed = false;
    }

    public override string GetDescription()
    {
        return $"Magazine: {Title} with ISSN: {ISSN}";
    }
}
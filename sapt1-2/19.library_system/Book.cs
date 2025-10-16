namespace LibraryCatalog;
using System;
using LibraryCatalog.Abstractions;

public sealed class Book : LibraryItem, ILoanable
{
    public string ISBN { get; }
    public ILoanPolicy Policy { get; set; } 

    public bool IsBorrowed { get; private set; } = false;
    public TimeSpan? DurationOfBorrow { get; private set; }

    public Book(string title, string isbn, ILoanPolicy policy) : base(title)
    {
        if (string.IsNullOrEmpty(isbn))
            throw new ArgumentException("Needs a valid ISBN");
        ISBN = isbn;
        Policy = policy;
        DurationOfBorrow = Policy.GetLoanPeriod();
    }

    public void MarkBorrowed()
    {
        if (IsBorrowed)
            throw new InvalidOperationException("Book already borrowed.");
        IsBorrowed = true;
    }

    public void MarkReturned()
    {
        if (!IsBorrowed) throw new InvalidOperationException("Not borrowed.");
        IsBorrowed = false;
    }

    public override string GetDescription()
    {
        return $"Book: {Title} with ISBN: {ISBN}";
    }
}
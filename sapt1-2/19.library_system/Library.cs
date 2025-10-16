namespace LibraryCatalog;
using LibraryCatalog.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Library
{
	public Librarian Librarian { get; set; }
	public IList<Member> Members { get; }
	public IList<LibraryItem> Items { get; }
	public IList<Loan> Loans { get; } = new List<Loan>();

	public Library(IList<Member> members, IList<LibraryItem> items, Librarian librarian)
	{
		Librarian = librarian ?? throw new ArgumentNullException("Libr Field can not be null");
		Members = members ?? throw new ArgumentNullException("Members Field can not be null");
		Items = items ?? throw new ArgumentNullException("items Field can not be null");
	}

	public ILoanable? FindFreeBookByTitle(string title)
	{
		if (string.IsNullOrEmpty(title))
			throw new ArgumentException("Title is required.");

		var filtered = Items
			.Where(i => string.Equals(i.Title, title, StringComparison.OrdinalIgnoreCase))
			.OfType<ILoanable>()
			.FirstOrDefault(i => !i.IsBorrowed);

		return filtered;
	}

	public bool BorrowBookByTitle(Member member, string title)
	{
		if (member == null)
			throw new ArgumentNullException("Member null not alloweed!");
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Title is required.");

		var libraryItem = FindFreeBookByTitle(title);

		if (libraryItem == null)
			return false;

		Loans.Add(new Loan(libraryItem, member));
		libraryItem.MarkBorrowed();
		return true;
	}

	public bool EndLoan(Loan loan)
	{
		if (loan == null)
			throw new ArgumentNullException("Invalid loan");
		if (loan.ReturnTime is not null)
			return false;

		loan.MarkReturned();
		loan.LoanedItem.MarkReturned();
		return true;
	}

	public string GetInventory()
	{
		var sb = new StringBuilder();
		foreach (var item in Items)
			sb.AppendLine(item.GetDescription());
		return sb.ToString();
	}

	public string GetHistoryForItem(ILoanable loanable)
	{
		if (loanable == null)
			throw new ArgumentNullException("Invalid item");

		var sb = new StringBuilder();
		var loansForItem = Loans
			.Where(i => ReferenceEquals(i.LoanedItem, loanable));

		foreach (var loan in loansForItem)
			sb.AppendLine(loan.Loaner.GetDescription());

		return sb.ToString();
	}

	public IList<Member> GetPastDueMembers()
	{
		return Loans
			.Where(i => i.IsPastDue())
			.Select(i => i.Loaner)
			.Distinct()
			.ToList();
	}
}
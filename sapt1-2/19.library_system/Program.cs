using System;
using System.Collections.Generic;
using System.Linq;
using LibraryCatalog;
using LibraryCatalog.Abstractions;

// gpt tests
static IEnumerable<Book> BookCopies(string title, string isbnPrefix, ILoanPolicy policy, int count)
{
    for (var i = 1; i <= count; i++)
        yield return new Book(title, $"{isbnPrefix}-{i:D4}", policy);
}

static IEnumerable<Magazine> MagazineCopies(string title, string issnPrefix, ILoanPolicy policy, int count)
{
    for (var i = 1; i <= count; i++)
        yield return new Magazine(title, $"{issnPrefix}{i:D4}", policy);
}

static IEnumerable<DVD> DVDCopies(string title, string upcPrefix, int count)
{
    for (var i = 1; i <= count; i++)
        yield return new DVD(title, $"{upcPrefix}{i:D4}");
}


// librarian and members
var librarian = new Librarian("Dana", "E001");
var members = new List<Member>
{
    new Member("Alice", "M001"),
    new Member("Bob",   "M002"),
    new Member("Carol", "M003"),
};

// policies you already defined
var bookShort = new BookShortPolicy();
var bookLong = new BookLongPolicy();
var magMid = new MagazineMidPolicy();

// items with duplicate titles
var items = new List<LibraryItem>();
items.AddRange(BookCopies("Robinson Crusoe", "978-014143982", bookShort, 4)); // 4 copies, same title
items.AddRange(BookCopies("Dune", "978-044117271", bookLong, 2));             // 2 copies, same title
items.AddRange(MagazineCopies("Scientific American", "SA-", magMid, 3));      // 3 issues, same title
items.AddRange(DVDCopies("Inception", "DVD-UPC-", 2));

var library = new Library(members, items, librarian);

// borrow two different physical "Robinson Crusoe" copies
_ = library.BorrowBookByTitle(members[0], "Robinson Crusoe"); // Alice
_ = library.BorrowBookByTitle(members[1], "Robinson Crusoe"); // Bob

// return Alice's loan to exercise EndLoan
var aliceLoan = library.Loans.First(l => ReferenceEquals(l.Loaner, members[0]));
_ = library.EndLoan(aliceLoan);

// borrow a magazine
_ = library.BorrowBookByTitle(members[2], "Scientific American"); // Carol

// pick one physical copy to print history for
var rcAnyCopy = library.FindFreeBookByTitle("Robinson Crusoe") ?? library.Loans.First(l => ((LibraryItem)l.LoanedItem).Title == "Robinson Crusoe").LoanedItem;



// simulate a past due
var instantPolicy = new TestInstantPolicy();
var instantBook = new Book("Test Past Due", "978-0000000000", instantPolicy);
library.Items.Add(instantBook);
library.BorrowBookByTitle(members[1], "Test Past Due");

// Wait to exceed loan duration
Thread.Sleep(2000); // exce
//


// outputs
Console.WriteLine("=== Inventory ===");
Console.Write(library.GetInventory());

Console.WriteLine("=== History for a 'Robinson Crusoe' copy ===");
Console.Write(library.GetHistoryForItem((ILoanable)rcAnyCopy));

Console.WriteLine("=== Past-due members ===");
foreach (var m in library.GetPastDueMembers())
    Console.WriteLine(m.GetDescription());

Console.WriteLine("=== Open loans ===");
foreach (var loan in library.Loans.Where(l => l.ReturnTime is null))
    Console.WriteLine($"{loan.Loaner.Name} -> {((LibraryItem)loan.LoanedItem).Title} at {loan.BorrowTime:u}");

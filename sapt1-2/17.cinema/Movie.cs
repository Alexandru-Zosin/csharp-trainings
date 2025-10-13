namespace Cinema;
using System.Collections;

public class Movie
{
    public string Title { get; set; }
    public IList<Ticket> soldTickets { get; } = new List<Ticket>();
    public ITicketPricer Pricer { get; set; }
    public decimal BasePrice { get; set; }

    public Movie(string title, ITicketPricer pricer, decimal basePrice)
    {
        if (title == null)
            throw new ArgumentNullException("illegal null name");
        Title = title;
        Pricer = pricer;
        BasePrice = basePrice;
    }

    public void SellTicket(Ticket ticket)
    {
        if (ticket == null)
            throw new ArgumentNullException("Illegal ticket type");
        soldTickets.Add(ticket);
    }

    public decimal GetRevenue()
    {
        decimal total = 0;
        foreach (var ticket in soldTickets)
            total += Pricer.GetPrice(ticket);
        return total;
    }
}
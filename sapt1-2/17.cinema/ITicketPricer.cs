namespace Cinema;

public interface ITicketPricer
{
    decimal GetPrice(Ticket ticket); // public is auto filled (implicitly understood)
}
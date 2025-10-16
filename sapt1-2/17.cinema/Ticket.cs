namespace CinemaNamespace;
using CinemaNamespace.Enums;

public class Ticket
{
    public Seat SeatPlace { get; }
    public Movie Movie { get; }
    public TicketType TicketType { get; }
    public TicketZone TicketZone { get; }
    
    internal Ticket(Seat seatPlace, Movie movie, TicketType ticketType, TicketZone ticketZone)
    {
        SeatPlace = seatPlace ?? throw new ArgumentNullException("seat can t be null");
        Movie = movie ?? throw new ArgumentNullException("movie can not be null");
        TicketType = ticketType;
        TicketZone = ticketZone;
    }

    public decimal GetBasePrice() => Movie.BasePrice;
}
namespace Cinema;
using Cinema.Enums;

public class Ticket
{
    public Seat SeatPlace { get; set; }
    public Movie Movie { get; set; }
    public TicketType TicketType { get; set; }
    
    public Ticket(Seat seatPlace, Movie movie, TicketType ticketType)
    {
        SeatPlace = seatPlace;
        Movie = movie;
        TicketType = ticketType;
    }

    public decimal GetBasePrice()
    {
        return Movie.BasePrice;
    }
}
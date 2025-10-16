using System;
using System.Collections.Generic;
using CinemaNamespace.Enums;
namespace CinemaNamespace;

public class Cinema
{
    public IList<Ticket> SoldTickets { get; } = new List<Ticket>();
    public IPricePolicy PricePolicy { get; set; }
    public ISeatZonePolicy SeatZonePolicy { get; }

    public Cinema(IPricePolicy pricePolicy, ISeatZonePolicy seatPolicy)
    {
        PricePolicy = pricePolicy ?? throw new ArgumentNullException("Policy cant be null");
        SeatZonePolicy = seatPolicy ?? throw new ArgumentNullException("seatzone policy cant be null");
    }

    public void SellTicket(Movie movie, Seat seat, TicketType type)
    {
        if (movie == null)
            throw new ArgumentNullException("Illegal movie");

        var zone = SeatZonePolicy.GetZone(seat);
        var ticket = new Ticket(seat, movie, type, zone);

        _ = PricePolicy.GetPrice(ticket); // validation if the ticket
        // is legal, otherwise will throw - not the best solution

        SoldTickets.Add(ticket);
    }

    public decimal GetRevenue()
    {
        decimal total = 0;
        foreach (var ticket in SoldTickets)
            total += PricePolicy.GetPrice(ticket);

        return total;
    }
}
namespace CinemaNamespace.Policies;
using CinemaNamespace;
using CinemaNamespace.Enums;
using System;

public sealed class DefaultSeatZonePolicy : ISeatZonePolicy
{ // multiple classes like Bucharest,Constanta,Iasi should be implemented l
    //like in the case of prices
    public TicketZone GetZone(Seat seat)
    {
        if (seat is null)
            throw new ArgumentNullException("Seat can t be null");

        return seat.Row switch
        {
            <= 3 => TicketZone.VIP,
            <= 6 => TicketZone.Upper,
            <= 10 => TicketZone.Mid,
            _ => TicketZone.Low
        };
    }
}
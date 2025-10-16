namespace CinemaNamespace.Policies;
using CinemaNamespace;
using CinemaNamespace.Enums;
using System.Collections;
using System.Collections.Generic;

public sealed class ConstantaPricePolicy : IPricePolicy
{
    public IDictionary<TicketZone, decimal> ZoneMultipliers { get; set; } =
        new Dictionary<TicketZone, decimal>
    {
        { TicketZone.Low, 0.9m },
        { TicketZone.Mid, 1.0m }
    };

    public IDictionary<TicketType, decimal> TypeMultipliers { get; set; } =
        new Dictionary<TicketType, decimal>
    {
        { TicketType.TwoD, 0.5m },
        { TicketType.ThreeD, 1.0m }
    };

    public decimal GetPrice(Ticket ticket)
    {
        if (ticket == null)
            throw new ArgumentNullException("Ticket cant be null");

        if (!ZoneMultipliers.TryGetValue(ticket.TicketZone, out var zoneMult))
            throw new KeyNotFoundException("Zone not supported");

        if (!TypeMultipliers.TryGetValue(ticket.TicketType, out var typeMult))
            throw new KeyNotFoundException("Type not supp");

        return ticket.GetBasePrice() * zoneMult * typeMult;
    }
}


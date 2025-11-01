namespace CinemaNamespace.Policies;
using CinemaNamespace;
using CinemaNamespace.Enums;
using System.Collections;
using System.Collections.Generic;

public sealed class BucharestPricePolicy : IPricePolicy
{
    public IDictionary<TicketZone, decimal> ZoneMultipliers { get; private set; } =
        new Dictionary<TicketZone, decimal>
    {
        { TicketZone.Low, 1.1m },
        { TicketZone.Mid, 1.5m },
        { TicketZone.Upper, 1.8m },
        { TicketZone.VIP, 2.5m }
    };

    public IDictionary<TicketType, decimal> TypeMultipliers { get; private set; } =
        new Dictionary<TicketType, decimal>
    {
        { TicketType.TwoD, 0.7m },
        { TicketType.ThreeD, 1.3m }
    };

    public decimal GetPrice(Ticket ticket)
    {
        if (ticket == null)
            throw new ArgumentNullException("Ticket cant be null");

        if (!ZoneMultipliers.TryGetValue(ticket.TicketZone, out var zoneMult))
            throw new InvalidOperationException("Zone not supported");

        if (!TypeMultipliers.TryGetValue(ticket.TicketType, out var typeMult))
            throw new InvalidOperationException("Type not supp");

        return ticket.GetBasePrice() * zoneMult * typeMult;
    }
}


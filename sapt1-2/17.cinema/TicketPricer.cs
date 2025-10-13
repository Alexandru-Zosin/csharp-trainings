using System.Collections;
namespace Cinema;
using Cinema.Enums;

public class TicketPricer : ITicketPricer
{
    private readonly IDictionary<TicketZone, decimal> _zoneMultiplier;
    private readonly IDictionary<TicketType, decimal> _typeMultiplier;

    public TicketPricer(IDictionary<TicketZone, decimal> zoneMultiplier,
        IDictionary<TicketType, decimal> typeMultiplier)
    {
        _zoneMultiplier = zoneMultiplier;
        _typeMultiplier = typeMultiplier;
    }

    public decimal GetPrice(Ticket ticket)
    {
        var zone = _zoneMultiplier[ticket.SeatPlace.GetZone()];
        var type = _typeMultiplier[ticket.TicketType];
        return (zone + type) * ticket.GetBasePrice();
    }
}
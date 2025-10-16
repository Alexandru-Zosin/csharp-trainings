namespace CinemaNamespace; 
using CinemaNamespace.Enums;

public interface ISeatZonePolicy
{
    TicketZone GetZone(Seat seat);
}
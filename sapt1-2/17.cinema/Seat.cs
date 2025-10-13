namespace Cinema;
using Cinema.Enums;

public readonly record struct Seat(int Col, int Row)
{   
    public TicketZone GetZone()
    {
        switch(Row)
        {
            case <= 3:
                return TicketZone.Low;
            case <= 7:
                return TicketZone.Mid;
            default:
                return TicketZone.Upper;
        }
    }
}
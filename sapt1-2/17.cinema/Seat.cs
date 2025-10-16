namespace CinemaNamespace;
using CinemaNamespace.Enums;

public sealed record Seat
{
    public int Row { get; }
    public int Number { get; }
    public Seat(int row, int number)
    {
        if (row <= 0) throw new ArgumentOutOfRangeException(nameof(row));
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
        Row = row; 
        Number = number;
    }
}
// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using CinemaNamespace;
using CinemaNamespace.Enums;
using CinemaNamespace.Policies;

// made with gpt, however the fails are exceptions thrown because tickets
// are created randomly and they didn t respect the policies
// ----- Setup seed data -----
var movies = new List<Movie>
{
    new Movie("Dune", 30.00m),
    new Movie("Inside Out 2", 25.00m),
    new Movie("Oppenheimer", 35.00m),
    new Movie("The Batman", 28.00m),
};

// Simple seat map: 12 rows x 16 seats
IEnumerable<Seat> AllSeats()
{
    for (int r = 1; r <= 12; r++)
        for (int n = 1; n <= 16; n++)
            yield return new Seat(r, n);
}
var seats = AllSeats().ToArray();

// ----- Cinemas with policies -----
var cinemas = new Dictionary<string, Cinema>
{
    ["Bucharest MegaMall"] = new Cinema(new Bucharest(), new DefaultSeatZonePolicy()),
    ["Iasi Palas"] = new Cinema(new IasiPricePolicy(), new DefaultSeatZonePolicy()),
    ["Constanta CityPark"] = new Cinema(new ConstantaPricePolicy(), new DefaultSeatZonePolicy()),
};

// ----- Bulk sell tickets -----
var rng = new Random(42);

// Configure how many random sales per cinema
const int salesPerCinema = 750;

// Weighted ticket type selection
TicketType NextType(Random r)
{
    // ~60% TwoD, ~40% ThreeD
    return r.NextDouble() < 0.6 ? TicketType.TwoD : TicketType.ThreeD;
}

void TrySell(Cinema c, Movie m, Seat s, TicketType t, ref int ok, ref int fail)
{
    try
    {
        c.SellTicket(m, s, t);
        ok++;
    }
    catch
    {
        // Intentionally swallow here for load-style runs
        fail++;
    }
}

foreach (var kvp in cinemas)
{
    var cinemaName = kvp.Key;
    var cinema = kvp.Value;

    int success = 0, failed = 0;
    for (int i = 0; i < salesPerCinema; i++)
    {
        var movie = movies[rng.Next(movies.Count)];
        var seat = seats[rng.Next(seats.Length)];
        var type = NextType(rng);

        TrySell(cinema, movie, seat, type, ref success, ref failed);
    }

    Console.WriteLine($"{cinemaName}: sold={success}, failed={failed}, revenue={cinema.GetRevenue():F2}");
}

// ----- Spot checks and simple analytics -----
Console.WriteLine();
foreach (var (name, cinema) in cinemas)
{
    // Top 3 movies by revenue per cinema
    var topByMovie = cinema.SoldTickets
        .GroupBy(t => t.Movie.Title)
        .Select(g => new
        {
            Title = g.Key,
            Revenue = g.Sum(t => cinema.PricePolicy.GetPrice(t)),
            Count = g.Count()
        })
        .OrderByDescending(x => x.Revenue)
        .Take(3)
        .ToArray();

    Console.WriteLine($"{name} top movies:");
    foreach (var x in topByMovie)
        Console.WriteLine($"  {x.Title,-20} rev={x.Revenue:F2} cnt={x.Count}");
}

// Aggregate by zone across all cinemas
Console.WriteLine();
var zoneTotals = cinemas
    .SelectMany(c => c.Value.SoldTickets.Select(t => (cinema: c.Value, t)))
    .GroupBy(x => x.t.TicketZone)
    .Select(g => new
    {
        Zone = g.Key,
        Revenue = g.Sum(x => x.cinema.PricePolicy.GetPrice(x.t)),
        Count = g.Count()
    })
    .OrderByDescending(x => x.Revenue);

Console.WriteLine("Revenue by zone (all cinemas):");
foreach (var z in zoneTotals)
    Console.WriteLine($"  {z.Zone,-5} rev={z.Revenue:F2} cnt={z.Count}");

// Done
Console.WriteLine();
Console.WriteLine("Run complete.");

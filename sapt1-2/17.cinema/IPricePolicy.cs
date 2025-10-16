namespace CinemaNamespace;

public interface IPricePolicy
{
    decimal GetPrice(Ticket ticket); // public is auto filled (implicitly understood)
}
namespace CinemaNamespace;
using System.Collections;

public class Movie
{
    public string Title { get; set; }
    public decimal BasePrice { get; set; }

    public Movie(string title, decimal basePrice)
    {
        if (title == null)
            throw new ArgumentNullException("illegal null name");
        Title = title;
        BasePrice = basePrice;
    }
}

namespace top_movie_picks;
public class User
{
    public int num_reviews { get; set; }
    public string username { get; set; }
    public int globalMinimum = 10;
    public int globalMaximum = 0;
    
    public Genre drama;

    public Genre comedy;

    public Genre action;

    public Genre romance;

    public Genre fiction;

    public Genre animation;

    public Genre thriller;

    public Genre documentary;

    public List<Genre> Genres => new()
    {
        drama,
        comedy,
        action,
        romance,
        fiction,
        animation,
        thriller,
        documentary
    };


    public User()
    {
        drama = new Genre();
        comedy = new Genre();
        action = new Genre();
        romance = new Genre();
        fiction = new Genre();
        animation = new Genre();
        thriller = new Genre();
        documentary  = new Genre();
        
    }

    public void CountCoordinates()
    {
        foreach (var genre in Genres)
        {
            if (genre.ratings.Count == 0)
            {
                genre.average = 0;
                continue;
            }

            var localMinimum = genre.ratings.Select(rate => rate.rating_val).Min();
            if (localMinimum < globalMinimum)
                globalMinimum = localMinimum;
            var localMaximum = genre.ratings.Select(rate => rate.rating_val).Max();
            if (localMaximum > globalMaximum)
                globalMinimum = localMaximum;
            genre.average = genre.ratings.Average(rating => rating.rating_val);
        }

        foreach (var genre in Genres)
        {
            genre.average = (genre.average - globalMinimum) /
                            (globalMaximum - globalMinimum);
        }
    }

    public override string ToString()
    {
        return $"{username} -- {num_reviews}: {drama} - {comedy} - {action} - {romance} - {fiction} - {animation} - {thriller} - {documentary}";
    }
}
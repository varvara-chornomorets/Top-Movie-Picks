
namespace top_movie_picks;
public class User
{
    public int num_reviews { get; set; }
    public string username { get; set; }
    
    public Genre drama;

    public Genre comedy;

    public Genre action;

    public Genre romance;

    public Genre fiction;

    public Genre animation;

    public Genre thriller;

    public Genre documentary;

    public List<Genre> Genres => new List<Genre>
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
        foreach (Genre genre in Genres)
        {
            if (genre.ratings.Count == 0)
            {
                genre.average = 0;
                continue;
            }
            genre.average = genre.ratings.Average(rating => rating.rating_val);
        }
    }

    

}
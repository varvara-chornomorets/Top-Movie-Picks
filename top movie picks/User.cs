
namespace top_movie_picks;
public class User
{
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
       
    }

    public void CountCoordinates()
    {
        foreach (Genre genre in Genres)
        {
            genre.average = genre.ratings.Average(rating => rating.rating_val);
        }
    }

    

}
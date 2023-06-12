namespace top_movie_picks;

public class Genre
{
    public List<Rating> ratings;
    public double average;

    public Genre()
    {
        ratings = new List<Rating>();
        average = 0;
    }
}
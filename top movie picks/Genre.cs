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

    public override string ToString()
    {
        return $"{Math.Round(average, 2)}";
    }
}
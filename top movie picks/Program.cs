using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Globalization;


var films = new List<Film>();
const string moviePath1 = "D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\movie_data.csv"; // We may need different paths
using var reader = new StreamReader(moviePath1);
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
csv.Read();
csv.ReadHeader();

while (csv.Read())
{
    try
    {
        var film = csv.GetRecord<Film>();
        if (film.Genres != null)
        {
            if (film.Genres.Contains("Drama") || film.Genres.Contains("Comedy") ||
                film.Genres.Contains("Documentary") || film.Genres.Contains("Thriller") ||
                film.Genres.Contains("Romance") || film.Genres.Contains("Action") ||
                film.Genres.Contains("Animation") || film.Genres.Contains("Adventure") ||
                film.Genres.Contains("Science Fiction") || film.Genres.Contains("Fantasy"))
                films.Add(film);
        }
    }
    catch (CsvHelper.MissingFieldException) {}
}
Console.WriteLine("Done. ");

public class Film
{
    [Name("_id")]
    public string ID { get; set; }

    public string[]? Genres { get; private set; }

    [Name("genres")]
    public string GenresString
    {
        set => Genres = makeArray(value);
    }
    
    [Name("imdb_link")]
    public string ImdbLink { get; set; }
    
    [Name("movie_id")]
    public string MovieId { get; set; }
    
    [Name("movie_title")]
    public string MovieTitle { get; set; }
    
    [Name("overview")]
    public string Overview { get; set; }
    
    [Name("release_date")]
    public string ReleaseDate { get; set; }
    
    [Name("tmdb_link")]
    public string TmdbLink { get; set; }

    private string[]? makeArray(string input)
    {
        var replace = input.Replace("\"", "").Replace("[", "")
            .Replace("]", "");
        return replace == "" ? null : replace.Split(',');
    }
}

/*
    _id
    genres
----image_url
----imdb_id
    imdb_link
    movie_id
    movie_title
----original_language
    overview
----popularity
-11-production_countries
    release_date
----runtime
----spoken_languages
----tmdb_id
    tmdb_link
----vote_average
----vote_count
----year_released

 */
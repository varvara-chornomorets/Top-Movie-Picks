using CsvHelper.Configuration.Attributes;

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
----_id
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
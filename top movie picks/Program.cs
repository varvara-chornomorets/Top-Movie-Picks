using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.IO;

var films = new List<Film>();

using (var reader = new StreamReader("D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\movie_data.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Read();
    csv.ReadHeader();

    while (csv.Read())
    {
        var film = csv.GetRecord<Film>();
        films.Add(film);
    }
}

public class Film
{
    [Name("_id")]
    public string ID { get; set; }
    
    [Name("genres")]
    public string[] Genres { get; set; }
    
    [Name("imdb_link")]
    public string ImdbLink { get; set; }
    
    [Name("movie_id")]
    public string MovieId { get; set; }
    
    [Name("movie_title")]
    public string MovieTitle { get; set; }
    
    [Name("overview")]
    public string Overview { get; set; }
    
    [Name("production_countries")]
    public string[] ProductionCountries { get; set; }
    
    [Name("release_date")]
    public string ReleaseDate { get; set; }
    
    [Name("tmdb_link")]
    public string TmdbLink { get; set; }
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
----release_date
----runtime
----spoken_languages
----tmdb_id
    tmdb_link
----vote_average
----vote_count
    year_released

 */
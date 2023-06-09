﻿using System.Globalization;
using CsvHelper.Configuration.Attributes;

public class Film
{
    public string[]? Genres { get; private set; }

    [Name("genres")]
    public string GenresString
    {
        get => Genres != null ? string.Join(",", Genres) : string.Empty;
        set => Genres = makeArray(value);
    }
    
    [Name("imdb_link")]
    public string ImdbLink { get; set; }
    
    [Name("movie_id")]
    public string MovieId { get; set; }
    
    [Name("popularity")] public string PopularityString { get; set; }
    [Ignore]
    public double Popularity => PopularityString == "null" ? 0 : double.Parse(PopularityString, CultureInfo.InvariantCulture);


    [Name("vote_average")] public string VoteAverageString { get; set; }
    [Ignore]
    public double VoteAverage => VoteAverageString == "null" ? 0 : double.Parse(VoteAverageString, CultureInfo.InvariantCulture);

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

    public string Description()
    {
        return $"\"{MovieTitle}\"\n" +
               $"Overview: {Overview}\n" +
               $"Average vote: {VoteAverage}\n" +
               $"Released: {ReleaseDate}\n" +
               $"Genre(s): {string.Join(", ", Genres)}\n" +
               $"IMDB link: {ImdbLink}\n" +
               $"TMDB link: {TmdbLink}\n";
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
    popularity
-11-production_countries
    release_date
----runtime
----spoken_languages
----tmdb_id
    tmdb_link
    vote_average
----vote_count
----year_released

 */
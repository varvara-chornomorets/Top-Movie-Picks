﻿
namespace top_movie_picks;

public class User
{
    public int num_reviews { get; set; }
    public string username { get; set; }
    public int globalMinimum = 10;
    public int globalMaximum = 1;

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
        documentary = new Genre();

    }

    public void CountCoordinates()
    {
        foreach (var genre in Genres)
        {
            if (genre.ratings.Count == 0)
            {
                genre.average = 5.5;
                continue;
            }

            var localMinimum = genre.ratings.Select(rate => rate.rating_val).Min();
            if (localMinimum < globalMinimum)
                globalMinimum = localMinimum;
            var localMaximum = genre.ratings.Select(rate => rate.rating_val).Max();
            if (localMaximum > globalMaximum)
                globalMaximum = localMaximum;
            genre.average = genre.ratings.Average(rating => rating.rating_val);
        }

        if (globalMinimum == globalMaximum)
        {
            globalMinimum = 1;
            globalMaximum = 10;
        }

        foreach (var genre in Genres)
        {
            genre.average = (genre.average - globalMinimum) /
                            (globalMaximum - globalMinimum);
        }
    }

    public double FindDifference(User anotherUser) => Enum.GetValues(typeof(genre)).Cast<genre>()
        .Sum(genre => Math.Pow(GetGenre(genre).average - anotherUser.GetGenre(genre).average, 2));

    public string[] AllMovieIds() =>
        (from genre in Genres from rating in genre.ratings select rating.movie_id).ToArray();

    public override string ToString()
    {
        return
            $"{username} -- {num_reviews}: {drama} - {comedy} - {action} - {romance} - {fiction} - {animation} - {thriller} - {documentary}";
    }

    public void AddRating(Rating rating, string[]? genres)
    {
        if (genres.Length == 0)
        {
            return;
        }
        foreach (var genre in genres)
        {
            switch (genre)
            {
                case "Drama":
                    drama.ratings.Add(rating);
                    break;
                case "Comedy":
                    comedy.ratings.Add(rating);
                    break;
                case "Action" or "Adventure":
                    action.ratings.Add(rating);
                    break;
                case "Romance":
                    romance.ratings.Add(rating);
                    break;
                case "Science Fiction" or "Fantasy":
                    fiction.ratings.Add(rating);
                    break;
                case "Animation":
                    animation.ratings.Add(rating);
                    break;
                case "Thriller":
                    thriller.ratings.Add(rating);
                    break;
                case "Documentary":
                    documentary.ratings.Add(rating);
                    break;
            }
        }
        
    }

    public Genre GetGenre(genre genre)
    {
        return genre switch
        {
            genre.drama => drama,
            genre.comedy => comedy,
            genre.action => action,
            genre.romance => romance,
            genre.fiction => fiction,
            genre.animation => animation,
            genre.thriller => thriller,
            genre.documentary => documentary,
            _ => throw new ArgumentException("Invalid genre"),
        };
    }
}
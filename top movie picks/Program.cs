using CsvHelper;
using System.Globalization;
using top_movie_picks;

var movieById = new Dictionary<string, Film>();

var movies = ReadFilms();

var users = ReadUserCsv();

var ratings = ReadRatingsCsv();

foreach (var movie in movies)
    movieById[movie.MovieId] = movie;


// var userPoints = CreateSpace(users, ratings);
// foreach (var point in userPoints)
//     Console.WriteLine($"{point.animation.average}, {point.action.average}, {point.action.average} ");

List<Film> ReadFilms()
{
    var films = new List<Film>();
    const string moviePath1 = "movie_data.csv";
    const string moviePath2 = "D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\movie_data.csv";
    var path = File.Exists(moviePath1) ? moviePath1 : moviePath2;
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    csv.Read();
    csv.ReadHeader();

    while (csv.Read())
    {
        try
        {
            var film = csv.GetRecord<Film>();
            if (film.Genres == null) continue;
            if (film.Genres.Contains("Drama") || film.Genres.Contains("Comedy") ||
                film.Genres.Contains("Documentary") || film.Genres.Contains("Thriller") ||
                film.Genres.Contains("Romance") || film.Genres.Contains("Action") ||
                film.Genres.Contains("Animation") || film.Genres.Contains("Adventure") ||
                film.Genres.Contains("Science Fiction") || film.Genres.Contains("Fantasy"))
                films.Add(film);
        }
        catch (CsvHelper.MissingFieldException) {}
    }

    return films;
}

List<User> ReadUserCsv()
{
    const string moviePath1 = "users_export.csv";
    const string moviePath2 = "D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\users_export.csv";
    var path = File.Exists(moviePath1) ? moviePath1 : moviePath2;
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
    var records = new List<User>();
    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
        var user = new User()
        {
            username = csv.GetField("username"),
            num_reviews = csv.GetField<int>("num_reviews"),
        };
        records.Add(user);
    }
    var sortedRecords = records.OrderBy(U => U.username);
    return sortedRecords.ToList();
}

List<Rating> ReadRatingsCsv()
{
    const string moviePath1 = "ratings_export.csv";
    const string moviePath2 = "D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\ratings_export.csv";
    var path = File.Exists(moviePath1) ? moviePath1 : moviePath2;
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
    {
        var records = new List<Rating>();
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            var record = new Rating()
            {
                movie_id = csv.GetField("movie_id"),
                rating_val = csv.GetField<int>("rating_val"),
                user_id = csv.GetField("user_id")
            };
            records.Add(record);
        }
        
        var sortedRecords = records.OrderBy(R => R.user_id);
        return sortedRecords.ToList();
    }
}

List<User>? CreateSpace(List<User> users, List<Rating> ratings)
{
    var result = new List<User>();
    var counter = 0;
    foreach (var userRaw in users)
    {
        var user = new User();
        while (counter < ratings.Count && ratings[counter].user_id == userRaw.username)
        {
            var curRating = ratings[counter];
            var curMovie = curRating.movie_id;
            counter++;
            if (!movieById.ContainsKey(curMovie)) continue;
            foreach (var genre in movieById[curMovie].Genres)
            {
                switch (genre)
                {
                    case "Drama":
                        user.drama.ratings.Add(curRating);
                        break;
                    case "Comedy":
                        user.comedy.ratings.Add(curRating);
                        break;
                    case "Action":
                        user.action.ratings.Add(curRating);
                        break;
                    case "Romance":
                        user.romance.ratings.Add(curRating);
                        break;
                    case "Fiction":
                        user.fiction.ratings.Add(curRating);
                        break;
                    case "Animation":
                        user.animation.ratings.Add(curRating);
                        break;
                    case "Thriller":
                        user.thriller.ratings.Add(curRating);
                        break;
                    case "Documentary":
                        user.documentary.ratings.Add(curRating);
                        break;
                }
                
            }
            

        }

        user.CountCoordinates();

        result.Add(user);

    }

    return result;
}





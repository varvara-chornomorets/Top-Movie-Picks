using System.ComponentModel;
using CsvHelper;
using System.Globalization;
using top_movie_picks;

var movieById = new Dictionary<string, Film>();
var userByUsername = new Dictionary<string, User>();

var movies = ReadFilms();

foreach (var movie in movies)
    movieById[movie.MovieId] = movie;

var users = ReadUserCsv();
foreach (var user in users)
{
    userByUsername[user.username] = user;
}
AddReviewsToUsers(users);
CreateSpace(users);
Console.WriteLine(users);


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

void AddReviewsToUsers(List<User> users)
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
            if (!userByUsername.ContainsKey(record.user_id)) continue;
            var curUser = userByUsername[record.user_id];
            var curMovie = record.movie_id;
            if (!movieById.ContainsKey(curMovie)) continue;
            foreach (var genre in movieById[curMovie].Genres)
            {
                switch (genre)
                {
                    case "Drama":
                        curUser.drama.ratings.Add(record);
                        break;
                    case "Comedy":
                        curUser.comedy.ratings.Add(record);
                        break;
                    case "Action":
                        curUser.action.ratings.Add(record);
                        break;
                    case "Romance":
                        curUser.romance.ratings.Add(record);
                        break;
                    case "Fiction":
                        curUser.fiction.ratings.Add(record);
                        break;
                    case "Animation":
                        curUser.animation.ratings.Add(record);
                        break;
                    case "Thriller":
                        curUser.thriller.ratings.Add(record);
                        break;
                    case "Documentary":
                        curUser.documentary.ratings.Add(record);
                        break;
                }
            }
        }
    }
}

void CreateSpace(List<User> users)
{
    foreach (var variableUser in users)
    {
        variableUser.CountCoordinates();
    }
}





using System.Collections;
using CsvHelper;
using System.Globalization;
using top_movie_picks;

var movieById = new Dictionary<string, Film>();
var userByUsername = new Dictionary<string, User>();

var movies = ReadFilms();
Console.WriteLine("Movies picked! ");

foreach (var movie in movies)
    movieById[movie.MovieId] = movie;

var users = ReadUserCsv();
foreach (var user in users)
{
    userByUsername[user.username] = user;
}
Console.WriteLine("Users pickled! ");
Console.WriteLine("Working on reviews... ");

AddReviewsToUsers(); 
Console.WriteLine("Reviews added! ");

Console.WriteLine($"Empty users: {DeleteUsersWithoutReviews()}");

CreateSpace(users);
Console.WriteLine("Press any button, if you want to see \"Space\": ");
Console.ReadKey();
foreach (var user in users)
    Console.WriteLine(user);



List<Film> ReadFilms()
{
    var films = new List<Film>();
    const string shortMoviePath = "movies.csv";
    if (File.Exists(shortMoviePath))
    {
        using var reader = new StreamReader(shortMoviePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            try
            {
                var film = csv.GetRecord<Film>();
                films.Add(film);
            }
            catch (CsvHelper.MissingFieldException) {}
        }
    }
    else
    {
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

        using var writer = new StreamWriter(shortMoviePath);
        using var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv2.WriteRecords((IEnumerable)films);
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
        var user = csv.GetRecord<User>();
        records.Add(user);
    }
    return records;
}

void AddReviewsToUsers()
{
    const string moviePath1 = "ratings_export.csv";
    const string moviePath2 = "D:\\C#Projects\\Top-Movie-Picks\\top movie picks\\ratings_export.csv";
    var path = File.Exists(moviePath1) ? moviePath1 : moviePath2;
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    {
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            var record = csv.GetRecord<Rating>();
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
                    case "Action" or "Adventure":
                        curUser.action.ratings.Add(record);
                        break;
                    case "Romance":
                        curUser.romance.ratings.Add(record);
                        break;
                    case "Science Fiction" or "Fantasy":
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

int DeleteUsersWithoutReviews()
{
    var counter = 0;
    var usersToDelete = new List<User>();
    foreach (var user in users.Where(user => user.Genres.All(genre => genre.ratings.Count == 0)))
    {
        usersToDelete.Add(user);
        counter++;
    }

    foreach (var user in usersToDelete)
    {
        users.Remove(user);
    }

    return counter;
}

void CreateSpace(List<User> users)
{
    foreach (var variableUser in users)
    {
        variableUser.CountCoordinates();
    }
}





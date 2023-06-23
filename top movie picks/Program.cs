using System.Collections;
using System.ComponentModel;
using CsvHelper;
using System.Globalization;
using top_movie_picks;

var movieById = new Dictionary<string, Film>();

Console.WriteLine("hello, out precious user! \nlet us prepare real quick :} ");
(var space, Dictionary<string, Film> movieByName) = Preparation();
var preciousUser = new User();
var kDTree = new K_dTree(space.ToArray());
Console.WriteLine("Let the magic begin! Firstly, we want to get to know you! So, please, give us your reviews " +
                  "by typing in \n'rate <movie_name> <your rate from 1 to 10>'");
const string whenCommandIsWrong = "We regret to inform you that there is " +
                                  "no such command. \navailable commands are: " +
                                  "rate <movie_name> <your rate from 1 to 10>" +
                                  "\n discover";

while (true)
{
    var command = Console.ReadLine();
    if (command == null)
    {
        continue;
    }
    if (command.Contains("rate"))
    {
        Rate(command);
        continue;
    }

    if (command.Contains("recommend"))
    {
        Recommend();
        continue;
    }
    if (command.Contains("discover"))
    {
        Discover(command);
        continue;
    }
    if (command.Contains("describe"))
    {
        Describe(command);
        continue;
    }

    Console.WriteLine(whenCommandIsWrong);
}

void Recommend()
{
    preciousUser.CountCoordinates();
    var kNeighbours = kDTree.FindNeighbours(preciousUser, 15);
    var recommendMovies = new Dictionary<string, int>();
    var watchedMovies = preciousUser.AllMovieIds();
    foreach (var neighbour in kNeighbours) // through all neighbours...
    {
        foreach (var neighbourGenre in neighbour.Genres) // through all genres they like...
        {
            if (neighbourGenre.average < 0.5) continue;
            foreach (var rating in neighbourGenre.ratings) // and all movies they consider cool...
            {
                if (rating.rating_val < 7) continue;
                var movieId = rating.movie_id;
                if (watchedMovies.Contains(movieId)) continue;
                if (recommendMovies.ContainsKey(movieId))
                    recommendMovies[movieId] += rating.rating_val;
                else
                    recommendMovies[movieId] = rating.rating_val;
            }
        }
    } // we choose those that have the biggest ratings sum, regardless of whether it's a few 10s or a lot of 7s...

    var topThreeFilms = recommendMovies
        .OrderByDescending(kv => kv.Value).Take(3).Select(kv => kv.Key);
    foreach (var filmId in topThreeFilms)
    {
        Console.WriteLine($"Have you watched the movie \"{movieById[filmId].MovieTitle}\"?");
        Console.WriteLine($"If you want further information about the film, type this: \"describe {movieById[filmId].MovieTitle}\"");
        Console.WriteLine();
    }
}

void Rate(string command)
{
    var commandArr = command.Split(" ");
    var ratingVal = commandArr[^1];
    if (!int.TryParse(ratingVal, out var intRatingVal) || intRatingVal is < 1 or > 10)
    {
        Console.WriteLine("Looks like your rate is not valid");
        return;
    }
    var movieNameArr = commandArr[1..^1];
    var movieName = string.Join(" ", movieNameArr);
    if (!movieByName.ContainsKey(movieName))
    {
        var similarMovies = movieByName.Keys.OrderBy(key => LevenshteinDistance(movieName, key)).Take(3).ToArray();

        Console.WriteLine($"Looks like you've made a typo. Maybe instead of {movieName} you meant:");

        foreach (var option in similarMovies)
            Console.WriteLine(option);

        return;
    }

    var curMovie = movieByName[movieName];
    if (preciousUser.AllMovieIds().Contains(curMovie.MovieId))
    {
        Console.WriteLine("Sorry, you've already rated the movie.");
        Console.WriteLine();
        return;
    }
    var rating = new Rating
    {
        rating_val = intRatingVal,
        movie_id = curMovie.MovieId
    };
    preciousUser.AddRating(rating, curMovie.Genres);
    Console.WriteLine("okay, we've got your review, let's continue");
}

List<string>? FindBestMatches(string inputMovieName)
{
    var possibleDistance = inputMovieName.Length / 5;
    List<string> bestMatches = new List<string>();
    foreach (var word in movieByName.Keys)
    {
        var distance = LevenshteinDistance(inputMovieName, word);
        if (distance <= possibleDistance)
        {
            bestMatches.Add(word);
        }
    }
    return bestMatches;
}

int LevenshteinDistance(string original, string candidate)
{
    // preparation stage
    var array = new int[candidate.Length + 1, original.Length + 1];
    for (var i = 0; i <= candidate.Length; i++)
    {
        array[i, 0] = i;
    }

    for (var j = 0; j <= original.Length; j++)
    {
        array[0, j] = j;
    }

    for (var i = 1; i <= candidate.Length; i++)
    {
        for (var j = 1; j <= original.Length; j++)
        {
            array[i, j] = Math.Min(Math.Min(array[i - 1, j] + 1, // deletion
                    array[i, j - 1] + 1), // insertion
                array[i - 1, j - 1] + IsDifferent(candidate[i - 1], original[j - 1])); //substitution
            if ((i > 1) && (j > 1) && (candidate[i - 1] == original[j - 2]) && (candidate[i - 2] == original[j - 1]))
            {
                array[i, j] = Math.Min(array[i, j], array[i - 2, j - 2] + 1); //transposition
            }
        }
    }

    return array[candidate.Length, original.Length];
}

int IsDifferent(char first, char second)
{
    return first == second ? 0 : 1;
}

void Discover(string command)
{
    preciousUser.CountCoordinates();
}

void Describe(string command)
{
    var name = command[9..];
    if (movieByName.TryGetValue(name, out var value))
    {
        Console.WriteLine(value.Description());
        return;
    }

    var similarMovies = movieByName.Keys.OrderBy(key => LevenshteinDistance(name, key)).Take(3).ToArray();

    Console.WriteLine("Sorry, we don't know what you meant, but here we have some possible options: ");

    foreach (var movieName in similarMovies)
        Console.WriteLine(movieName);
}


(List<User>  users, Dictionary<string, Film> movieNames) Preparation()
{
    var userByUsername = new Dictionary<string, User>();
    var movieNames = new Dictionary<string, Film>();

    var movies = ReadFilms();
    Console.WriteLine("Movies picked! ");

    foreach (var movie in movies)
    {
        movieById[movie.MovieId] = movie;
        movieNames[movie.MovieTitle] = movie;
    }
        

    var users = ReadUserCsv();
    foreach (var user in users)
    {
        userByUsername[user.username] = user;
    }
    Console.WriteLine("Users picked! ");
    Console.WriteLine("Working on reviews... ");

    AddReviewsToUsers(userByUsername); 
    Console.WriteLine("Reviews added! ");

    Console.WriteLine($"Empty users: {DeleteUsersWithoutReviews(users)}");

    CreateSpace(users);
    return (users, movieNames);
}

List<Film> ReadFilms()
{
    var films = new List<Film>();
    const string shortMoviePath = "movies.csv";
    if (File.Exists(shortMoviePath))
    {
        using var reader = new StreamReader(shortMoviePath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        films.AddRange(csvReader.GetRecords<Film>());
        return films;
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
                if (film!.Genres == null) continue;
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
        records.Add(user!);
    }
    return records;
}

void AddReviewsToUsers(Dictionary<string, User> userByUsername)
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
            if (!userByUsername.ContainsKey(record!.user_id)) continue;
            var curUser = userByUsername[record.user_id];
            var curMovie = record.movie_id;
            if (!movieById.ContainsKey(curMovie)) continue;
            var genres = movieById[curMovie].Genres;
            curUser.AddRating(record, genres);
        }
    }
}

int DeleteUsersWithoutReviews(List<User> users)
{
    var counter = 0;
    var usersToDelete = new List<User>();
    foreach (var user in users.Where(user => user.Genres.All(genre => genre.ratings.Count == 0)))
    {
        usersToDelete.Add(user);
        counter++;
    }

    foreach (var user in usersToDelete)
        users.Remove(user);

    return counter;
}

void CreateSpace(List<User> users)
{
    foreach (var variableUser in users)
        variableUser.CountCoordinates();
}





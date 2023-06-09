using CsvHelper;
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

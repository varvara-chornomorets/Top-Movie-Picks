using CsvHelper;
using top_movie_picks;



List<UserRaw> users = ReadUserCsv("users_export.csv");

List<Rating> ratings = ReadRatingsCsv("ratings_export.csv");


// CreateSpace(users, ratings);


    
List<UserRaw> ReadUserCsv(string path)
{
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
    var records = csv.GetRecords<UserRaw>();
    var sortedRecords = records.OrderBy(U => U.username);
    return sortedRecords.ToList();
}

List<Rating> ReadRatingsCsv(string path)
{
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
    var records = csv.GetRecords<Rating>();
    // maybe we could omit here some of the ratings, which are about genres we aren't interested 
    var sortedRecords = records.OrderBy(R => R.user_id);
    return sortedRecords.ToList();
}

List<User>? CreateSpace(List<UserRaw> users, List<Rating> ratings)
{
    List<User> result = new List<User>();
    int counter = 0;
    foreach (var userRaw in users)
    {
        User user = new User();
        while (ratings[counter].user_id == userRaw.username || counter !=ratings.Count)
        {
            var curRating = ratings[counter];
            var curMovie = curRating.movie_id;
            foreach (var genre in genreList)
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


                counter++;
            }

        }

        user.CountCoordinates();

        result.Add(user);

    }

    return result;
}





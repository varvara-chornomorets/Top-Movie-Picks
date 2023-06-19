using CsvHelper.Configuration.Attributes;
using Microsoft.Win32.SafeHandles;

namespace top_movie_picks;

public class Rating
{
    public string movie_id{ get; set; }
    [TypeConverter(typeof(ConverterStrToInt))]
    public int rating_val{ get; set; }
    public string user_id{ get; set; }
    
    // public Rating(string movieId, int ratingVal)
    // {
    //     movie_id = movieId;
    //     rating_val = ratingVal;
    // }
}

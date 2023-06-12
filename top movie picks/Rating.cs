using CsvHelper.Configuration.Attributes;

namespace top_movie_picks;

public class Rating
{
    public string movie_id{ get; set; }
    [TypeConverter(typeof(ConverterStrToInt))]
    public int rating_val{ get; set; }
    public string user_id{ get; set; }
}

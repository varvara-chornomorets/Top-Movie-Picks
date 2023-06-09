using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration.Attributes;

using CsvHelper;

namespace top_movie_picks;

public class UserRaw
{
    public string _id { get; set; }
    public string display_name { get; set; }

    [TypeConverter(typeof(ConverterStrToInt))]
    public int num_ratings_pages { get; set; }

    [TypeConverter(typeof(ConverterStrToInt))]
    public int num_reviews { get; set; }

    public string username { get; set; }

}




using CsvHelper.Configuration.Attributes;
using Microsoft.Win32.SafeHandles;

namespace top_movie_picks;

public class Rating
{
    public string movie_id{ get; set; }
    [TypeConverter(typeof(ConverterStrToInt))]
    public int rating_val{ get; set; }
    public string user_id{ get; set; }
    
    
    // i haven't used it yet, but it may be useful
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Rating)obj;

        return movie_id == other.movie_id &&
               rating_val == other.rating_val &&
               user_id == other.user_id;
    }
}

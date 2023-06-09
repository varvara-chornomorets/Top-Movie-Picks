using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class ConverterStrToInt : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        if (int.TryParse(text, out var value))
        {
            return value;
        }
        else
        {
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
using Microsoft.EntityFrameworkCore.Query;

namespace SurveyApplication.Domain.Common;

public static class JsonSqlExtensions
{
    public static string JsonValue(string column, [NotParameterized] string path)
    {
        throw new NotSupportedException();
    }

    public static int IsJson(string column)
    {
        throw new NotSupportedException();
    }
}
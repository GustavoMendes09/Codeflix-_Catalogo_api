
using System.Runtime.CompilerServices;

namespace FC.Codeflix.Catalog.EndToEndTests.Extensions
{
    internal static class DateTimeExtensions
    {
        public static System.DateTime TrimMillisseconds(this System.DateTime dateTime) =>
            new System.DateTime(System.DateTime.Now.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Kind
                );

    }
}

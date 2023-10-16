using FC.Codeflix.catalog.Api.Extensions.String;
using System.Text.Json;

namespace FC.Codeflix.catalog.Api.Configurations.Policies
{
    public class JsonSnakeCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        => name.ToSnakeCase();
    }
}

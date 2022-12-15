using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.ListCategoryApiTest
{
    [CollectionDefinition(nameof(ListCategoryApiTestFixture))]
    public class ListCategoryApiTestFixtureCollection : ICollectionFixture<ListCategoryApiTestFixture> { }
    public class ListCategoryApiTestFixture : CategoryBaseFixture
    {
        public CreateCategoryInput getExampleInput()
            => new(
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    getRandomBoolean()
                );
    }
}

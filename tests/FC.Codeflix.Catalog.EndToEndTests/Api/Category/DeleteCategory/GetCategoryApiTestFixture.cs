using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryApiTestFixture))]
    public class DeleteCategoryApiTestFixtureCollection : ICollectionFixture<DeleteCategoryApiTestFixture> { }
    public class DeleteCategoryApiTestFixture : CategoryBaseFixture
    {
        public CreateCategoryInput getExampleInput()
            => new(
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    getRandomBoolean()
                );
    }
}

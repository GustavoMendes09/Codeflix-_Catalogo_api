using FC.Codeflix.catalog.Api.ApiModels.Category;
using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using System;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryApiTestFixture))]
    public class UpdateCategoryApiTestFixtureCollection : ICollectionFixture<UpdateCategoryApiTestFixture> { }
    public class UpdateCategoryApiTestFixture : CategoryBaseFixture
    {
        public UpdateCategoryApiInput GetExampleInput() =>
                new (
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    getRandomBoolean()
                    );
            
        
    }
}

﻿using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.GetCategory
{
    [CollectionDefinition(nameof(GetCategoryApiTestFixture))]
    public class GetCategoryApiTestFixtureCollection : ICollectionFixture<GetCategoryApiTestFixture> { }
    public class GetCategoryApiTestFixture : CategoryBaseFixture
    {
        public CreateCategoryInput getExampleInput()
            => new(
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    getRandomBoolean()
                );
    }
}

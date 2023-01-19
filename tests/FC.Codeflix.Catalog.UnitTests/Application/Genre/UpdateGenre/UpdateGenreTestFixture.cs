using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Genre.CreateGenre;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Application.Genre.Common;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Application.Genre.UpdateGenre
{
    [CollectionDefinition(nameof(UpdateGenreTestFixture))]

    public class UpdateGenreTestFixtureCollection : ICollectionFixture<UpdateGenreTestFixture> {}
    public class UpdateGenreTestFixture : GenreUseCasesBaseFixture
    {
        public CreateGenreInput GetExampleInputWithCategories()
        {
            var numberOfCategoriesIds = (new Random()).Next(0, 10);
            var categoriesIds = Enumerable.Range(1, numberOfCategoriesIds)
                .Select(_ => Guid.NewGuid())
                .ToList();

            return new (
                        GetValidGenreName(),
                        GetRandomBoolean(),
                        categoriesIds
                        );
        }

        public CreateGenreInput GetExampleInput()
            => new CreateGenreInput(
                GetValidGenreName(),
                GetRandomBoolean()
                );

        public CreateGenreInput GetExampleInput(string? name)
            => new CreateGenreInput(
                name!,
                GetRandomBoolean()
                );

       
    }
}

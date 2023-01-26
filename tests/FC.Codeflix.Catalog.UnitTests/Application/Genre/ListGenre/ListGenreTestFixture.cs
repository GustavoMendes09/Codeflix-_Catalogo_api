using FC.Codeflix.Catalog.UnitTests.Application.Genre.Common;
using FC.Codeflix.Catalog.Application.UseCases.Genre.ListGenres;
using Xunit;
using System;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;


namespace FC.Codeflix.Catalog.UnitTests.Application.Genre.ListGenre
{
    [CollectionDefinition(nameof(ListGenreTestFixture))]

    public class ListGenreTestFixtureCollection : ICollectionFixture<ListGenreTestFixture> { }


    public class ListGenreTestFixture : GenreUseCasesBaseFixture
    {
        public ListGenresInput GetExampleInput()
        {
            var random = new Random();
            return new ListGenresInput(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Commerce.ProductName(),
                dir: random.Next(0, 10) > 5 ?
                    SearchOrder.Asc : SearchOrder.Desc
            );
        }

    }
}

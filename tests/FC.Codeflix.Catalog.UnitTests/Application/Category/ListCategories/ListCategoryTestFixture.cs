using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;
using Entity = FC.Codeflix.Catalog.Domain.Entity;
using System.Collections.Generic;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories
{
    [CollectionDefinition(nameof(ListCategoryTestFixture))]
    public class ListCategoryTestFixtureCollection : ICollectionFixture<ListCategoryTestFixture>
    { }
    public class ListCategoryTestFixture : CategoryUseCasesBaseFixture
    {
        public List<Entity.Category> GetExampleCategoriesList(int legth = 10)
        {
            var list = new List<Entity.Category>();
            for (int i = 0; i < legth; i++)
                list.Add(GetExampleCategory());

            return list;
        }

        public ListCategoriesInput GetExampleInput()
        {
            return new ListCategoriesInput(
                page: Faker.Random.Int(1, 10),
                perPage: Faker.Random.Int(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Commerce.ProductName(),
                dir: Faker.Random.Int(0, 10) > 5 ?
                SearchOrder.Asc : SearchOrder.Desc
                );
        }
    }
}

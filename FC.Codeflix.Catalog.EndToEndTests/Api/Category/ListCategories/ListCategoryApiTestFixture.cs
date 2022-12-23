﻿using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using System.Collections.Generic;
using System.Linq;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.ListCategoryApiTest
{
    [CollectionDefinition(nameof(ListCategoryApiTestFixture))]
    public class ListCategoryApiTestFixtureCollection : ICollectionFixture<ListCategoryApiTestFixture> { }
    public class ListCategoryApiTestFixture : CategoryBaseFixture
    {
        public List<DomainEntity.Category> GetExampleCategoriesListWithNames(List<string> names)
            => names.Select(name =>
            {
                var category = GetExampleCategory();
                category.Update(name);
                return category;
            }).ToList();

        public List<DomainEntity.Category> CloneCategoriesListOrdered(
            List<DomainEntity.Category> categoriesList,
            string orderBy,
            SearchOrder order
        ){
            var listClone = new List<DomainEntity.Category>(categoriesList);
            var orderedEnumerable = (orderBy.ToLower(), order) switch
            {
                ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
                ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name).ThenBy(x => x.Id),
                ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
                _ => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
            };
            return orderedEnumerable.ToList();
        }
    }
}

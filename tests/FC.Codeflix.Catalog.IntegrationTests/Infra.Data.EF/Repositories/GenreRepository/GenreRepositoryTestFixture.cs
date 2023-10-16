﻿using FC.Codeflix.Catalog.IntegrationTests.Base;
using System.Collections.Generic;
using System;
using Xunit;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Entity;
using System.Linq;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.GenreRepository
{
    [CollectionDefinition(nameof(GenreRepositoryTestFixture))]
    public class GenreRepositoryTestFixtureCollection : ICollectionFixture<GenreRepositoryTestFixture>
    { }
    public class GenreRepositoryTestFixture : BaseFixture
    {
        public string GetValidGenreName()
            => Faker.Commerce.Categories(1)[0];

        public DomainEntity.Genre GetExampleGenre(bool? isActive = null, List<Guid>? categoriesIds = null)
        {
            var genre = new DomainEntity.Genre(GetValidGenreName(), isActive ?? GetRandomBoolean());
            categoriesIds?.ForEach(genre.AddCategory);
            return genre;
        }

        public bool GetRandomBoolean() =>
            new Random().NextDouble() < 0.5;

        public string GetValidCategoryName()
        {
            var categoryName = "";
            while (categoryName.Length < 3)
                categoryName = Faker.Commerce.Categories(1)[0];

            if (categoryName.Length > 255)
                categoryName = categoryName[..255];
            return categoryName;
        }


        public string GetValidCategoryDescription()
        {
            var categoryDescription = Faker.Commerce.ProductDescription();

            if (categoryDescription.Length > 10_000)
                categoryDescription = categoryDescription[..10_000];

            return categoryDescription;
        }

        public Category GetExampleCategory()
            => new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolean()
                );

        public List<Category> GetExampleCategoriesList(int lenght = 10)
            => Enumerable
            .Range(1, lenght)
            .Select(_ => GetExampleCategory())
            .ToList();
    }
}
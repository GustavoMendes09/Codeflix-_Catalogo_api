using FC.Codeflix.Catalog.EndToEndTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common
{
    public class CategoryBaseFixture : BaseFixture
    {
        public CategoryPersistence Persistence;

        public CategoryBaseFixture() : base()
        {
            Persistence = new CategoryPersistence(CreateDbContext());
        }

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
            var categoryDescription =
                Faker.Commerce.ProductDescription();
            if (categoryDescription.Length > 10_000)
                categoryDescription =
                    categoryDescription[..10_000];
            return categoryDescription;
        }

        public bool getRandomBoolean()
            => new Random().NextDouble() < 0.5;

        public string GetInvalidNameTooShort() =>
            Faker.Commerce.ProductName().Substring(0, 2);

        public string GetInvalidNameTooLong()
        {
            var tooLongNameForCategory = Faker.Commerce.ProductName();

            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";

            return tooLongNameForCategory;
        }

        public string GetInvalidDescriptionTooLong()
        {
            var tooLongDescriptionForCategory = Faker.Commerce.ProductDescription();

            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";

            return tooLongDescriptionForCategory;
        }

        public DomainEntity.Category GetExampleCategory() =>
            new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                getRandomBoolean()
                );

        public List<DomainEntity.Category> GetExampleCategoryList(int listLenght = 15) =>
            Enumerable.Range(1, listLenght).Select(_ => new DomainEntity.Category(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                getRandomBoolean()
                )).ToList();
    }
}

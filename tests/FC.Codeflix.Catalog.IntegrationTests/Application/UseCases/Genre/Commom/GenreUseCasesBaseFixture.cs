using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FC.Codeflix.Catalog.IntegrationTests.Base;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Genre.Commom;

public class GenreUseCasesBaseFixture : BaseFixture
{
    public string GetValidGenreName()
       => Faker.Commerce.Categories(1)[0];

    public DomainEntity.Genre GetExampleGenre(
    bool? isActive = null,
    List<Guid>? categoriesIds = null,
    string? name = null
    )
    {
        var genre = new DomainEntity.Genre(
            name ?? GetValidGenreName(),
            isActive ?? GetRandomBoolean()
        );
        categoriesIds?.ForEach(genre.AddCategory);
        return genre;
    }

    public bool GetRandomBoolean() =>
        new Random().NextDouble() < 0.5;

    public List<DomainEntity.Genre> GetExampleListGenres(int count = 10)
    => Enumerable
        .Range(1, count)
        .Select(_ => GetExampleGenre())
        .ToList();


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

    public DomainEntity.Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
            );

    public List<DomainEntity.Category> GetExampleCategoriesList(int lenght = 10)
        => Enumerable
        .Range(1, lenght)
        .Select(_ => GetExampleCategory())
        .ToList();
}

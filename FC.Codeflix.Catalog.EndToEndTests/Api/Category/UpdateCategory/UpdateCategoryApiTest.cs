using FC.Codeflix.catalog.Api.ApiModels.Category;
using FC.Codeflix.Catalog.Application.UseCases.Category.Commom;
using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.UpdateCategory
{


    [Collection(nameof(UpdateCategoryApiTestFixture))]
    public class UpdateCategoryApiTest : IDisposable
    {
        private readonly UpdateCategoryApiTestFixture _fixture;
        public UpdateCategoryApiTest(UpdateCategoryApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(UpdateCategory))]
        [Trait("EndToEnd/API", "Category/Update - Endpoints")]
        public async void UpdateCategory()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var exampleCategory = exampleCategoriesList[10];
            var input = _fixture.GetExampleInput();

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOutput>($"/categories/{exampleCategory.Id}", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode) StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbCategory = await _fixture.Persistence.GetById(output.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
            dbCategory.IsActive.Should().Be((bool)input.IsActive!);
        }

        [Fact(DisplayName = nameof(UpdateCategoryOnlyName))]
        [Trait("EndToEnd/API", "Category/Update - Endpoints")]
        public async void UpdateCategoryOnlyName()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var exampleCategory = exampleCategoriesList[10];
            var input = new UpdateCategoryApiInput(_fixture.GetValidCategoryName());

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOutput>($"/categories/{exampleCategory.Id}", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Name.Should().Be(input.Name);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbCategory = await _fixture.Persistence.GetById(output.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(input.Name);
        }

        [Fact(DisplayName = nameof(UpdateCategoryNameAndDescription))]
        [Trait("EndToEnd/API", "Category/Update - Endpoints")]
        public async void UpdateCategoryNameAndDescription()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var exampleCategory = exampleCategoriesList[10];
            var input = new UpdateCategoryApiInput(_fixture.GetValidCategoryName(), exampleCategory.Description!);

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOutput>($"/categories/{exampleCategory.Id}", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbCategory = await _fixture.Persistence.GetById(output.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
        }

        [Fact(DisplayName = nameof(ErrorWhenNotFound))]
        [Trait("EndToEnd/API", "Category/Update - Endpoints")]
        public async void ErrorWhenNotFound()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var randomGuid = Guid.NewGuid();
            var input = _fixture.GetExampleInput();

            var (response, output) = await _fixture.ApiClient.Put<ProblemDetails>($"/categories/{randomGuid}", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Type.Should().Be("NotFound");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Detail.Should().Be($"Category '{randomGuid}' not found.");
        }

        [Theory(DisplayName = nameof(ErrorWhenCantInstantiateAggregate))]
        [Trait("EndToEnd/API", "Category/Update - Endpoints")]
        [MemberData(nameof(UpdateCategoryApiTestDataGenerator.GetInvalidInputs),
            MemberType = typeof(UpdateCategoryApiTestDataGenerator)
            )]
        public async void ErrorWhenCantInstantiateAggregate(UpdateCategoryApiInput input, string expectedDetails)
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList(20);
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var exampleCategoriy = exampleCategoriesList[10];

            var (response, output) = await _fixture.ApiClient.Put<ProblemDetails>($"/categories/{exampleCategoriy.Id}", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status422UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be(expectedDetails);
        }


        public void Dispose() =>
            _fixture.CleanPersistence();
    }
}

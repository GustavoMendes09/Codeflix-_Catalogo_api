﻿using FC.Codeflix.Catalog.Application.UseCases.Category.Commom;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.DeleteCategory
{
    [Collection(nameof(DeleteCategoryApiTestFixture))]
    public class DeleteCategoryApiTest : IDisposable
    {
        private readonly DeleteCategoryApiTestFixture _fixture;
        public DeleteCategoryApiTest(DeleteCategoryApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("EndToEnd/API", "Category/Get - Endpoints")]
        public async Task DeleteCategory()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var exampleCategory = exampleCategoriesList[10];

            var (response, output) = await _fixture.ApiClient.Delete<object>($"/categories/{exampleCategory.Id}");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status204NoContent);
            output.Should().BeNull();
            var persistenceCategory = await _fixture.Persistence.GetById(exampleCategory.Id);
            persistenceCategory.Should().BeNull();
        }

        [Fact(DisplayName = nameof(ErrorWhenNotFound))]
        [Trait("EndToEnd/API", "Category/Get - Endpoints")]
        public async Task ErrorWhenNotFound()
        {
            var exampleCategoriesList = _fixture.GetExampleCategoryList();
            await _fixture.Persistence.InsertList(exampleCategoriesList);
            var randomGuid = Guid.NewGuid();

            var (response, output) = await _fixture.ApiClient.Delete<ProblemDetails>($"/categories/{randomGuid}");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output!.Type.Should().Be($"NotFound");
            output!.Status.Should().Be(StatusCodes.Status404NotFound);
            output!.Detail.Should().Be($"Category '{randomGuid}' not found.");
        }
        public void Dispose() =>
            _fixture.CleanPersistence();
    }
}
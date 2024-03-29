﻿using FC.Codeflix.Catalog.Application.UseCases.Category.Commom;
using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Entity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories
{
    [Collection(nameof(ListCategoryTestFixture))]
    public class ListCategoryTest
    {
        private readonly ListCategoryTestFixture _fixture;

        public ListCategoryTest(ListCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(List))]
        [Trait("Application", "ListCategory - Use Case")]
        public async Task List()
        {
            var categoriesExampleList = _fixture.GetExampleCategoriesList();
            var repositoryMock = _fixture.GetRepositoryMock();
            var input = _fixture.GetExampleInput();

            var outputRepositorySearch = new SearchOutput<Entity.Category>(
                        currentPage: input.Page,
                        perPage: input.PerPage,
                        items: (IReadOnlyList<Entity.Category>)categoriesExampleList,
                        total: new Random().Next(50, 200)
                    ); ;

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count());
            ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory!.CreatedAt);
            });
            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                    ), Times.Once);
        }

        [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
        [Trait("Application", "ListCategory - Use Case")]
        [MemberData(nameof(ListCategoriesTestDataGenerator.GetInputsWithoutAllParameter),
            parameters: 14,
            MemberType = typeof(ListCategoriesTestDataGenerator)
            )]
        public async Task ListInputWithoutAllParameters(ListCategoriesInput input)
        {
            var categoriesExampleList = _fixture.GetExampleCategoriesList();
            var repositoryMock = _fixture.GetRepositoryMock();

            var outputRepositorySearch = new SearchOutput<Entity.Category>(
                        currentPage: input.Page,
                        perPage: input.PerPage,
                        items: (IReadOnlyList<Entity.Category>)categoriesExampleList,
                        total: new Random().Next(50, 200)
                    ); ;

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count());
            ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory!.CreatedAt);
            });
            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                    ), Times.Once);
        }

        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("Application", "ListCategory - Use Case")]
        public async Task ListOkWhenEmpty()
        {
            var categoriesExampleList = _fixture.GetExampleCategoriesList();
            var repositoryMock = _fixture.GetRepositoryMock();
            var input = _fixture.GetExampleInput();

            var outputRepositorySearch = new SearchOutput<Entity.Category>(
                        currentPage: input.Page,
                        perPage: input.PerPage,
                        items: new List<Entity.Category>().AsReadOnly(),
                        total: 0
                    ); ;

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);

            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                           searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.Order == input.Dir
                        ),
                    It.IsAny<CancellationToken>()
                    ), Times.Once);
        }
    }
}

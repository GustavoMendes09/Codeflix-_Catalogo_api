using FC.Codeflix.Catalog.Application.Exceptions;
using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.Domain.Entity;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Entity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTest
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory(DisplayName = nameof(UpdateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(nameof(
            UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
            )]
        public async void UpdateCategory(Entity.Category exampleCategory, UpdateCategoryInput input)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            repositoryMock.Setup(r => r
                .Get(exampleCategory.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(exampleCategory);

            var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);

            repositoryMock.Verify(u => u
            .Get(exampleCategory.Id, It.IsAny<CancellationToken>()), Times.Once);

            repositoryMock.Verify(u => u
            .Update(exampleCategory, It.IsAny<CancellationToken>()), Times.Once);

            unitOfWorkMock.Verify(u =>
            u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory(DisplayName = nameof(UpdateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(nameof(
            UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
            )]
        public async void UpdateCategoryWithoutProvingIsActive(Entity.Category exampleCategory, UpdateCategoryInput exampleInput)
        {
            var input = new UpdateCategoryInput(exampleInput.Id, exampleInput.Name, exampleInput.Description);
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            repositoryMock.Setup(r => r
                .Get(exampleCategory.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(exampleCategory);

            var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);

            repositoryMock.Verify(u => u
            .Get(exampleCategory.Id, It.IsAny<CancellationToken>()), Times.Once);

            repositoryMock.Verify(u => u
            .Update(exampleCategory, It.IsAny<CancellationToken>()), Times.Once);

            unitOfWorkMock.Verify(u => u
            .Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task ThrowWhenCategoryNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var input = _fixture.GetValidInput();

            repositoryMock.Setup(r => r
                .Get(input.Id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"category '{input.Id}' not found"));

            var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();
        }
    }
}

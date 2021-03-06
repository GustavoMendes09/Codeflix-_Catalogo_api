using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
               unitOfWorkMock.Object
                );

            var input = _fixture.GetInput();

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(r =>
            r.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once);

            unitOfWorkMock.Verify(u =>
            u.Commit(It.IsAny<CancellationToken>()),
            Times.Once);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstatiateAggregate))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(nameof(GetInvalidInputs))]
        public async void ThrowWhenCantInstatiateAggregate(CreateCategoryInput input, string exceptionMessage)
        {
            var useCase = new UseCases.CreateCategory(
               _fixture.GetRepositoryMock().Object,
              _fixture.GetUnitOfWorkMock().Object
                );

            Func<Task> task = async () =>
            await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
        }



        [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategoryWithOnlyName()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
               unitOfWorkMock.Object
                );

            var input = new CreateCategoryInput(_fixture.GetValidCategoryName());

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(r =>
            r.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once);

            unitOfWorkMock.Verify(u =>
            u.Commit(It.IsAny<CancellationToken>()),
            Times.Once);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be("");
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }





        [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategoryWithOnlyNameAndDescription()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object,
               unitOfWorkMock.Object
                );

            var input = new CreateCategoryInput(_fixture.GetValidCategoryName(),
                _fixture.GetValidCategoryDescription()
                );

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(r =>
            r.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once);

            unitOfWorkMock.Verify(u =>
            u.Commit(It.IsAny<CancellationToken>()),
            Times.Once);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        private static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputList = new List<object[]>();

            var invalidInputShortName = fixture.GetInput();

            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            invalidInputList.Add(new object[]
            {
                invalidInputShortName,
                "Name should be at leats 3 characters long"
            });


            var invalidInputTooLongName = fixture.GetInput();
            var tooLongNameForCategory = fixture.Faker.Commerce.ProductName();

            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {fixture.Faker.Commerce.ProductName()}";

            invalidInputTooLongName.Name = tooLongNameForCategory;

            invalidInputList.Add(new object[]
            {
                invalidInputTooLongName,
                "Name should be less or equal 255 characters long"
            });


            //description não pode ser nula
            var invalidInputDescriptionNull = fixture.GetInput();
            invalidInputDescriptionNull.Description = null;

            invalidInputList.Add(new object[]
            {
                invalidInputDescriptionNull,
                "Description should not be null"
            });

            // description não pode ser maior que 10000 caracters
            var invalidInputTooLongDescription = fixture.GetInput();
            var tooLongDescriptionForCategory = fixture.Faker.Commerce.ProductDescription();

            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {fixture.Faker.Commerce.ProductDescription()}";

            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;

            invalidInputList.Add(new object[]
            {
                invalidInputTooLongDescription,
                "Description should be less or equal 10000 characters long"
            });


            return invalidInputList;

        }
    }
}

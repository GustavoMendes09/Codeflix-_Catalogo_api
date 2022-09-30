using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using System.Collections.Generic;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory
{
    public class UpdateCategoryTestDataGenerator
    {
        public static IEnumerable<object[]> GetCategoriesToUpdate(int time = 10)
        {
            var fixture = new UpdateCategoryTestFixture();
            for (int i = 0; i < time; i++)
            {
                var exampleCategory = fixture.GetExampleCategory();
                var exampleInput = fixture.GetValidInput(exampleCategory.Id);
                yield return new object[] { exampleCategory, exampleInput };
            }
        }
    }
}

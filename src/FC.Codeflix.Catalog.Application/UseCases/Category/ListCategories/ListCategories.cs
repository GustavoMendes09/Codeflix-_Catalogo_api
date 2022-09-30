using FC.Codeflix.Catalog.Application.UseCases.Category.Commom;
using FC.Codeflix.Catalog.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public class ListCategories : IListCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _categoryRepository.Search(
                 new(
                     request.Page,
                     request.PerPage,
                     request.Search,
                     request.Sort,
                     request.Dir
                ),
                 cancellationToken
              );
            return new ListCategoriesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items.Select(x => CategoryModelOutput.FromCategory(x))
                .ToList()
                );
        }
    }
}

using FC.Codeflix.Catalog.Application.Common;
using FC.Codeflix.Catalog.Application.UseCases.Genre.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Genre.ListGenres
{
    public class ListGenresOutput : PaginatedListOutput<GenreModelOutput>
    {
        public ListGenresOutput(int page, int perPage, int total, IReadOnlyList<GenreModelOutput> items) : base(page, perPage, total, items)
        {
        }

        public static ListGenresOutput FromSearchOutput(SearchOutput<DomainEntity.Genre> searchOutput)
        => new (
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items.Select(GenreModelOutput.FromGenre).ToList()
                );
    }
}

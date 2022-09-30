using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository
{
    public class SearchOutput<T> where T : AggregateRoot
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public SearchOutput(int currentPage, int perPage, int total, IReadOnlyList<T> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}

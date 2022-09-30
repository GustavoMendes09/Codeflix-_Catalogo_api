using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.Common
{
    public abstract class PaginatedListOutput<T>
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public PaginatedListOutput(int page, int perPage, int total, IReadOnlyList<T> items)
        {
            Page = page;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}

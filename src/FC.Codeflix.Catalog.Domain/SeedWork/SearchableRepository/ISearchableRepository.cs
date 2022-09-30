using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository
{
    public interface ISearchableRepository<T> where T : AggregateRoot
    {
        Task<SearchOutput<T>> Search(SearchInput input, CancellationToken cancellationToken);
    }
}

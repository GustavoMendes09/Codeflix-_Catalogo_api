using FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<T> : IRepository where T : AggregateRoot
    {
        public Task Insert(T aggregate, CancellationToken cancellationToken);
        public Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
        public Task DeleteAsync(T aggregate, CancellationToken cancellationToken);
        public Task Update(T aggregate, CancellationToken cancellationToken);
    }
}

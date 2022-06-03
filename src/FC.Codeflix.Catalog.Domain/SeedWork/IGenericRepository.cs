namespace FC.Codeflix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<T> : IRepository
    {
        public Task Insert(T aggregate, CancellationToken cancellationToken);
    }
}

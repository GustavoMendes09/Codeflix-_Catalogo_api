namespace FC.Codeflix.Catalog.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken);
        public Task RollBack(CancellationToken cancellationToken);
    }
}

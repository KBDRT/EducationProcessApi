namespace Application.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public int GetRecordsCount();

    }
}

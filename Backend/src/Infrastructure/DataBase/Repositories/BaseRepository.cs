using Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public int GetRecordsCount()
        {
            return _dbSet.AsNoTracking().Count();
        }
    }
}

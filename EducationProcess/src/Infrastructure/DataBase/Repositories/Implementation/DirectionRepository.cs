using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class DirectionRepository : IDirectionRepository
    {
        private readonly ApplicationContext _context;

        public DirectionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(ArtDirection artDirection)
        {
            _context.ArtDirections.Add(artDirection);
            await _context.SaveChangesAsync();
            return artDirection.Id;
        }

        public async Task<List<ArtDirection>> GetAsync()
        {
            return await _context.ArtDirections.AsNoTracking().ToListAsync();
        }

        public async Task<ArtDirection?> GetByIdAsync(Guid id)
        {
            var direction = await _context.ArtDirections
                                        //.AsNoTracking()
                                        .SingleOrDefaultAsync(i => i.Id == id) ?? null;

            return direction;
        }

    }
}

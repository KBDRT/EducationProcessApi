using EducationProcessAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EducationProcessAPI.Application.Abstractions.Repositories;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class UnionRepository : IUnionRepository
    {
        private readonly ApplicationContext _context;

        public UnionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(ArtUnion artUnion)
        {
            _context.ArtUnions.Add(artUnion);
            await _context.SaveChangesAsync();
            return artUnion.Id;
        }

        public async Task<ArtUnion?> GetByIdAsync(Guid id)
        {
            var union = await _context.ArtUnions
                              //.AsNoTracking()
                              .SingleOrDefaultAsync(i => i.Id == id) ?? null;

            return union;
        }

        public async Task<List<ArtUnion>> GetByTeacherIdAsync(Guid teacherId)
        {
           return await _context.ArtUnions
                                .AsNoTracking()
                                .Where(x => x.Teacher.Id == teacherId)
                                .Include(x => x.Groups)
                                .ToListAsync();
        }
    }
}

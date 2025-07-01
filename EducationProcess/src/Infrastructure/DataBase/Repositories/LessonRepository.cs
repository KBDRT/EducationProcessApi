using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationContext _context;

        public LessonRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Lesson newLesson)
        {
            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();
            return newLesson.Id;
        }

        public async Task<Lesson?> GetByIdAsync(Guid id)
        {
            return await _context.Lessons
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == id) ?? null;
        }

        public async Task<List<Lesson>?> GetByGroupIdAsync(Guid groupId)
        {
            return await _context.Lessons
                              .AsNoTracking()
                              .Where(x => x.Group.Id == groupId)
                              .ToListAsync() ?? null;
        }

        public async Task<Lesson?> GetWithIncludesByIdAsync(Guid id)
        {
            return await _context.Lessons
                            .Include(x => x.Group)
                            .ThenInclude(x => x.ArtUnion)
                            .ThenInclude(x => x.Teacher)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == id) ?? null;
        }
    }
}

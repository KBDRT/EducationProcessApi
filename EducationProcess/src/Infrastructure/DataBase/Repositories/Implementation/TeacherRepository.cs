using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.DTO;
using Microsoft.EntityFrameworkCore;
using EducationProcessAPI.Application.Abstractions.Repositories;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;

        public TeacherRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Teacher newTeacher)
        {
            _context.Teachers.Add(newTeacher);
            await _context.SaveChangesAsync();

            return newTeacher.Id;
        }

        public async Task<int> DeleteAllAsync()
        {
            var count = _context.Teachers.Count();
            await _context.Teachers.ExecuteDeleteAsync();

            return count;
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            var teacher = await _context.Teachers
                                //.AsNoTracking()
                                .SingleOrDefaultAsync(i => i.Id == id) ?? null;

            return teacher;
        }


        public async Task<List<Teacher>> GetAfterWithSizeAsync(GetAfterIdWithPaginationDto request)
        {
            List<Teacher> teachers = await _context.Teachers
                                        .Where(x => x.Id > request.afterId)
                                        //.OrderBy(x => x.Id)
                                        .Take(request.size)
                                        .AsNoTracking().ToListAsync();

            return teachers;
        }

        public async Task<Teacher?> UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        public async Task<List<Teacher>?> GetByEduYearAsync(int year)
        {
            var teachers = await _context.Teachers
                                        .Include(a => a.Union)
                                        .ThenInclude(x => x.Groups.Where(t => t.StartYear == year))
                                        .AsNoTracking()
                                        .ToListAsync() ?? null;
            return teachers;
        }

    }
}

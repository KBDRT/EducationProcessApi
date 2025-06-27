using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class GroupRepository : IGroupRepository
    {

        private readonly ApplicationContext _context;

        public GroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Group newGroup)
        {
            _context.Groups.Add(newGroup);
            await _context.SaveChangesAsync();
            return newGroup.Id;
        }

        public async Task CreateRangeAsync(List<Group> groups)
        {
            _context.Groups.AddRange(groups);
            await _context.SaveChangesAsync();
        }

        public async Task<Group?> GetByIdAsync(Guid id)
        {
            var group = await _context.Groups
                             .SingleOrDefaultAsync(i => i.Id == id) ?? null;

            return group;
        }

    }
}

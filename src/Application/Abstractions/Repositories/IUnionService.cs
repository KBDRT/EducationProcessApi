using EducationProcessAPI.Domain.Entities;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IUnionRepository
    {
        public Task<Guid> CreateAsync(ArtUnion artUnion);

        public Task<List<ArtUnion>> GetByTeacherIdAsync(Guid teacherId);

        public Task<ArtUnion?> GetByIdAsync(Guid id);

    }
}

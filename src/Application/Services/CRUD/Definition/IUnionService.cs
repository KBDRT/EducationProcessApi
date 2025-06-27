using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IUnionService
    {
        public Task<(AppOperationStatus, Guid)> CreateAsync(CreateUnionDto artUnion);

        public Task<List<GetUnionDto>> GetByTeacherIdAsync(Guid teacherId);

        public Task<ArtUnion?> GetByIdAsync(Guid id);
    }
}

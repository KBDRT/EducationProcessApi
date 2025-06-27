using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface ILessonService
    {
        public Task<Result<Guid>> CreateAsync(LessonDto lesson);

        public Task<List<LessonsDateDto>?> GetByGroupIdAsync(Guid id);

        public Task<LessonShortDto?> GetByIdAsync(Guid id);
    }
}

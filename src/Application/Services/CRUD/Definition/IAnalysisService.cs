
using Application.DTO;
using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IAnalysisService
    {
        public Task<Result<Guid>> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto);

        public Task<Result<Guid>> CreateOptionAsync(Guid criteriaId, string name);

        public Task<List<GetCriteriasWithOptionsDto>> GetByTargetAsync(AnalysisTarget target);

        public Task CreateFromFileAsync(CreateAnalysisFromFileRequest request);

        public Task DeleteByTargetAsync(AnalysisTarget target);

        public Task<(ValidationResult, Guid)> CreateAnalysisDocumentAsync(CreateAnalysisDocumentDto request);
    }

}

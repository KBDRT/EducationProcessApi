using Application;
using Application.DTO;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IAnalysisService
    {
        public Task<ServiceResultManager<Guid>> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto);

        public Task<ServiceResultManager<Guid>> CreateOptionAsync(CreateOptionDto optionDto);

        public Task<ServiceResultManager<List<GetCriteriasWithOptionsDto>>> GetByTargetAsync(AnalysisTarget target);

        public Task<ServiceResultManager> CreateFromFileAsync(CreateAnalysisFromFileRequest analysisDto);

        public Task<ServiceResultManager> DeleteByTargetAsync(AnalysisTarget target);

        public Task<ServiceResultManager<Guid>> CreateAnalysisDocumentAsync(CreateAnalysisDocumentDto analysisDto);
    }

}

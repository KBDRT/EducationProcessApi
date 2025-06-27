using Application;
using Application.DTO;
using CSharpFunctionalExtensions;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entities.Analysis;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using FluentValidation.Results;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IParseFile<AnalysisCriteria> _fileParser;

        public AnalysisService(IAnalysisRepository analysisRepository, 
                                IParseFile<AnalysisCriteria> fileParser,
                                ILessonRepository lessonRepository)
        {
            _analysisRepository = analysisRepository;
            _lessonRepository = lessonRepository;
            _fileParser = fileParser;
        }

        public async Task<(ValidationResult, Guid)> CreateAnalysisDocumentAsync(CreateAnalysisDocumentDto request)
        {
            AnalysisDocument document = new AnalysisDocument(AnalysisTarget.Lesson);

            document.SetDesctiption(request.ResultDescription);
            document.SetDate(request.CheckDate);
            document.SetAuditor(request.AuditorName);

            document.SetLesson(await _lessonRepository.GetWithIncludesByIdAsync(request.LessonId));

            var documentStatus = document.IsDocumentCorrect();

            if (documentStatus.IsValid)
            {
                var id = await _analysisRepository.CreateDocumentAsync(document);

                return (documentStatus, id);
            }
            else
            {
                return (documentStatus, Guid.Empty);
            }
        }

        public async Task<Result<Guid>> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto)
        {
            AnalysisCriteria criteria = new AnalysisCriteria()
            {
                Id = Guid.NewGuid(),
                AnalysisTarget = criteriaDto.AnalysisTarget,
                Description = criteriaDto.Description ?? string.Empty,
                Name = criteriaDto.Name,
                Order = criteriaDto.Order,
                WordMark = criteriaDto.WordMark,
            };

            var id = await _analysisRepository.CreateCriteriaAsync(criteria);

            return id.CheckGuidForEmpty();
        }

        public async Task CreateFromFileAsync(CreateAnalysisFromFileRequest request)
        {
            var file = request.File;

            using var fileStream = file.OpenReadStream();

            var criterias = await _fileParser.ParseAsync(fileStream);

            criterias.ForEach(item => item.AnalysisTarget = request.Target);

            if (request.IsDeletePrev)
            {
                await _analysisRepository.DeleteByTargetAsync(request.Target);
            }

            await _analysisRepository.CreateRangeAsync(criterias);
        }

        public async Task<Result<Guid>> CreateOptionAsync(Guid criteriaId, string name)
        {
            var criteria = await _analysisRepository.GetCriteriaByIdAsync(criteriaId);

            if (criteria == null)
            {
                return Result.Failure<Guid>("Criteria not found"); 
            }    

            CriterionOption option = new CriterionOption()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Criterion = criteria
            };

            var id = await _analysisRepository.CreateOptionAsync(option);

            return id.CheckGuidForEmpty();
        }

        public async Task DeleteByTargetAsync(AnalysisTarget target)
        {
            await _analysisRepository.DeleteByTargetAsync(target);
        }

        public async Task<List<GetCriteriasWithOptionsDto>> GetByTargetAsync(AnalysisTarget target)
        {
            var criterias = await _analysisRepository.GetByTargetAsync(target);

            List <GetCriteriasWithOptionsDto> outputCriterias = new List<GetCriteriasWithOptionsDto>();

            if (criterias == null)
            {
                return outputCriterias;
            }

            criterias = criterias?.OrderBy(c => c.Order).ToList();

            foreach (var criteria in criterias)
            {
                List<GetOptionsDto> options = new List<GetOptionsDto>();

                foreach (var option in criteria.Options)
                {
                    options.Add(new(option.Name, option.Id));
                }

                outputCriterias.Add(new(criteria.Name, criteria.Description, criteria.Id, options));
            }

            return outputCriterias;
        }
    }
}

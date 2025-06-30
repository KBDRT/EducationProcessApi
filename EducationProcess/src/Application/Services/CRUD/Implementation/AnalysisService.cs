using Application;
using Application.DTO;
using Application.Validators.Base;
using Domain.Entities.Analysis;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Microsoft.AspNetCore.Http;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IParseFile<AnalysisCriteria> _fileParser;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public AnalysisService(IAnalysisRepository analysisRepository, 
                                IParseFile<AnalysisCriteria> fileParser,
                                ILessonRepository lessonRepository,
                                IValidatorFactoryCustom validatorFactory)
        {
            _analysisRepository = analysisRepository;
            _lessonRepository = lessonRepository;
            _fileParser = fileParser;
            _validatorFactory = validatorFactory;
        }

        public async Task<ServiceResultManager<Guid>> CreateAnalysisDocumentAsync(CreateAnalysisDocumentDto analysisDto)
        {
            var serviceResult = new ServiceResultManager<Guid>();
            AnalysisDocument document = new AnalysisDocument(AnalysisTarget.Lesson);

            document.SetDesctiption(analysisDto.ResultDescription);
            document.SetDate(analysisDto.CheckDate);
            document.SetAuditor(analysisDto.AuditorName);
            document.SetLesson(await _lessonRepository.GetWithIncludesByIdAsync(analysisDto.LessonId));

            var documentStatus = document.IsDocumentCorrect();

            if (documentStatus.IsValid)
            {
                var id = await _analysisRepository.CreateDocumentAsync(document);
                serviceResult.SetResultData(id);

                return serviceResult;
            }
            else
            {
                serviceResult.AddMessages(documentStatus);
                return serviceResult;
            }
        }

        public async Task<ServiceResultManager<Guid>> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto)
        {
            var validation = _validatorFactory.GetValidator<CreateAnalysisCriteriaDto>().Validate(criteriaDto);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (validation.IsValid)
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
                serviceResult.SetResultData(id);
            }

            return serviceResult;
        }

        public async Task<ServiceResultManager> CreateFromFileAsync(CreateAnalysisFromFileRequest analysisDto)
        {
            var file = analysisDto.File;
            var validation = _validatorFactory.GetValidator<IFormFile>().Validate(analysisDto.File);
            var serviceResult = new ServiceResultManager(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }

            using var fileStream = file.OpenReadStream();

            var criterias = await _fileParser.ParseAsync(fileStream);
            criterias.ForEach(item => item.AnalysisTarget = analysisDto.Target);

            if (analysisDto.IsDeletePrev)
            {
                await _analysisRepository.DeleteByTargetAsync(analysisDto.Target);
            }

            await _analysisRepository.CreateRangeAsync(criterias);

            return serviceResult;
        }

        public async Task<ServiceResultManager<Guid>> CreateOptionAsync(CreateOptionDto optionDto)
        {
            var validation = _validatorFactory.GetValidator<CreateOptionDto>().Validate(optionDto);
            var serviceResult = new ServiceResultManager<Guid>(validation);
            
            if (!validation.IsValid)
            {
                return serviceResult;
            }

            var criteria = await _analysisRepository.GetCriteriaByIdAsync(optionDto.CriteriaId);

            if (criteria == null)
            {
                serviceResult.AddMessage("Criteria не найден", "criteriaId");
                return serviceResult;
            }

            CriterionOption option = new CriterionOption()
            {
                Id = Guid.NewGuid(),
                Name = optionDto.OptionName,
                Criterion = criteria
            };

            var id = await _analysisRepository.CreateOptionAsync(option);
            serviceResult.SetResultData(id);

            return serviceResult;
        }

        public async Task<ServiceResultManager> DeleteByTargetAsync(AnalysisTarget target)
        {
            await _analysisRepository.DeleteByTargetAsync(target);

            return new ServiceResultManager();
        }

        public async Task<ServiceResultManager<List<GetCriteriasWithOptionsDto>>> GetByTargetAsync(AnalysisTarget target)
        {
            var serviceResult = new ServiceResultManager<List<GetCriteriasWithOptionsDto>>();
            var criterias = await _analysisRepository.GetByTargetAsync(target);

            List<GetCriteriasWithOptionsDto> outputCriterias = [];
            serviceResult.SetResultData(outputCriterias);

            if (criterias != null)
            {
                foreach (var criteria in criterias)
                {
                    List<GetOptionsDto> options = new List<GetOptionsDto>();

                    foreach (var option in criteria.Options)
                    {
                        options.Add(new(option.Name, option.Id));
                    }

                    outputCriterias.Add(new(criteria.Name, criteria.Description, criteria.Id, options));
                }
            }
            else
            {
                serviceResult.AddMessage("Criteria не найден", "criteriaId");
            }

            return serviceResult;
        }
    }
}

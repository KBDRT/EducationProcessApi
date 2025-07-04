using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Analysis.Commands.CreateCriteriasFromFile
{
    public class CreateCriteriasFromFileCommandHandler : IRequestHandler<CreateCriteriasFromFileCommand, CQResult>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IParseFile<AnalysisCriteria> _fileParser;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public CreateCriteriasFromFileCommandHandler(IAnalysisRepository analysisRepository,
                                                     IParseFile<AnalysisCriteria> fileParser,
                                                     IValidatorFactoryCustom validatorFactory)
        {
            _analysisRepository = analysisRepository;
            _fileParser = fileParser;
            _validatorFactory = validatorFactory;
        }


        public async Task<CQResult> Handle(CreateCriteriasFromFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var validation = _validatorFactory.GetValidator<IFormFile>().Validate(request.File);
            var serviceResult = new CQResult(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }

            using var fileStream = file.OpenReadStream();
            var criterias = await _fileParser.ParseAsync(fileStream);
            criterias.ForEach(item => item.AnalysisTarget = request.Target);

            if (request.IsDeletePrev)
            {
                await _analysisRepository.DeleteByTargetAsync(request.Target);
            }

            await _analysisRepository.CreateRangeAsync(criterias);

            return serviceResult;
        }
    }
}

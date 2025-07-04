
using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.DeleteCriteriasByTarget
{
    public class DeleteCriteriasByTargetCommandHandler : IRequestHandler<DeleteCriteriasByTargetCommand, CQResult>
    {
        private readonly IAnalysisRepository _analysisRepository;

        public DeleteCriteriasByTargetCommandHandler(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<CQResult> Handle(DeleteCriteriasByTargetCommand request, CancellationToken cancellationToken)
        {
            await _analysisRepository.DeleteByTargetAsync(request.Target);
            return new CQResult();
        }
    }
}

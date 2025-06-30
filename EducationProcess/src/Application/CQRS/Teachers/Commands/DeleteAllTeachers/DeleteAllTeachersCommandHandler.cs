using Application.CQRS.Helpers.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Commands.DeleteAllTeachers
{
    public class DeleteAllTeachersCommandHandler : IRequestHandler<DeleteAllTeachersCommand, CQResult<int>>
    {
        public readonly ITeacherRepository _teacherRepository;

        public DeleteAllTeachersCommandHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<CQResult<int>> Handle(DeleteAllTeachersCommand request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<int>();
            var deletedCount = await _teacherRepository.DeleteAllAsync();

            serviceResult.SetResultData(deletedCount);

            return serviceResult;
        }
    }
}

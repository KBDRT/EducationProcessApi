using Application.CQRS.Result.CQResult;
using Application.DTO;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using MediatR;

namespace Application.CQRS.Teachers.Commands.DeleteTeacherById
{
    public class DeleteTeacherByIdCommandHandler : IRequestHandler<DeleteTeacherByIdCommand, CQResult>
    {
        private readonly ITeacherRepository _teacherRepository;

        public DeleteTeacherByIdCommandHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<CQResult> Handle(DeleteTeacherByIdCommand request, CancellationToken cancellationToken)
        {
            var validation = new GuidEmptyValidator().Validate(request.TeacherId);
            var serviceResult = new CQResult<TeacherDto>(validation);

            if (validation.IsValid)
            {
                await _teacherRepository.DeleteByIdAsync(request.TeacherId);
            }

            return serviceResult;
        }
    }
}

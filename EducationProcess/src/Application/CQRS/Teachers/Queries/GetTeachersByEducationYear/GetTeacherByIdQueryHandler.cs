using Application.CQRS.Helpers.CQResult;
using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using DocumentFormat.OpenXml.Bibliography;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersByEducationYear
{
    public class GetTeachersByEducationYearQueryHandler : IRequestHandler<GetTeachersByEducationYearQuery, CQResult<List<TeachersForEduYearDto>>>
    {
        public readonly ITeacherRepository _teacherRepository;

        public GetTeachersByEducationYearQueryHandler(ITeacherRepository teacherRepository,
                                    IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<CQResult<List<TeachersForEduYearDto>>> Handle(GetTeachersByEducationYearQuery request, CancellationToken cancellationToken)
        {
            var validator = new InlineValidator<int>();
            validator.RuleFor(x => x).InclusiveBetween(2000, 9999);
            var validation = validator.Validate(request.EducationYear);

            var serviceResult = new CQResult<List<TeachersForEduYearDto>>(validation);

            if (validation.IsValid)
            {
                var teachers = await _teacherRepository.GetByEduYearAsync(request.EducationYear);

                List<TeachersForEduYearDto> outputTeachers = new List<TeachersForEduYearDto>();

                if (teachers != null)
                {
                    foreach (var teacher in teachers)
                    {
                        List<UnionNameDto> unionsShort = new List<UnionNameDto>();

                        foreach (var union in teacher.Union)
                        {
                            List<GroupsNameDto> groups = new List<GroupsNameDto>();

                            foreach (var group in union.Groups)
                            {
                                GroupsNameDto groupShort = new(group.Name, group.Id);
                                groups.Add(groupShort);
                            }

                            UnionNameDto unionShort = new(union.Name, groups);
                            unionsShort.Add(unionShort);
                        }

                        TeachersForEduYearDto teacherShort = new(teacher.Initials, unionsShort);
                        outputTeachers.Add(teacherShort);
                    }
                }
            }

            return serviceResult;
        }
    }
}

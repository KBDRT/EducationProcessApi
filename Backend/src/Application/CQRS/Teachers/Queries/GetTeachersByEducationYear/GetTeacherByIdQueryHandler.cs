using Application.Cache.Definition;
using Application.Cache.Implementation;
using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersByEducationYear
{
    public class GetTeachersByEducationYearQueryHandler : IRequestHandler<GetTeachersByEducationYearQuery, CQResult<List<TeachersForEduYearDto>>>
    {
        private const CacheManagerTypes _CACHE_TYPE = CacheManagerTypes.DistibutedRedis;

        private readonly ITeacherRepository _teacherRepository;
        private readonly ICacheManager  _cacheManager;

        public GetTeachersByEducationYearQueryHandler(ITeacherRepository teacherRepository,
                                                      ICacheManagerFactory cacheFactory)
        {
            _teacherRepository = teacherRepository;
            _cacheManager = cacheFactory.Create(_CACHE_TYPE);
        }

        public async Task<CQResult<List<TeachersForEduYearDto>>> Handle(GetTeachersByEducationYearQuery request, CancellationToken cancellationToken)
        {
            var validator = new InlineValidator<int>();
            validator.RuleFor(x => x).InclusiveBetween(2000, 9999);
            var validation = validator.Validate(request.EducationYear);

            var serviceResult = new CQResult<List<TeachersForEduYearDto>>(validation);

            if (validation.IsValid)
            {
                var cacheKey = GetCacheKey(request.EducationYear);

                if (_cacheManager.TryGetValue(cacheKey, out List<TeachersForEduYearDto>? teacherFromCache))
                {
                    serviceResult.SetResultData(teacherFromCache);
                    return serviceResult;
                }

                var teachers = await _teacherRepository.GetByEduYearAsync(request.EducationYear);
                if (teachers != null)
                {
                    List<TeachersForEduYearDto> outputTeachers = FillTeachersList(teachers);
                    _cacheManager.Set(cacheKey, outputTeachers);
                    serviceResult.SetResultData(outputTeachers);
                }
            }

            return serviceResult;
        }


        private string GetCacheKey(int year) => $"{CackePrefixKeyConstants.TEACHERS_FOR_YEAR}_{year}";

        private List<TeachersForEduYearDto> FillTeachersList(List<Teacher> teachers)
        {
            List<TeachersForEduYearDto> outputTeachers = new List<TeachersForEduYearDto>();

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

                TeachersForEduYearDto teacherShort = new(teacher.Initials.Short, unionsShort);
                outputTeachers.Add(teacherShort);
            }

            return outputTeachers;
        }
    }
}

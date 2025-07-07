using Application;
using Application.Cache.Definition;
using Application.Cache.Implementation;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class LessonService : ILessonService
    {
        private const CacheManagerTypes _CACHE_TYPE = CacheManagerTypes.Memory;

        private readonly ILessonRepository _lessonRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;
        private readonly ICacheManager _cacheManager;
        public LessonService(ILessonRepository lessonRepository, 
                             IGroupRepository groupRepository,
                             IValidatorFactoryCustom validatorFactory,
                             ICacheManagerFactory cacheFactory)
        {
            _lessonRepository = lessonRepository;
            _groupRepository = groupRepository;
            _validatorFactory = validatorFactory;
            _cacheManager = cacheFactory.Create(_CACHE_TYPE);
        }

        public async Task<ServiceResultManager<Guid>> CreateAsync(LessonDto lesson)
        {
            var validation = _validatorFactory.GetValidator<LessonDto>().Validate(lesson);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (validation.IsValid)
            {
                Group? group = await _groupRepository.GetByIdAsync(lesson.GroupId);
                if (group == null)
                {
                    serviceResult.AddMessage("Group не найден", "GroupId");
                    return serviceResult;
                }

                Lesson newLesson = CreateNewLesson(lesson, group);
                var id = await _lessonRepository.CreateAsync(newLesson);
                serviceResult.SetResultData(id);

                var cacheKey = GetCacheKey(lesson.GroupId);
                _cacheManager.Remove(cacheKey);

            }

            return serviceResult;
        }

        private Lesson CreateNewLesson(LessonDto lesson, Group group)
        {
            return new Lesson
            {
                Id = Guid.NewGuid(),
                Date = lesson.Date,
                FormControl = lesson.FormControl,
                FormExercise = lesson.FormExercise,
                Group = group,
                Name = lesson.Name,
                Place = lesson.Place,
                StudyHours = lesson.StudyHours,
            };
        }

        public async Task<ServiceResultManager<LessonShortDto?>> GetByIdAsync(Guid id)
        {
            var validation = new GuidEmptyValidator().Validate(id);
            var serviceResult = new ServiceResultManager<LessonShortDto?>(validation); 
            
            if (validation.IsValid)
            {
                var lesson = await _lessonRepository.GetByIdAsync(id);
                if (lesson != null)
                {
                    serviceResult.SetResultData(CreateShortLesson(lesson));
                }
            }

            return serviceResult;
        }

        public async Task<ServiceResultManager<List<LessonsDateDto>?>> GetByGroupIdAsync(Guid id)
        {
            var validation = new GuidEmptyValidator().Validate(id);
            var serviceResult = new ServiceResultManager<List<LessonsDateDto>?>(validation);

            if (validation.IsValid)
            {
                var cacheKey = GetCacheKey(id);
                if (_cacheManager.TryGetValue(cacheKey, out List<LessonsDateDto>? lessonsFromMemory))
                {
                    serviceResult.SetResultData(lessonsFromMemory);
                    return serviceResult;
                }

                var lessons = await _lessonRepository.GetByGroupIdAsync(id);
                if (lessons != null)
                {
                    var lessonsShort = CreateLessonsList(lessons);
                    _cacheManager.Set(cacheKey, lessonsShort);
                    serviceResult.SetResultData(lessonsShort);
                }
            }

            return serviceResult;
        }


        private List<LessonsDateDto> CreateLessonsList(List<Lesson> lessons)
        {
            List<LessonsDateDto> lessonsShort = [];
            foreach (var lesson in lessons)
            {
                DateOnly? dateOnly = null;
                if (lesson.Date != null)
                {
                    dateOnly = DateOnly.FromDateTime((DateTime)lesson.Date);
                }

                lessonsShort.Add(new(lesson.Id, dateOnly));
            }

            return lessonsShort;
        }

        private string GetCacheKey(Guid id) => $"{CackePrefixKeyConstants.LESSONS_FOR_GROUP}_{id}";

        private LessonShortDto CreateShortLesson(Lesson fullLesson)
        {
            return new(fullLesson.Id,
                    fullLesson.Name,
                    fullLesson.Date,
                    fullLesson.StudyHours,
                    fullLesson.FormExercise,
                    fullLesson.Place,
                    fullLesson.FormControl);
        }

    }
}

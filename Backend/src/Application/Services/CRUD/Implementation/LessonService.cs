using Application;
using Application.Validators;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;
using Application.Validators.CRUD.Create;
using Application.Validators.CRUD.General;
using Application.Validators.Base;


namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public LessonService(ILessonRepository lessonRepository, 
                             IGroupRepository groupRepository,
                             IValidatorFactoryCustom validatorFactory)
        {
            _lessonRepository = lessonRepository;
            _groupRepository = groupRepository;
            _validatorFactory = validatorFactory;
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

                Lesson newLesson = new Lesson()
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

                var id = await _lessonRepository.CreateAsync(newLesson);
                serviceResult.SetResultData(id);
            }

            return serviceResult;
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
                var lessons = await _lessonRepository.GetByGroupIdAsync(id);
                List<LessonsDateDto> lessonShort = [];

                if (lessons != null)
                {
                    foreach (var lesson in lessons)
                    {
                        DateOnly? dateOnly = null;
                        if (lesson.Date != null)
                        {
                            dateOnly = DateOnly.FromDateTime((DateTime)lesson.Date);
                        }

                        lessonShort.Add(new(lesson.Id, dateOnly));
                    }
                }
            }

            return serviceResult;
        }

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

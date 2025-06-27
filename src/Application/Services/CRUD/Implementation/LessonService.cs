using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;


namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IGroupRepository _groupRepository;

        public LessonService(ILessonRepository lessonRepository, 
                             IGroupRepository groupRepository,
                             IParseFile<Group> fileParser)
        {
            _lessonRepository = lessonRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result<Guid>> CreateAsync(LessonDto lesson)
        {
            Group? group = await _groupRepository.GetByIdAsync(lesson.groupId);

            if (group == null)
            {
                return Result.Failure<Guid>("Group not found");
            }
            else
            {
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

                Guid id = await _lessonRepository.CreateAsync(newLesson);

                return id.CheckGuidForEmpty();
            }
        }

        public async Task<LessonShortDto?> GetByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            return lesson == null ? null : CreateShortLesson(lesson);
        }

        public async Task<List<LessonsDateDto>?> GetByGroupIdAsync(Guid id)
        {
            var lessons = await _lessonRepository.GetByGroupIdAsync(id);

            List<LessonsDateDto> lessonShort = new List<LessonsDateDto>();

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

            return lessonShort;
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

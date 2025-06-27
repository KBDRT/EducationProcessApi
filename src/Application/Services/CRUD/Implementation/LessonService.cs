using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;


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

        public async Task<(AppOperationStatus, Guid)> CreateAsync(LessonDto lesson)
        {
            Group? group = await _groupRepository.GetByIdAsync(lesson.groupId);

            if (group == null)
            {
                return (AppOperationStatus.NotFound, Guid.Empty);
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
                return (AppOperationStatus.Success, id);
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

            foreach(var lesson in lessons)
            {
                DateOnly? dateOnly = null;
                if (lesson.Date != null)
                {
                    dateOnly = DateOnly.FromDateTime((DateTime)lesson.Date);
                }

                lessonShort.Add(new(lesson.Id, dateOnly));
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

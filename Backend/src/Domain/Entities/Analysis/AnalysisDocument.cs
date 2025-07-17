using CSharpFunctionalExtensions;
using Domain.Validators;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using FluentValidation.Results;


namespace Domain.Entities.Analysis
{

    /// <summary>
    ///  Rich Domain Model
    /// </summary>
    public class AnalysisDocument : BaseEntity
    {

        private List<CriterionOption> _selectedOptions = [];

        public AnalysisTarget AnalysisTarget { get; private set; }

        public DateTime CreatedTime { get; private set; }

        public Teacher? Teacher { get; private set; }

        public ArtUnion? ArtUnion { get; private set; }

        public Lesson? Lesson { get; private set; }

        public DateOnly CheckDate { get; private set; }

        public string ResultDescription { get; private set; } = string.Empty;

        public string AuditorName { get; private set; } = string.Empty;

        public int ChildrenCount { get; private set; } = 0;

        public Guid FileId { get; private set; } = Guid.Empty;

        public ICollection<CriterionOption> SelectedOptions => _selectedOptions;


        public AnalysisDocument()
        {
            SetInitData();
        }

        public AnalysisDocument(AnalysisTarget target)
        {
            SetInitData();
            AnalysisTarget = target;
        }

        public void SetChildrenCount(int count)
        {
            if (count > 0)
            {
                ChildrenCount = count;
            }
        }

        private void SetInitData()
        {
            Id = Guid.NewGuid();
            CreatedTime = DateTime.Now;
            _selectedOptions = [];
        }

        public void SetLesson(Lesson? lesson)
        {
            Lesson = lesson;
            ArtUnion = lesson?.Group?.ArtUnion;
            Teacher = lesson?.Group?.ArtUnion?.Teacher;
        }

        public void SetDate(DateOnly date)
        {
            CheckDate = date;
        }

        public void SetDesctiption(string desctiprion)
        {
            if (new StringEmptyValidator().Validate(desctiprion).IsValid)
                ResultDescription = desctiprion;
        }
        
        public void SetAuditor(string name)
        {
            if (new StringEmptyValidator().Validate(name).IsValid)
                AuditorName = name;
        }

        public void AddCriterias(List<AnalysisCriteria>? criterias)
        {
            if (criterias == null)
                return;

            foreach (var criteria in criterias)
            {
                _selectedOptions.AddRange(criteria.Options);
            }
        }

        public ValidationResult IsDocumentCorrect()
        {
           return new AnalysisDocumentValidator().Validate(this);
        }

        public Dictionary<string, string> GetDefaultMarksForOutput()
        {
            return new Dictionary<string, string>
            {
                { "{FIO}", Teacher?.Initials?.Short ?? string.Empty },
                { "{Activity}", ArtUnion?.Name ?? string.Empty },
                { "{Date}", Lesson?.Date?.ToShortDateString()},
                { "{Lesson}", Lesson?.Name ?? string.Empty },
                { "{Teacher}", Teacher?.Initials?.Short ?? string.Empty },
                { "{ChildrenCount}", ChildrenCount.ToString() },
                { "{Auditor}", AuditorName ?? string.Empty },
            };
        }

    }
}

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
    public class AnalysisDocument
    {

        private List<AnalysisCriteria> _selectedCriterias;

        public Guid Id { get; private set; }

        public AnalysisTarget AnalysisTarget { get; private set; }

        public DateTime CreatedTime { get; private set; }

        public Teacher? Teacher { get; private set; }

        public ArtUnion? ArtUnion { get; private set; }

        public Lesson? Lesson { get; private set; }

        public DateOnly CheckDate { get; private set; }

        public string ResultDescription { get; private set; } = string.Empty;

        public string AuditorName { get; private set; } = string.Empty;

        public ICollection<AnalysisCriteria>? SelectedCriterias => _selectedCriterias;

        public AnalysisDocument()
        {
            SetInitData();
        }

        public AnalysisDocument(AnalysisTarget target)
        {
            SetInitData();
            AnalysisTarget = target;
        }

        private void SetInitData()
        {
            Id = Guid.NewGuid();
            CreatedTime = DateTime.Now;
            _selectedCriterias = [];
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

        public void AddCriteria(AnalysisCriteria criteria)
        {
            if (criteria.Options.Count > 0)
            {
                _selectedCriterias.Add(criteria);
            }
        }

        public ValidationResult IsDocumentCorrect()
        {
           return new AnalysisDocumentValidator().Validate(this);
        }
    }
}

namespace EducationProcess.Presentation.Contracts.Teachers
{
    public record UpdateTeacherRequest
    (
        Guid Id,
        BaseTeacherRequest TeacherData
    );
}

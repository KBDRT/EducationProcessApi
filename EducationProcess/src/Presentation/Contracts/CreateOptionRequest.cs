namespace EducationProcess.Presentation.Contracts
{
    public record CreateOptionRequest
    (
        Guid criteriaId,
        string name
    );
}

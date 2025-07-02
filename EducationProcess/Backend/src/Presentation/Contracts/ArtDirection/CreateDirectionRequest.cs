namespace EducationProcess.Presentation.Contracts.ArtDirection
{
    public record CreateDirectionRequest
    (
        string ShortName,
        string FullName,
        string Description
    );
}

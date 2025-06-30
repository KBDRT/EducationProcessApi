namespace EducationProcess.Presentation.Contracts.Group
{
    public record CreateGroupsFromFileRequest
    (
        Guid unionId,
        IFormFile? file,
        int educationYear
    );
}

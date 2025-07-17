
namespace EducationProcessAPI.Application.Fillers
{
    public interface IFillFile<in T> where T : class
    {
        Task<MemoryStream> FillAsync(MemoryStream templateStream, T inputData);
    }
}

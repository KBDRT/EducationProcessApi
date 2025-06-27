
namespace EducationProcessAPI.Application.Parsers
{
    public interface IParseFile<T> where T : class
    {
        Task<List<T>> ParseAsync(Stream fileStream);
    }
}

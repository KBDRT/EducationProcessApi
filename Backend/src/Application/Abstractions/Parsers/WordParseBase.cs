using DocumentFormat.OpenXml.Packaging;

namespace EducationProcessAPI.Application.Parsers
{
    public abstract class WordParseBase<T> : IParseFile<T> where T : class
    {
        public async Task<List<T>> ParseAsync(Stream fileStream)
        {
            using var document = WordprocessingDocument.Open(fileStream, false);
            return await Task.Run(() => ParseFileData(document));
        }

        protected abstract List<T> ParseFileData(WordprocessingDocument document);

    }
}

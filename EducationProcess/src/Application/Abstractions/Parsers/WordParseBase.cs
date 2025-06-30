using DocumentFormat.OpenXml.Packaging;
using EducationProcessAPI.Application.Parsers;

namespace EducationProcessAPI.Application.Parsers
{
    public abstract class WordParseBase<T> : IParseFile<T> where T : class
    {
        protected List<T> _outputData = new List<T>();
        protected WordprocessingDocument? _document;


        public async Task<List<T>> ParseAsync(Stream fileStream)
        {

            _document = WordprocessingDocument.Open(fileStream, false);

            await Task.Run(() => { _outputData = ParseFileData(); });

            return _outputData;
        }

        protected abstract List<T> ParseFileData();

    }
}

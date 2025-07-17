using DocumentFormat.OpenXml.Packaging;
using EducationProcessAPI.Application.Fillers;
namespace Application.Abstractions.Fillers
{
    public abstract class WordFillerBase<T> : IFillFile<T> where T : class
    {
        public async Task<MemoryStream> FillAsync(MemoryStream templateStream, T inputData)
        {
            return await Task.Run(() => FillFileData(templateStream, inputData));
        }

        protected abstract MemoryStream FillFileData(MemoryStream templateStream, T inputData);

    }
}

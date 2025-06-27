using DocumentFormat.OpenXml.Wordprocessing;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Infrastructure.Files.Parsers
{
    public class WordParserGrades : WordParseBase<AnalysisCriteria>
    {

        protected override List<AnalysisCriteria> ParseFileData()
        {
            var tables = _document.MainDocumentPart.Document.Body.Elements<Table>();

            List<AnalysisCriteria> criterias = new List<AnalysisCriteria>();

            foreach (Table table in tables)
            {

                IEnumerable<TableRow> rows = table.Elements<TableRow>();

                foreach (TableRow row in rows)
                {
                    AnalysisCriteria criteria = new AnalysisCriteria();

                    var cells = row.Descendants<TableCell>();

                    criteria.Name = cells.ElementAt(0).InnerText;
                    var variants = (cells.ElementAt(1).InnerText).Split("[Вар]");

                    int count = criterias.Count + 1;
                    criteria.WordMark = "{Score" + count + "}";
                    criteria.Order = count;

                    foreach (var variant in variants)
                    {
                        if (variant.Length > 0)
                        {
                            CriterionOption option = new CriterionOption();
                            option.Name = variant;
                            criteria.Options.Add(option);
                        }
                    }

                    if (criteria.Options.Count > 0)
                    {
                        criterias.Add(criteria);
                    }
                }

            }

            return criterias;
        }
    }
}

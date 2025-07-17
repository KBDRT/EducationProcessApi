
using Application.Abstractions.Fillers;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities.Analysis;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Serilog;

namespace Infrastructure.Files.Filler
{
    public class WordFillerDocument : WordFillerBase<AnalysisDocument>
    {
        private string _FONT_ROMAN = "Times New Roman";

        protected override MemoryStream FillFileData(MemoryStream templateStream, AnalysisDocument documentInfo)
        {
            if (documentInfo == null)
                return new();

            const string templatePath = "Шаблон.docx";
            const string outputPath = "alo.docx";


            var outputStream = new MemoryStream();

            try
            {
                using (var templateDoc = WordprocessingDocument.Open(templateStream, false))
                using (var outputDoc = WordprocessingDocument.Create(outputStream, templateDoc.DocumentType))
                {
                    CopyTemplateContent(templateDoc, outputDoc);
                    ProcessDocumentContent(outputDoc, documentInfo);

                    outputDoc.Save();
                }

                outputStream.Position = 0;
                return outputStream;
            }
            catch
            {
                Log.Error("[]");
                return new();
            }
        }

        private void CopyTemplateContent(WordprocessingDocument source, WordprocessingDocument destination)
        {
            foreach (var part in source.Parts)
            {
                destination.AddPart(part.OpenXmlPart, part.RelationshipId);
            }
        }

        private void ProcessDocumentContent(WordprocessingDocument document, AnalysisDocument documentInfo)
        {
            var body = document.MainDocumentPart.Document.Body;
            var marks = documentInfo.GetDefaultMarksForOutput();

            foreach (var table in body.Elements<Table>())
            {
                ProcessTable(table, marks, documentInfo);
            }
        }

        private void ProcessTable(Table table, Dictionary<string, string> marks, AnalysisDocument documentInfo)
        {
            foreach (var row in table.Elements<TableRow>())
            {
                foreach (var cell in row.Elements<TableCell>())
                {
                    ProcessCell(cell, marks, documentInfo);
                }
            }
        }

        private void ProcessCell(TableCell cell, Dictionary<string, string> marks, AnalysisDocument documentInfo)
        {
            foreach (var paragraph in cell.Elements<Paragraph>())
            {
                ProcessParagraph(paragraph, marks, documentInfo, cell);
            }
        }

        private void ProcessParagraph(Paragraph paragraph, Dictionary<string, string> marks,
                                    AnalysisDocument documentInfo, TableCell parentCell)
        {
            foreach (var run in paragraph.Elements<Run>())
            {
                foreach (var text in run.Elements<Text>())
                {
                    ReplaceMarks(text, marks);
                    ProcessCriteriaMarks(text, documentInfo, parentCell, paragraph);
                }
            }
        }

        private void ReplaceMarks(Text textElement, Dictionary<string, string> marks)
        {
            foreach (var mark in marks)
            {
                if (textElement.Text.Contains(mark.Key))
                {
                    textElement.Text = textElement.Text.Replace(mark.Key, mark.Value);
                }
            }
        }

        private void ProcessCriteriaMarks(Text textElement, AnalysisDocument documentInfo,
                                          TableCell parentCell, Paragraph originalParagraph)
        {
            if (documentInfo?.SelectedOptions == null) return;

            var criterias = documentInfo.SelectedOptions.Select(x => x.Criterion).Distinct().ToList();

            foreach (var criteria in criterias)
            {
                if (textElement.Text.Contains(criteria.WordMark))
                {
                    var options = documentInfo.SelectedOptions.Where(x => x.Criterion == criteria).ToList();
                    textElement.Text = textElement.Text.Replace(criteria.WordMark, string.Empty);
                    AddCriteriaOptions(options, parentCell, originalParagraph);
                }
            }
        }

        private void AddCriteriaOptions(List<CriterionOption> options, TableCell cell, Paragraph afterParagraph)
        {
            foreach (var option in options)
            {
                var newParagraph = CreateOptionParagraph(option.Name);
                cell.InsertAfter(newParagraph, afterParagraph);
            }
        }

        private Paragraph CreateOptionParagraph(string optionText)
        {
            var paragraph = new Paragraph();
            var run = new Run();
            var text = new Text(optionText);

            var runProperties = new RunProperties();
            var runFonts = new RunFonts()
            {
                Ascii = _FONT_ROMAN,
                HighAnsi = _FONT_ROMAN,
                ComplexScript = _FONT_ROMAN,
            };

            runProperties.Append(runFonts);
            run.Append(runProperties);
            run.Append(text);
            paragraph.Append(run);

            return paragraph;
        }
    }
}

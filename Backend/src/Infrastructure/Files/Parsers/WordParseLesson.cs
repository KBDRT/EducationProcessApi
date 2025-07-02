using DocumentFormat.OpenXml.Wordprocessing;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.Parsers;

namespace EducationProcessAPI.Infrastructure.Files.Parsers
{
    public class WordParseLesson : WordParseBase<Group>
    {

        protected override List<Group> ParseFileData()
        {

            var tables = _document.MainDocumentPart.Document.Body.Elements<Table>();

            int i = 1;

            List<Lesson> lessons = new List<Lesson>();  

            List<Group> groups = new List<Group>();

            foreach (Table table in tables)
            {

                Group group = new Group();

                group.Name = $"Группа {i}";

                IEnumerable<TableRow> rows = table.Elements<TableRow>();

                rows = rows.Skip(1);

                foreach (TableRow row in rows)
                {

                    if (row == null || row.Elements<TableCell>().Count() < 9)
                    {
                        continue;
                    }

                    int startIndex = 1;

                    if (row.Elements<TableCell>().Count() == 10)
                        startIndex++;


                    Lesson lesson = new Lesson();

                    var tableCells = row.Elements<TableCell>();

                    try
                    {

                        lesson.Date = GetLessonDate(tableCells.ElementAt(startIndex).InnerText);
                        lesson.FormExercise = tableCells.ElementAt(startIndex + 3).InnerText;
                        if (double.TryParse(tableCells.ElementAt(startIndex + 4).InnerText, out double studyHours))
                        {
                            lesson.StudyHours = studyHours;
                        }
                        lesson.Name = tableCells.ElementAt(startIndex + 5).InnerText;
                        lesson.Place = tableCells.ElementAt(startIndex + 6).InnerText;
                        lesson.FormControl = tableCells.ElementAt(startIndex + 7).InnerText;

                        if (lesson.Date != DateTime.MinValue && !String.IsNullOrEmpty(lesson.Name))
                            group.Lessons.Add(lesson);
                            //lessons.Add(lesson);
                    }
                    catch
                    {
                        continue;
                    }

                }

                if (group.Lessons.Count > 0)
                {
                    groups.Add(group);
                    i++;
                }
            }

            return groups;
        }


        private DateTime GetLessonDate(string InputString)
        {
            var splittedCell = InputString.Split(' ');

            var splittedDate = InputString.Split('.');

            List<int> current_months = [9, 10, 11, 12];


            if (splittedDate.Length >= 2)
            {
                int.TryParse(splittedDate[0], out int day);
                int.TryParse(splittedDate[1], out int month);
                int year = 1;

                if (!current_months.Contains(month))
                {
                    year++;
                }

                try
                {
                    return new DateTime((int)year, month, day);
                }
                catch
                {
                    return new DateTime();
                }

            }
            else
            {
                return new DateTime();
            }
        }


    }
}

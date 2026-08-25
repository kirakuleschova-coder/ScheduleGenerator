using System;
using System.Data;
using System.IO;
using System.Text;

namespace ScheduleGenerator
{
    /// <summary>
    /// Класс ScheduleSaver отвечает за сохранение готового расписания в файлы.
    /// Поддерживает сохранение в текстовый файл (.txt) и в документ Word (.doc).
    /// </summary>
    public class ScheduleSaver
    {
        /// <summary>
        /// Сохраняет расписание в текстовый файл (.txt) в виде таблицы.
        /// </summary>
        /// <param name="fileName">Путь к файлу для сохранения</param>
        /// <param name="schedule">Таблица расписания (DataTable)</param>
        public void SaveToTxt(string fileName, DataTable schedule)
        {
            // StringBuilder - это "умная" строка, которая быстро склеивает много текста
            StringBuilder text = new StringBuilder();

            // 1. Записываем заголовки столбцов (Время, ПН, ВТ, СР...)
            foreach (DataColumn column in schedule.Columns)
            {
                text.Append(column.ColumnName + "\t"); // \t - это символ табуляции (отступ)
            }
            text.AppendLine(); // Переход на новую строку

            // 2. Записываем строки с данными
            foreach (DataRow row in schedule.Rows)
            {
                foreach (DataColumn column in schedule.Columns)
                {
                    text.Append(row[column].ToString() + "\t");
                }
                text.AppendLine();
            }

            // 3. Сохраняем текст в файл в кодировке UTF-8 (чтобы русский текст не превратился в иероглифы)
            File.WriteAllText(fileName, text.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Сохраняет расписание в документ Word (.doc).
        /// </summary>
        /// <param name="fileName">Путь к файлу для сохранения</param>
        /// <param name="schedule">Таблица расписания (DataTable)</param>
        public void SaveToWord(string fileName, DataTable schedule)
        {
            // Создаём HTML-разметку с таблицей
            StringBuilder html = new StringBuilder();

            // Начало HTML-документа и стили для красивой таблицы
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='UTF-8'>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Arial, sans-serif; }");
            html.AppendLine("table { border-collapse: collapse; width: 100%; }");
            html.AppendLine("th, td { border: 1px solid black; padding: 8px; text-align: center; }");
            html.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");

            // Заголовок документа
            html.AppendLine("<h2>Расписание учебных занятий</h2>");

            // Начало таблицы
            html.AppendLine("<table>");

            // 1. Заголовки столбцов
            html.AppendLine("<tr>");
            foreach (DataColumn column in schedule.Columns)
            {
                html.AppendLine($"<th>{column.ColumnName}</th>");
            }
            html.AppendLine("</tr>");

            // 2. Строки с данными
            foreach (DataRow row in schedule.Rows)
            {
                html.AppendLine("<tr>");
                foreach (DataColumn column in schedule.Columns)
                {
                    string cellValue = row[column].ToString();
                    html.AppendLine($"<td>{cellValue}</td>");
                }
                html.AppendLine("</tr>");
            }

            // Конец таблицы и документа
            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            // Сохраняем HTML-код в файл с расширением .doc
            // Word автоматически откроет этот файл и отобразит его как обычную таблицу!
            File.WriteAllText(fileName, html.ToString(), Encoding.UTF8);
        }
    }
}
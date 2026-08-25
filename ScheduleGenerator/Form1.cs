using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ScheduleGenerator
{
    public partial class Form1 : Form
    {
        // Создаём объекты генератора и сохранителя
        ScheduleGenerator generator = new ScheduleGenerator();
        ScheduleSaver saver = new ScheduleSaver();

        // Здесь будет храниться текущее расписание
        System.Data.DataTable currentSchedule;

        // Конструктор формы (вызывается при запуске программы)
        public Form1()
        {
            InitializeComponent();

            // При запуске сразу добавляем тестовые временные слоты (расписание звонков)
            // Это нужно, чтобы программа могла работать без настройки
            generator.AddTimeSlot(new TimeSlot
            {
                StartTime = new TimeSpan(9, 0, 0),      // 09:00
                EndTime = new TimeSpan(10, 30, 0),      // 10:30
                BreakDuration = new TimeSpan(0, 15, 0)  // 15 минут перемена
            });

            generator.AddTimeSlot(new TimeSlot
            {
                StartTime = new TimeSpan(10, 45, 0),    // 10:45
                EndTime = new TimeSpan(12, 15, 0),      // 12:15
                BreakDuration = new TimeSpan(1, 0, 0)   // 1 час перемена (обед)
            });

            generator.AddTimeSlot(new TimeSlot
            {
                StartTime = new TimeSpan(13, 0, 0),     // 13:00
                EndTime = new TimeSpan(14, 30, 0),      // 14:30
                BreakDuration = new TimeSpan(0, 15, 0)  // 15 минут перемена
            });

            generator.AddTimeSlot(new TimeSlot
            {
                StartTime = new TimeSpan(14, 45, 0),    // 14:45
                EndTime = new TimeSpan(16, 15, 0),      // 16:15
                BreakDuration = new TimeSpan(0, 0, 0)   // без перемены
            });

            // Обновляем статус
            lblStatus.Text = "Программа готова к работе. Добавьте группу и загрузите данные.";
        }

        // ============================================================
        // ОБРАБОТЧИКИ СОБЫТИЙ КНОПОК
        // ============================================================

        /// <summary>
        /// Кнопка "Загрузить план" - загружает учебный план из текстового файла
        /// </summary>
        private void btnLoadDisciplines_Click(object sender, EventArgs e)
        {
            // Открываем диалог выбора файла
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы|*.txt";
                ofd.Title = "Выберите файл учебного плана";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Читаем файл в кодировке UTF-8
                        string[] lines = File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                        // Определяем, для какой группы загружаем план
                        // Если группа выбрана в cmbGroup - используем её, иначе создаём тестовую
                        string targetGroup = "П-30";
                        if (cmbGroup.SelectedItem != null)
                        {
                            targetGroup = cmbGroup.SelectedItem.ToString();
                        }

                        // Массив преподавателей для назначения (для демонстрации)
                        string[] teachersPool = { "Иванов И.И.", "Петрова М.А.", "Сидоров А.В.", "Кузнецов В.В." };
                        int teacherIndex = 0;

                        // Обрабатываем каждую строку файла
                        foreach (string line in lines)
                        {
                            // Пропускаем пустые строки
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            // Разделяем строку по точке с запятой
                            string[] parts = line.Split(';');

                            // Проверяем, что строка имеет правильный формат (3 части)
                            if (parts.Length == 3)
                            {
                                try
                                {
                                    // Создаём объект дисциплины
                                    Discipline disc = new Discipline
                                    {
                                        Course = int.Parse(parts[0].Trim()),           // Курс
                                        Name = parts[1].Trim(),                        // Название
                                        HoursPerWeek = int.Parse(parts[2].Trim()),     // Часы в неделю
                                        Teacher = teachersPool[teacherIndex % teachersPool.Length]  // Преподаватель (по кругу)
                                    };

                                    // Добавляем дисциплину в генератор
                                    generator.AddDiscipline(targetGroup, disc);
                                    teacherIndex++;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ошибка в строке: {line}\n{ex.Message}", "Ошибка формата");
                                }
                            }
                        }

                        lblStatus.Text = $"Учебный план для группы {targetGroup} загружен!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "Загрузить препод." - загружает данные о преподавателях из файла
        /// </summary>
        private void btnLoadTeachers_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы|*.txt";
                ofd.Title = "Выберите файл преподавателей";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                        foreach (string line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            string[] parts = line.Split(';');

                            if (parts.Length == 3)
                            {
                                try
                                {
                                    string fio = parts[0].Trim();      // ФИО
                                    string day = parts[1].Trim();      // День недели
                                    int slot = int.Parse(parts[2].Trim());  // Номер слота

                                    // Создаём или находим преподавателя
                                    Teacher teacher = new Teacher { FullName = fio };

                                    // Добавляем доступность
                                    teacher.AddAvailability(day, slot);

                                    // Добавляем в генератор
                                    generator.AddTeacher(teacher);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ошибка в строке: {line}\n{ex.Message}", "Ошибка формата");
                                }
                            }
                        }

                        lblStatus.Text = "Преподаватели загружены!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "Сгенерировать" - запускает генерацию расписания
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Генерация расписания...";

            // Вызываем метод генерации
            currentSchedule = generator.GenerateSchedule();

            // Если расписание создано успешно
            if (currentSchedule != null)
            {
                // Отображаем его в таблице
                dgvSchedule.DataSource = currentSchedule;
                lblStatus.Text = "Расписание успешно сгенерировано!";
            }
            else
            {
                lblStatus.Text = "Ошибка генерации. Проверьте данные.";
            }
        }

        /// <summary>
        /// Кнопка "Сохранить в Word" - сохраняет расписание в файл
        /// </summary>
        private void btnSaveWord_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли расписание для сохранения
            if (currentSchedule == null)
            {
                MessageBox.Show("Сначала сгенерируйте расписание!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Открываем диалог сохранения файла
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Документ Word|*.doc";
                sfd.Title = "Сохранить расписание";
                sfd.FileName = "Расписание.doc";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Сохраняем расписание
                        saver.SaveToWord(sfd.FileName, currentSchedule);
                        lblStatus.Text = $"Расписание сохранено в {sfd.FileName}";
                        MessageBox.Show("Расписание успешно сохранено!", "Готово",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "Редактор препод." - заглушка для редактора преподавателей
        /// </summary>
        private void btnOpenTeacherEditor_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Редактор преподавателей.\n\n" +
                "В данной версии данные загружаются из файла через кнопку 'Загрузить препод.'\n" +
                "Формат файла: ФИО;День;НомерСлота\n" +
                "Пример: Иванов И.И.;ПН;1",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Кнопка "Редактор групп" - добавляет тестовую группу
        /// </summary>
        private void btnOpenGroupEditor_Click(object sender, EventArgs e)
        {
            // Добавляем тестовую группу, чтобы программа могла работать
            generator.AddGroup(new Group
            {
                Name = "П-30",
                Specialty = "Информационные системы",
                Course = 2
            });

            // Добавляем группу в ComboBox для выбора
            cmbGroup.Items.Add("П-30");
            cmbGroup.SelectedIndex = 0;

            lblStatus.Text = "Группа П-30 добавлена!";
            MessageBox.Show("Добавлена тестовая группа П-30 (2 курс, Информационные системы)",
                "Группа добавлена", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
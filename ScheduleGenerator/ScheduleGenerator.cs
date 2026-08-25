using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ScheduleGenerator;  

namespace ScheduleGenerator
{
    public class ScheduleGenerator
    {
        // Список всех учебных групп
        private List<Group> _groups = new List<Group>();

        // Список всех временных слотов
        private List<TimeSlot> _timeSlots = new List<TimeSlot>();

        // Словарь дисциплин: ключ - название группы, значение - список дисциплин
        private Dictionary<string, List<Discipline>> _disciplines = new Dictionary<string, List<Discipline>>();

        // Словарь преподавателей: ключ - ФИО, значение - объект Teacher
        private Dictionary<string, Teacher> _teachers = new Dictionary<string, Teacher>();

        // Генератор случайных чисел
        private Random _random = new Random();

        #region Методы для добавления данных

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }

        public void AddTimeSlot(TimeSlot slot)
        {
            _timeSlots.Add(slot);
        }

        public void AddDiscipline(string groupName, Discipline discipline)
        {
            if (!_disciplines.ContainsKey(groupName))
            {
                _disciplines[groupName] = new List<Discipline>();
            }
            _disciplines[groupName].Add(discipline);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teachers[teacher.FullName] = teacher;
        }

        #endregion

        #region Алгоритм 4.1: Проверка данных

        public bool ValidateData(out string errorMessage)
        {
            errorMessage = "";

            // Шаг 2: Проверка списка групп
            if (_groups.Count == 0)
            {
                errorMessage = "Не добавлено ни одной группы.";
                return false;
            }

            // Шаг 3: Проверка временных слотов
            if (_timeSlots.Count == 0)
            {
                errorMessage = "Не задано расписание звонков.";
                return false;
            }

            if (_timeSlots.Count < 2)
            {
                errorMessage = "Должно быть хотя бы 2 временных слота.";
                return false;
            }

            // Шаг 4: Проверка учебных планов
            foreach (Group group in _groups)
            {
                if (!_disciplines.ContainsKey(group.Name) || _disciplines[group.Name].Count == 0)
                {
                    errorMessage = $"Для группы {group.Name} не загружен учебный план.";
                    return false;
                }
            }

            // Шаг 5: Проверка преподавателей
            foreach (var groupDisciplines in _disciplines.Values)
            {
                foreach (Discipline discipline in groupDisciplines)
                {
                    if (string.IsNullOrEmpty(discipline.Teacher))
                    {
                        errorMessage = $"Для дисциплины {discipline.Name} не назначен преподаватель.";
                        return false;
                    }

                    if (!_teachers.ContainsKey(discipline.Teacher))
                    {
                        errorMessage = $"Преподаватель {discipline.Teacher} не найден.";
                        return false;
                    }
                }
            }

            // Шаг 6: Проверка доступности преподавателей
            foreach (Teacher teacher in _teachers.Values)
            {
                bool hasAvailability = false;

                foreach (var daySlots in teacher.Availability.Values)
                {
                    if (daySlots.Count > 0)
                    {
                        hasAvailability = true;
                        break;
                    }
                }

                // Если нет записей о доступности или все списки пустые
                if (teacher.Availability.Count == 0 || !hasAvailability)
                {
                    // Проверяем, есть ли дни с пустым списком (доступен весь день)
                    bool hasFullDay = false;
                    foreach (var daySlots in teacher.Availability.Values)
                    {
                        if (daySlots.Count == 0)
                        {
                            hasFullDay = true;
                            break;
                        }
                    }

                    if (!hasFullDay && teacher.Availability.Count > 0)
                    {
                        errorMessage = $"Преподаватель {teacher.FullName} не имеет доступных слотов.";
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Алгоритм 4.2: Генерация расписания

        public DataTable GenerateSchedule()
        {
            // Шаг 2: Проверяем данные
            if (!ValidateData(out string error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Шаг 3: Создаём пустое расписание
            Dictionary<string, string> schedule = new Dictionary<string, string>();

            // Шаг 4: Создаём список всех занятий
            List<Discipline> allLessons = new List<Discipline>();

            foreach (Group group in _groups)
            {
                if (_disciplines.ContainsKey(group.Name))
                {
                    foreach (Discipline disc in _disciplines[group.Name])
                    {
                        for (int i = 0; i < disc.HoursPerWeek; i++)
                        {
                            allLessons.Add(new Discipline
                            {
                                Name = disc.Name,
                                HoursPerWeek = 1,
                                Teacher = disc.Teacher,
                                Course = disc.Course
                            });
                        }
                    }
                }
            }

            // Шаг 5: Перемешиваем
            allLessons = allLessons.OrderBy(x => _random.Next()).ToList();

            // Шаг 6: Словарь занятости преподавателей
            Dictionary<string, HashSet<string>> teacherBusy = new Dictionary<string, HashSet<string>>();
            foreach (string teacherName in _teachers.Keys)
            {
                teacherBusy[teacherName] = new HashSet<string>();
            }

            // Дни недели
            string[] days = { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };

            // Шаг 7: Распределяем занятия
            foreach (string day in days)
            {
                for (int slotIndex = 0; slotIndex < _timeSlots.Count; slotIndex++)
                {
                    int slotNumber = slotIndex + 1;
                    string slotKey = $"{day}_{slotNumber}";

                    Discipline foundLesson = null;

                    foreach (Discipline lesson in allLessons)
                    {
                        string teacherName = lesson.Teacher;
                        Teacher teacher = _teachers[teacherName];

                        // Шаг 7.1.3: Проверяем доступность
                        bool isAvailable = IsTeacherAvailable(teacher, day, slotNumber);

                        // Шаг 7.1.4: Проверяем занятость
                        bool isBusy = teacherBusy[teacherName].Contains(slotKey);

                        if (isAvailable && !isBusy)
                        {
                            foundLesson = lesson;
                            break;
                        }
                    }

                    if (foundLesson != null)
                    {
                        // Шаг 7.1.5: Добавляем в расписание
                        schedule[slotKey] = $"{foundLesson.Name} ({foundLesson.Teacher})";

                        // Шаг 7.1.6: Отмечаем занятым
                        teacherBusy[foundLesson.Teacher].Add(slotKey);

                        // Шаг 7.1.7: Удаляем из списка
                        allLessons.Remove(foundLesson);
                    }
                    else
                    {
                        schedule[slotKey] = "";
                    }
                }
            }

            // Шаг 8: Проверяем, все ли распределили
            if (allLessons.Count > 0)
            {
                MessageBox.Show($"Не хватило слотов! Не распределено {allLessons.Count} занятий.",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Шаг 9: Преобразуем в таблицу
            return ConvertToDataTable(schedule, days);
        }

        // Вспомогательный метод проверки доступности преподавателя
        private bool IsTeacherAvailable(Teacher teacher, string day, int slotNumber)
        {
            // Если для дня нет записи - доступен весь день
            if (!teacher.Availability.ContainsKey(day))
            {
                return true;
            }

            // Если список пуст - доступен весь день
            if (teacher.Availability[day].Count == 0)
            {
                return true;
            }

            // Проверяем, есть ли слот в списке
            return teacher.Availability[day].Contains(slotNumber);
        }

        // Преобразование в DataTable
        private DataTable ConvertToDataTable(Dictionary<string, string> schedule, string[] days)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Время", typeof(string));

            foreach (string day in days)
            {
                dt.Columns.Add(day, typeof(string));
            }

            for (int i = 0; i < _timeSlots.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["Время"] = _timeSlots[i].ToString();

                foreach (string day in days)
                {
                    string key = $"{day}_{i + 1}";
                    row[day] = schedule.ContainsKey(key) ? schedule[key] : "";
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        #endregion
    }
}
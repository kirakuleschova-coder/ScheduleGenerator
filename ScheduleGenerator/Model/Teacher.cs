using System.Collections.Generic;
namespace ScheduleGenerator
{
    public class Teacher
    {
        public string FullName { get; set; }
        // Словарь: Ключ - день недели (ПН, ВТ...), Значение - список доступных слотов
        public Dictionary<string, List<int>> Availability { get; set; }

        public Teacher()
        {
            Availability = new Dictionary<string, List<int>>();
        }
    }
}
using System.Collections.Generic;

namespace ScheduleGenerator
{
    public class Teacher
    {
        public string FullName { get; set; }
        public Dictionary<string, List<int>> Availability { get; set; }

        public Teacher()
        {
            FullName = "";
            Availability = new Dictionary<string, List<int>>();
        }

        /// <summary>
        /// Добавить доступность преподавателя в определённый день и слот
        /// </summary>
        public void AddAvailability(string day, int slotNumber)
        {
            // Если для этого дня ещё нет списка - создаём его
            if (!Availability.ContainsKey(day))
            {
                Availability[day] = new List<int>();
            }

            // Добавляем номер слота, если его ещё нет
            if (!Availability[day].Contains(slotNumber))
            {
                Availability[day].Add(slotNumber);
            }
        }
    }
}
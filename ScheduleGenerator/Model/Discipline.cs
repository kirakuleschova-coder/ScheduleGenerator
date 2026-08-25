namespace ScheduleGenerator
{
    public class Discipline
    {
        public string Name { get; set; }
        public int HoursPerWeek { get; set; }
        public string Teacher { get; set; } // ФИО преподавателя
        public int Course { get; set; } // Добавила курс, чтобы связывать с группой при загрузке
    }
}
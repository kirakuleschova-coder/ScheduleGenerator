using System;

namespace ScheduleGenerator
{
    public class TimeSlot
    {

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakDuration { get; set; }


        // Для удобства отображения в таблице
        public override string ToString()
        {
            return $"{StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}";
        }
    }
}
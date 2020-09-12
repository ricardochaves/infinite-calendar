using System;

namespace InfiniteCalendar.Models.DTOs
{
    public class HolidayDto
    {
        public HolidayDto(int? id, DateTime dateTime, string name, int? limitYear)
        {
            Id = id;
            DateTime = dateTime;
            Name = name;
            LimitYear = limitYear;
        }
        public int? Id { get; private set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public int? LimitYear { get; set; }
    }
}

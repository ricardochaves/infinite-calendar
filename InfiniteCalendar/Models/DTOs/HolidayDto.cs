using System;

namespace InfiniteCalendar.Models.DTOs
{
    public class HolidayDto
    {
        public HolidayDto(int? id, DateTime dateTime, string name)
        {
            Id = id;
            DateTime = dateTime;
            Name = name;
        }
        public int? Id { get; private set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
    }
}

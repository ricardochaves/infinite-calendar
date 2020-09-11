using System;

namespace InfiniteCalendar.Models
{
    public class Day
    {
        public Day(int day, bool isHoliday, Holiday? holiday, DateTime dateTime)
        {
            this.day = day;
            this.isHoliday = isHoliday;
            Holiday = holiday;
            DateTime = dateTime;
        }

        public int day { get; }
        public bool isHoliday { get;  }
        public Holiday? Holiday { get;  }
        public DateTime DateTime { get;  }
    }
}

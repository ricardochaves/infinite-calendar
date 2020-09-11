using System.Collections.Generic;

namespace InfiniteCalendar.Models
{
    public class Calendar
    {
        public Calendar(int year, List<Month> months)
        {
            this.year = year;
            this.months = months;
        }

        public int year { get;  }
        public List<Month> months { get;  }
        
        
    }
}

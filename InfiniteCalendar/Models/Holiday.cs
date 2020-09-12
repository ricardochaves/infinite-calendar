using System;

using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace InfiniteCalendar.Models
{
    public class Holiday
    {
        public Holiday(int? id, DateTime dateTime, string name, int? limitYear)
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

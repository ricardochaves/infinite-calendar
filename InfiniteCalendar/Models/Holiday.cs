using System;

using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace InfiniteCalendar.Models
{
    public class Holiday
    {
        public Holiday(int? id, DateTime dateTime, string name)
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

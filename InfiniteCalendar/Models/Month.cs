using System.Collections.Generic;

namespace InfiniteCalendar.Models
{
    public class Month
    {
        public Month(string name, int number, string slugName, List<Day> days)
        {
            Name = name;
            this.number = number;
            this.slugName = slugName;
            this.days = days;
        }

        public string Name { get; }
        public int number { get; }
        public string slugName { get; }
        public List<Day> days { get; }
    }
}

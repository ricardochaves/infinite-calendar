using Microsoft.EntityFrameworkCore;

namespace InfiniteCalendar.Models
{
    public class InfiniteCalendarContext : DbContext
    {
        public InfiniteCalendarContext(DbContextOptions<InfiniteCalendarContext> options) : base(options)
        {
        }

        public DbSet<Holiday> Holidays => Set<Holiday>();
    }
}

namespace InfiniteCalendar.Parameters
{
    public class PaginationParameters
    {
        private const int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public int itemsToSkip
        {
            get { return (pageNumber - 1) * pageSize; }
        }
    }
}

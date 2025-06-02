namespace FridayAssignments.Models
{
    public class Parameters
    {
        //pageSize  represents the number of items or records to display on each page.
        //pageNumber represents the number of page, means the size splitted for each page

        const int maxPageSize = 50;
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
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
    }
}

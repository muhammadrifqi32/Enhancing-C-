namespace FridayAssignments.Models
{
    public class PagedData<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IQueryable<T> Data { get; set; }
    }
}

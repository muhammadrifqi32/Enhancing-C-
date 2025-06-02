namespace FridayAssignments.Models
{
    public class JqueryDatatableParam
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public DataTableSearch? Search { get; set; } // Make 'search' property optional
        public DataTableOrder? Order { get; set; }
    }
}

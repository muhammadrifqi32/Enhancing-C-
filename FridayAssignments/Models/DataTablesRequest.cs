using System.Data;

namespace FridayAssignments.Models
{
    public class DataTablesRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<DataTablesColumn> Columns { get; set; }
        public DataTableSearch Search { get; set; }
    }
}

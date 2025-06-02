using Azure;
using Azure.Core;
using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext myContext;

        public DepartmentRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(string Dept_Id)
        {
            var findData = myContext.Departments.Find(Dept_Id);
            if (findData != null)
            {
                myContext.Departments.Remove(findData);
            }
            var save = myContext.SaveChanges();
            return save;
        }

        public IEnumerable<Department> Get()
        {
            return myContext.Departments.ToList();
        }

        //public DataTablesResponse GetData(JqueryDatatableParam param)
        //{
        //    // Query your database based on the param parameters
        //    var data = myContext.Departments.AsQueryable(); // Start with all departments

        //    DataTablesResponse response = new DataTablesResponse();

        //    if (data.Count() != 0)
        //    {
        //        // Apply search filter if provided
        //        if (!string.IsNullOrEmpty(param.Search?.Value))
        //        {
        //            var searchTerm = param.Search.Value.ToLower();
        //            data = data.Where(e =>
        //                e.Dept_Id.ToLower().Contains(searchTerm) || // Ganti dengan kolom yang ingin dicari 
        //                e.Name.ToLower().Contains(searchTerm)
        //            );
        //        }

        //        // Apply sorting
        //        var sortColumnIndex = param.Order.column;
        //        var sortDirection = param.Order.dir;
        //        if (sortColumnIndex == 1)
        //        {
        //            data = sortDirection == "asc" ? data.OrderBy(d => d.Dept_Id) : data.OrderByDescending(d => d.Dept_Id);
        //        }
        //        else
        //        {
        //            data = sortDirection == "asc" ? data.OrderBy(d => d.Dept_Id) : data.OrderByDescending(d => d.Dept_Id);
        //        }

        //        // Apply paging
        //        var result = data.Skip(param.start).Take(param.length).ToList();

        //        // Return the response
        //        response.Draw = param.draw;
        //        response.RecordsTotal = myContext.Departments.Count();
        //        response.RecordsFiltered = data.Count();
        //        response.Data = result;
        //    }
        //    else
        //    {
        //        // Handle the case when data is empty
        //        response.Draw = param.draw;
        //        response.RecordsTotal = 0;
        //        response.RecordsFiltered = 0;
        //        response.Data = new List<Department>();
        //    }

        //    return response;
        //}

        public Department Get(string Dept_Id)
        {
            return myContext.Departments.Find(Dept_Id);
        }

        //public DataTablesResponse<Department> Get(DataTableRequest dataTableRequest)
        //{
        //    var query = myContext.Departments.AsQueryable();

        //    if (!string.IsNullOrEmpty(dataTableRequest.Search))
        //    {
        //        query = query.Where(d => d.Name.Contains(parameters.Search)); // Adjust filtering logic
        //    }

        //    if (!string.IsNullOrEmpty(request.Search?.Value))
        //    {
        //        var searchTerm = request.Search.Value.ToLower();
        //        query = query.Where(e =>
        //            e.Fullname.ToLower().Contains(searchTerm) || // Ganti dengan kolom yang ingin dicari 
        //            e.Position.ToLower().Contains(searchTerm)
        //        );
        //    }

        //    var recordsTotal = myContext.Departments.Count();
        //    var recordsFiltered = query.Count();

        //    var pagedData = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

        //    return new DataTablesResponse<Department>
        //    {
        //        RecordsTotal = recordsTotal,
        //        RecordsFiltered = recordsFiltered,
        //        Data = pagedData
        //    };
        //}


        public int Insert(Department department)
        {
            string nextDepartmentId = GeneratedDepartmentID();
            department.Dept_Id = nextDepartmentId;
            myContext.Departments.Add(department);
            var save = myContext.SaveChanges();
            return save;
        }

        public int Update(Department department)
        {
            myContext.Entry(department).State = EntityState.Modified;
            var save = myContext.SaveChanges();
            return save;
        }
        private string GeneratedDepartmentID()
        {
            int lastDepartmentNumber = myContext.Departments.Count();
            string nextDepartmentId = $"D{lastDepartmentNumber + 1:D3}";
            return nextDepartmentId;
        }
    }
}

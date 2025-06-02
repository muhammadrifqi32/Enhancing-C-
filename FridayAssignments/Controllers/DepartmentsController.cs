using Azure.Core;
using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository departmentRepository;
        private readonly MyContext _myContext;

        public DepartmentsController(DepartmentRepository departmentRepository, MyContext myContext)
        {
            this.departmentRepository = departmentRepository;
            this._myContext = myContext;
        }

        [HttpGet]
        public virtual ActionResult Get()
        {
            var get = departmentRepository.Get();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = get.Count() + " Data Ditemukan", Data = get });
            }
        }

        [HttpPost("Paging")]
        public virtual ActionResult GetData([FromBody] JqueryDatatableParam param)
        {
            // Query your database based on the param parameters
            var data = _myContext.Departments.AsQueryable(); // Start with all departments


            if (data.Count() != 0)
            {
                // Apply search filter if provided
                if (!string.IsNullOrEmpty(param.Search?.Value))
                {
                    var searchTerm = param.Search.Value.ToLower();
                    data = data.Where(e =>
                        e.Dept_Id.ToLower().Contains(searchTerm) || // Ganti dengan kolom yang ingin dicari 
                        e.Name.ToLower().Contains(searchTerm)
                    );
                }

                // Apply sorting
                var sortColumnIndex = param.Order.column;
                var sortDirection = param.Order.dir;
                if (sortColumnIndex == 1)
                {
                    data = sortDirection == "asc" ? data.OrderBy(d => d.Dept_Id) : data.OrderByDescending(d => d.Dept_Id);
                }
                else
                {
                    data = sortDirection == "asc" ? data.OrderBy(d => d.Dept_Id) : data.OrderByDescending(d => d.Dept_Id);
                }

                // Apply paging
                var result = data.Skip(param.start).Take(param.length).ToList();

                // Return the response
                var responseData = new
                {
                    draw = param.draw,
                    recordsTotal = _myContext.Departments.Count(),
                    recordsFiltered = data.Count(),
                    data = result
                };

                //return StatusCode(200, new { status = HttpStatusCode.OK, message = data.Count() + " Data Ditemukan", Data = responseData });
                return Ok(responseData);
            }
            //var getData = departmentRepository.GetData(param);
            //if(getData != null)
            //{
            //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Ditemukan", Data = getData });
            //}
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = data });
            }
        }

        //public virtual ActionResult GetPaging([FromQuery] DataTableRequest dataTableRequest)
        //{
        //    var pagedData = departmentRepository.Get(dataTableRequest);

        //    if (pagedData.Data.Any())
        //    {
        //        return StatusCode(200, new
        //        {
        //            status = HttpStatusCode.OK,
        //            message = pagedData.RecordsFiltered + " Data Ditemukan",
        //            Data = pagedData.Data,
        //            recordsTotal = pagedData.RecordsTotal,
        //            recordsFiltered = pagedData.RecordsFiltered
        //        });
        //    }
        //    else
        //    {
        //        return StatusCode(404, new
        //        {
        //            status = HttpStatusCode.NotFound,
        //            message = pagedData.RecordsFiltered + " Data Ditemukan",
        //            Data = pagedData.Data,
        //            recordsTotal = pagedData.RecordsTotal,
        //            recordsFiltered = pagedData.RecordsFiltered
        //        });
        //    }
        //}

        [HttpGet("{key}")]
        public virtual ActionResult Get(string key)
        {
            var get = departmentRepository.Get(key);
            if (get != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }
        }

        [HttpPost]
        public virtual ActionResult Insert(Department department)
        {
            var insert = departmentRepository.Insert(department);
            if (insert >= 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Dimasukkan",
                        Data = insert
                    });
            }
            else
            {
                return StatusCode(500,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Gagal Memasukkan Data",
                        Data = insert
                    });
            }
        }

        [HttpPut]
        public virtual ActionResult Update(Department department)
        {
            var insert = departmentRepository.Update(department);
            if (insert >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diperbaharui", Data = insert });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Memperbaharui Data", Data = insert });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {
            var delete = departmentRepository.Delete(key);
            if (delete >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dihapus", Data = delete });
            }
            else if (delete == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data dengan Id " + key + "Tidak Ditemukan", Data = delete });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Terjadi Kesalahan", Data = delete });
            }
        }
    }
}

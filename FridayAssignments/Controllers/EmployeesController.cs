using FridayAssignments.Models;
using FridayAssignments.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public virtual ActionResult Get()
        {
            var get = employeeRepository.Get();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }

        [HttpGet("{key}")]
        public virtual ActionResult Get(string key)
        {
            var get = employeeRepository.Get(key);
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
        public virtual ActionResult Insert(Employee employee)
        {
            var insert = employeeRepository.Insert(employee);
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
        public virtual ActionResult Update(Employee employee)
        {
            var insert = employeeRepository.Update(employee);
            if (insert >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diperbaharui", Data = insert });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Memperbaharui Data", Data = insert });
            }
        }

        [HttpPatch]
        public ActionResult Delete(string NIK)
        {
            var delete = employeeRepository.Delete(NIK);
            if (delete >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dihapus", Data = delete });
            }
            else if (delete == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data dengan Tidak Ditemukan", Data = delete });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Terjadi Kesalahan", Data = delete });
            }
        }
        [HttpGet("GetActiveEmployee")]
        public virtual ActionResult GetActiveEmployee()
        {
            var get = employeeRepository.GetActiveEmployee();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan"});
            }
        }
        [HttpGet("GetDeactiveEmployee")]
        public virtual ActionResult GetDeactiveEmployee()
        {
            var get = employeeRepository.GetDeactiveEmployee();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }

        [HttpGet("GetActiveEmployeePerDepartment")]
        public virtual ActionResult GetActiveEmployeePerDepartment(string DeptId)
        {
            var get = employeeRepository.GetActiveEmployeePerDepartment(DeptId);
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }
        [HttpGet("GetDeactiveEmployeePerDepartment")]
        public virtual ActionResult GetDeactiveEmployeePerDepartment(string DeptId)
        {
            var get = employeeRepository.GetDeactiveEmployeePerDepartment(DeptId);
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }

        [HttpGet("GetTotalActivePerDepartment")]
        public virtual ActionResult GetTotalActivePerDepartment()
        {
            var get = employeeRepository.GetTotalActivePerDepartment();
            if (get != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get});
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }

        [HttpGet("GetTotalDeactivePerDepartment")]
        public virtual ActionResult GetTotalDeactivePerDepartment()
        {
            var get = employeeRepository.GetTotalDeactivePerDepartment();
            if (get != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get});
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }

        //[EnableCors]
        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            return Ok("TestCors");
        }
    }
}

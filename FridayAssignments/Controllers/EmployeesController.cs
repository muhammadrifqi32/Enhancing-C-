using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly MyContext _context;

        public EmployeesController(EmployeeRepository employeeRepository, MyContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        private IActionResult ApiResponse(HttpStatusCode status, string message, object? data = null)
        {
            return StatusCode((int)status, new { status, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var get = await _employeeRepository.GetAsync();
            if (get.Any())
                return ApiResponse(HttpStatusCode.OK, $"{get.Count()} Data Ditemukan", get);

            return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var get = await _employeeRepository.GetAsync(key);
            if (get != null)
                return ApiResponse(HttpStatusCode.OK, "Data Ditemukan", get);

            return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Employee employee)
        {
            // VALIDASI Dept_Id terlebih dahulu
            var deptExists = await _context.Departments.AnyAsync(d => d.Dept_Id == employee.Dept_Id);
            if (!deptExists)
                return ApiResponse(HttpStatusCode.BadRequest, "Department tidak ditemukan");

            var result = await _employeeRepository.InsertAsync(employee);
            if (result >= 1)
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Dimasukkan", result);

            return ApiResponse(HttpStatusCode.InternalServerError, "Gagal Memasukkan Data");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Employee employee)
        {
            var result = await _employeeRepository.UpdateAsync(employee);
            if (result >= 1)
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Diperbaharui", result);

            return ApiResponse(HttpStatusCode.InternalServerError, "Gagal Memperbaharui Data");
        }

        [HttpPatch]
        public async Task<IActionResult> Delete(string NIK)
        {
            var result = await _employeeRepository.DeleteAsync(NIK);
            if (result >= 1)
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Dihapus", result);
            else if (result == 0)
                return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");

            return ApiResponse(HttpStatusCode.InternalServerError, "Terjadi Kesalahan");
        }
    }
}

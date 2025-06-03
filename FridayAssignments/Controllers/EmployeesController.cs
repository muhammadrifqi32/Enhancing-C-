using AutoMapper;
using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Models.DTOs;
using FridayAssignments.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, MyContext context, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
        }

        private IActionResult ApiResponse(HttpStatusCode status, string message, object? data = null)
        {
            return StatusCode((int)status, new { status, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeRepository.GetAsync();
            if (!employees.Any())
                return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");

            var dto = _mapper.Map<List<EmployeeDto>>(employees);
            return ApiResponse(HttpStatusCode.OK, $"{dto.Count} Data Ditemukan", dto);
        }

        [HttpGet("{nik}")]
        public async Task<IActionResult> Get(string nik)
        {
            var employee = await _employeeRepository.GetAsync(nik);
            if (employee == null)
                return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");

            var dto = _mapper.Map<EmployeeDto>(employee);
            return ApiResponse(HttpStatusCode.OK, "Data Ditemukan", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] EmployeePostDto input)
        {
            var deptExists = await _context.Departments.AnyAsync(d => d.Dept_Id == input.Dept_Id);
            if (!deptExists)
                return ApiResponse(HttpStatusCode.BadRequest, "Department tidak ditemukan");

            var employee = _mapper.Map<Employee>(input);
            var result = await _employeeRepository.InsertAsync(employee);
            if (result >= 1)
            {
                var dto = _mapper.Map<EmployeeDto>(employee);
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Dimasukkan", dto);
            }

            return ApiResponse(HttpStatusCode.InternalServerError, "Gagal Memasukkan Data");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeePutDto input)
        {
            var deptExists = await _context.Departments.AnyAsync(d => d.Dept_Id == input.Dept_Id);
            if (!deptExists)
                return ApiResponse(HttpStatusCode.BadRequest, "Department tidak ditemukan");

            var employee = _mapper.Map<Employee>(input);
            var result = await _employeeRepository.UpdateAsync(employee);
            if (result >= 1)
            {
                var dto = _mapper.Map<EmployeeDto>(employee);
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Diperbaharui", dto);
            }

            return ApiResponse(HttpStatusCode.InternalServerError, "Gagal Memperbaharui Data");
        }

        [HttpPatch("{nik}")]
        public async Task<IActionResult> Delete(string nik)
        {
            var result = await _employeeRepository.DeleteAsync(nik);
            if (result >= 1)
                return ApiResponse(HttpStatusCode.OK, "Data Berhasil Dihapus", result);
            else if (result == 0)
                return ApiResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");

            return ApiResponse(HttpStatusCode.InternalServerError, "Terjadi Kesalahan");
        }
    }
}

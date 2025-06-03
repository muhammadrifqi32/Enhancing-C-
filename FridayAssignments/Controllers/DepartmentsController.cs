using AutoMapper;
using FridayAssignments.Models;
using FridayAssignments.Models.DTOs.FridayAssignments.DTOs;
using FridayAssignments.Models.Dump;
using FridayAssignments.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        private IActionResult ApiResponse(HttpStatusCode status, string message, object? data = null)
        {
            return StatusCode((int)status, new { status, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _departmentRepository.GetAsync();
            var dto = _mapper.Map<List<DepartmentDTO>>(result);
            return ApiResponse(HttpStatusCode.OK, $"{dto.Count} data ditemukan", dto);
        }

        [HttpPost("Paging")]
        public async Task<IActionResult> GetData([FromBody] JqueryDatatableParam param)
        {
            var result = await _departmentRepository.GetPagedAsync(param);
            var dto = _mapper.Map<List<DepartmentDTO>>(result.Data);

            var response = new
            {
                draw = param.draw,
                recordsTotal = result.RecordsTotal,
                recordsFiltered = result.RecordsFiltered,
                data = dto
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _departmentRepository.GetAsync(id);
            if (result == null)
                return ApiResponse(HttpStatusCode.NotFound, $"Department dengan ID {id} tidak ditemukan");

            var dto = _mapper.Map<DepartmentDTO>(result);
            return ApiResponse(HttpStatusCode.OK, "Data ditemukan", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] DepartmentCreateDTO input)
        {
            var department = _mapper.Map<Department>(input);
            var save = await _departmentRepository.InsertAsync(department);

            if (!save)
                return ApiResponse(HttpStatusCode.InternalServerError, "Gagal memasukkan data");

            var dto = _mapper.Map<DepartmentDTO>(department);
            return ApiResponse(HttpStatusCode.OK, "Data berhasil dimasukkan", dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DepartmentDTO input)
        {
            var department = _mapper.Map<Department>(input);
            var save = await _departmentRepository.UpdateAsync(department);

            if (!save)
                return ApiResponse(HttpStatusCode.NotFound, $"Department dengan ID {input.Dept_Id} tidak ditemukan atau gagal update");

            var dto = _mapper.Map<DepartmentDTO>(department);
            return ApiResponse(HttpStatusCode.OK, "Data berhasil diperbarui", dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _departmentRepository.DeleteAsync(id);
            if (!isDeleted)
                return ApiResponse(HttpStatusCode.NotFound, $"Department dengan ID {id} tidak ditemukan");

            return ApiResponse(HttpStatusCode.OK, "Data berhasil dihapus");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;
using DATN.Common;
using DATN.Common.Resources;
using DATN.DAL.Interfaces;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BasesController<Employee>
    {
        IEmployeeService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm EmployeeBL</param>
        /// <param name="repository">Tham số được tiêm EmployeeRepository</param>
        /// Created By: NDDung (02/09/2022)
        public EmployeeController(IEmployeeService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Phương thức get bởi filter
        /// </summary>
        /// <param name="pageSize">Số bản ghi 1 trang</param>
        /// <param name="pageNumber">Trang số mấy</param>
        /// <param name="employeeFilter">Thông tin tìm kiếm theo</param>
        /// <returns>
        ///     Mã 200 và data khi tìm thành công    
        /// </returns>
        /// Created By: NDDung (02/08/2022) 
        [HttpGet("filter")]
        public IActionResult GetByFilter(int pageSize, int pageNumber, string? employeeFilter)
        {
            try
            {
                var data = _service.FilterService(pageSize, pageNumber, employeeFilter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Phương thức get mã nhân viên mới
        /// </summary>
        /// <returns>
        ///     Mã 200 và data khi lấy mã mới thành công
        /// </returns>
        /// Created By: NDDung (02/08/2022) 
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var data = _service.GetNewCodeService();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        ///     Phương xóa nhiều nhân viên
        /// </summary>
        /// <returns>
        ///     Mã 200  
        /// </returns>
        /// Created By: NDDung (16/08/2022) 
        [HttpDelete("DeleteMultiEmployee")]
        public IActionResult Delete(List<Guid> lstId)
        {
            try
            {
                var data = _service.DeleteEmployeesWereSelectedService(lstId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí exception 
        /// </summary>
        /// <param name="Exception">Lỗi</param>
        /// <returns>
        ///     Mã 500 là lỗi do serve
        ///     Mã 400 là do dữ liệu đầu vào không hợp lệ
        /// </returns>
        /// Created By: NDDung (02/08/2022)     
        private IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = ResourceVN.Error_Exception,
                errorMsg = ex.Data["validateError"]
            };

            if (ex is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);
        }

    }
}

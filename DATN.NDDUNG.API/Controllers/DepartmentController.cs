using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BasesController<Department>
    {
        IDepartmentService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm DepartmentBL</param>
        /// <param name="repository">Tham số được tiêm DepartmentRepository</param>
        /// Created By: NDDung (02/09/2022)
        public DepartmentController(IDepartmentService service) : base(service)
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
        /// Created By: NDDung (06/05/2023) 
        [HttpGet("filter")]
        public IActionResult GetByFilter(int pageSize, int pageNumber, string? textFilter)
        {
            try
            {
                var data = _service.FilterService(pageSize, pageNumber, textFilter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}

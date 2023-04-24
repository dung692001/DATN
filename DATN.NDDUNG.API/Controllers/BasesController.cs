using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common;
using DATN.Common.Resources;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<MISAEntity> : ControllerBase
    {
        IBaseService<MISAEntity> _service;
        /// <summary>
        /// Hàm khởi tạo base controller
        /// </summary>
        /// <param name="service">Tham số được tiêm BaseService</param>
        /// Created By: NDDung (02/09/2022)
        public BasesController(IBaseService<MISAEntity> service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (10/09/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _service.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi </param>
        /// <returns>Đối tượng có id tương ứng</returns>
        /// Created By: NDDung (10/09/2022)
        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id)
        {
            try
            {
                var data = _service.GetByIdService(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var res = _service.InsertService(entity);
               return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex, 150);
            }
        }


        /// <summary>
        /// Xử lí exception 
        /// </summary>
        /// <param name="Exception">Lỗi</param>
        /// <returns>
        ///     Code 500 là exception
        /// </returns>
        /// Created By: NDDung (02/09/2022)     
        protected IActionResult HandleException(Exception ex, int? subCode=200)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = ResourceVN.Error_Exception,
                errorMsg = ex.Data["validateError"],
                SubCode=subCode
            };

            if (ex is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);
        }
    }
}

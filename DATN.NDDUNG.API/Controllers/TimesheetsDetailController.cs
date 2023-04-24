using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimesheetsDetailController : BasesController<TimesheetsDetail>
    {
        ITimesheetsDetailService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm TimesheetsDetailBL</param>
        /// <param name="repository">Tham số được tiêm TimesheetsDetailRepository</param>
        /// Created By: NDDung (02/09/2022)
        public TimesheetsDetailController(ITimesheetsDetailService service) : base(service)
        {
            _service = service;
        }

    }
}

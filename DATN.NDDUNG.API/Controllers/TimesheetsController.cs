using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimesheetsController : BasesController<Timesheets>
    {
        ITimesheetsService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm TimesheetsBL</param>
        /// <param name="repository">Tham số được tiêm TimesheetsRepository</param>
        /// Created By: NDDung (02/09/2022)
        public TimesheetsController(ITimesheetsService service) : base(service)
        {
            _service = service;
        }

    }
}

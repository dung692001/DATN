using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BasesController<Positions>
    {
        IPositionsService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm PositionsBL</param>
        /// <param name="repository">Tham số được tiêm PositionsRepository</param>
        /// Created By: NDDung (02/09/2022)
        public PositionsController(IPositionsService service) : base(service)
        {
            _service = service;
        }

    }
}

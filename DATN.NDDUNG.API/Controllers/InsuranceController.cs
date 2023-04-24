using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InsuranceController : BasesController<Insurance>
    {
        IInsuranceService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm InsuranceBL</param>
        /// <param name="repository">Tham số được tiêm InsuranceRepository</param>
        /// Created By: NDDung (02/09/2022)
        public InsuranceController(IInsuranceService service) : base(service)
        {
            _service = service;
        }

    }
}

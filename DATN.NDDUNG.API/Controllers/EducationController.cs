using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EducationController : BasesController<Education>
    {
        IEducationService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm EducationBL</param>
        /// <param name="repository">Tham số được tiêm EducationRepository</param>
        /// Created By: NDDung (02/09/2022)
        public EducationController(IEducationService service) : base(service)
        {
            _service = service;
        }
    }
}

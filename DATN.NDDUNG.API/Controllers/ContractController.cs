using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.BLL.Interfaces;
using DATN.Common.Entities;

namespace DATN.NDDUNG.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContractController : BasesController<Contract>
    {
        IContractService _service;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="service">Tham số được tiêm ContractBL</param>
        /// <param name="repository">Tham số được tiêm ContractRepository</param>
        /// Created By: NDDung (02/09/2022)
        public ContractController(IContractService service) : base(service)
        {
            _service = service;
        }
    }
}

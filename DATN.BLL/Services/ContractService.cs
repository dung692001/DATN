using DATN.BLL.Interfaces;
using DATN.BLL.Services;
using DATN.Common.Entities;
using DATN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Services
{
    public class ContractService : BaseService<Contract>, IContractService
    {
        IContractRepository contractRepository;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="repository">Tham số được tiêm vào</param>
        /// Created By: NDDung (13/09/2022)
        public ContractService(IContractRepository repository) : base(repository)
        {
            contractRepository = repository;
        }
    }
}

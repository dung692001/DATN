using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Bảng lưu thông tin hợp đồng
    /// </summary>
    public class Contract : BaseEntity
    {
        public Guid ContractId { get; set; }

        public Guid EmployeeId { get; set; }

        public string? ContractCode { get; set; }
        public string? ContractName { get; set; }
        public DateTime? SignDate { get; set; }
        public string? SignNumber { get; set; }
        public int? Duration { get; set; }

        public decimal? Salary { get; set; }

        public decimal? SalaryInsurance { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model thông tin bảo hiểm
    /// </summary>
    public class Insurance : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid InsuranceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã bảo hiểm
        /// </summary>
        public string? InsuranceCode { get; set; }
        /// <summary>
        /// Số bảo hiểm
        /// </summary>
        public string? InsuranceNumber { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public DateTime? IssuedDate { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public string? IssuedPlace { get; set; }
        /// <summary>
        /// Nơi khám bệnh 
        /// </summary>
        public string? ClinicPlace { get; set; }
    }
}

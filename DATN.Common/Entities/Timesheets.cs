using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model lưu thông tin bảng công
    /// </summary>
    public class Timesheets : BaseEntity
    {
        /// <summary>
        /// Khóa chính bảng công
        /// </summary>
        public Guid TimesheetsId { get; set; }
        /// <summary>
        /// Khóa chính bảng người dùng
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã bảng công
        /// </summary>
        public string? TimesheetsCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? TimesheetsName { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Tổng ngày làm việc
        /// </summary>
        public int? TotalWorking { get; set; }
        /// <summary>
        /// Số ngày làm việc
        /// </summary>
        public int? WorkingDay { get; set; }
        /// <summary>
        /// Số ngày đi muộn
        /// </summary>
        public int? TotalDayLate { get; set; }
        /// <summary>
        /// Số phút đi muộn
        /// </summary>
        public int? TotalMinuteLate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model lưu thông tin chi tiết bảng công
    /// </summary>
    public class TimesheetsDetail : BaseEntity
    {
        /// <summary>
        /// Khóa chính chi tiết bảng công
        /// </summary>
        public Guid TimesheetsDetailId { get; set; }
        /// <summary>
        /// Khóa chính bảng công
        /// </summary>
        public Guid TimesheetsId { get; set; }
        /// <summary>
        /// Mã chi tiết bảng công
        /// </summary>
        public string? TimeSheetsDetailCode { get; set; }
        /// <summary>
        /// Ngày làm việc
        /// </summary>
        public DateTime? TimesheetsDate { get; set; }
        /// <summary>
        /// Thời gian vào
        /// </summary>
        public DateTime? StartTimeWorking { get; set; }
        /// <summary>
        /// Thời gian ra về
        /// </summary>
        public DateTime? EndTimeWorking { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckOutTime { get; set; }
        /// <summary>
        /// Thời gian bắt đầu nghỉ
        /// </summary>
        public DateTime? StartBreakTime { get; set; }
        /// <summary>
        /// Thời gian kết thúc nghỉ
        /// </summary>
        public DateTime? EndBreakTime { get; set; }
        /// <summary>
        /// Thứ
        /// </summary>
        public int? WeekDays { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Bảng lưu thông tin ca làm việc
    /// </summary>
    public class WorkingShift : BaseEntity
    {
        /// <summary>
        /// Khóa chính chi tiết bảng công
        /// </summary>
        public Guid WorkingShiftId { get; set; }
        /// <summary>
        /// Khóa chính chi tiết bảng công
        /// </summary>
        public Guid TimesheetsDetailId { get; set; }
        /// <summary>
        /// Mã chi tiết bảng công
        /// </summary>
        public string? WorkingShiftName { get; set; }
        /// <summary>
        /// Loại ca làm việc ( 0 ca hành chính, ca ngoài giờ hành chính) 
        /// </summary>
        public int? WorkingShiftType { get; set; }
        /// <summary>
        /// hệ số lương
        /// </summary>
        public int? Coefficient { get; set; }
    }
}

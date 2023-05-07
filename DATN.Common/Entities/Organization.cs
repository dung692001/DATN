using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model lưu thông ttin dự án
    /// </summary>
    public class Organization:BaseEntity
    {
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// Thông tin phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? DepartmentName { get; set; }
        public string? OrganizationCode { get; set; }

        public string? OrganizationName { get; set; }
    }
}

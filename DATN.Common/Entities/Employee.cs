using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model thông tin nhân viên
    /// </summary>
    public class Employee: BaseEntity
    {
        /// <summary>
        /// Khóa chính bảng người dùng
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Khóa chính bảng người thân
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// Thông tin phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        /// thông tin dự án
        /// </summary>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? OrganizationName { get; set; }
        /// <summary>
        /// Thông tin vị trí
        /// </summary>
        public Guid PositionsId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? PositionsName { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public string? EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }
        /// <summary>
        /// Giới tính 0 Nam 1 Nữ 2 Khác
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Ngày thắng năm sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string? IdentifyNumber { get; set; }
        /// <summary>
        /// Ngày cấp CCCD
        /// </summary>
        public DateTime? IdentifyIssuedDate { get; set; }
        /// <summary>
        /// Nơi cấp CCCD
        /// </summary>
        public string? IdentifyIssuePlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Địa chi hiện tại
        /// </summary>
        public string? CurrentAddress { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Địa chỉ email làm việc
        /// </summary>
        public string? OfficeEmail { get; set; }
        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public Guid? Avatar { get; set; }
        /// <summary>
        /// Ảnh chấm công
        /// </summary>
        public Guid? AvatarTimeKeeping { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public int? EthnicType { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int? MaritialStatus { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? BankCode { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankNumber { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? TaxCode { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? ContactName { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? ContactNumber { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? Password { get; set; }

        public int? Role { get; set; }


    }
}

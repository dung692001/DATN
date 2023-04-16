using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model lưu thông tin người thân
    /// </summary>
    public class Parent: BaseEntity
    {
        /// <summary>
        /// Khóa chính bảng người thân
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// Mã người thân
        /// </summary>
        public string? ParentCode { get; set; }
        /// <summary>
        /// Tên người thân
        /// </summary>
        public string? ParentName { get; set; }
        /// <summary>
        /// Số điện thoại người thân
        /// </summary>
        public string? ParentPhoneNumber { get; set; }
        /// <summary>
        /// Kiểu người thân 0 cha, 1 mẹ, 2 anh, 3 chị, 4 em
        /// </summary>
        public int? ParentType { get; set; }
    }
}

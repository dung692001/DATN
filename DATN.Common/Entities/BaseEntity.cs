using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Thời gian khởi tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi ai
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Sửa bởi ai
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}

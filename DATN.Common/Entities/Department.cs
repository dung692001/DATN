using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model thông tin phòng ban
    /// </summary>
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }

        public string? DepartmentName { get; set; }
    }
}

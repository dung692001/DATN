using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model lưu thông tin vị trí
    /// </summary>
    public class Positions: BaseEntity
    {
        public Guid PositionId { get; set; }

        public string? PositionCode { get; set; }

        public string? PositionName { get; set; }
    }
}

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
        public Guid PositionsId { get; set; }

        public string? PositionsCode { get; set; }

        public string? PositionsName { get; set; }
    }
}

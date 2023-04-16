using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    ///  Model bảng khen thưởng _ kỷ luật
    /// </summary>
    public class BonusDiscipline : BaseEntity
    {
        public Guid BonusDisciplineId { get; set; }

        public Guid EmployeeId { get; set; }

        public string? BonusDisciplineCode { get; set; }

        public int BonusDisciplineType { get; set; }

        public string? BonusDiciplineContent { get; set; }
         
        public DateTime? BonusDiciplineDate { get; set; }
    }
}

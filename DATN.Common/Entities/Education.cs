using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Entities
{
    /// <summary>
    /// Model thông tin trình độ giáo dục
    /// </summary>
    public class Education : BaseEntity
    {
        public Guid EducationId { get; set; }
        public string? EducationCode { get; set; }

        public string? EducationName { get; set; }
        public string? EducationPlace { get; set; }
    }
}

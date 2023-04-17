using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common.Exceptions
{
    public class ErrorService
    {
        /// <summary>
        /// Tin nhắn dành cho dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Tin nhắn dành cho user
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// data trả về
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Code dành cho user
        /// </summary>
        public int? SubCode { get; set; }
    }
}

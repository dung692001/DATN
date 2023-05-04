using DATN.BLL.Interfaces;
using DATN.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Interfaces
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Hàm phân trang tìm kiếm
        /// </summary>
        /// <param name="pageSize">Mã nhân viên</param>
        /// <param name="pageNumber">Mã nhân viên</param>
        /// <param name="employeeFilter">Mã nhân viên</param>
        /// <returns>
        ///     Thông tin mà employeeResponsitory.filterEmployee trả về
        /// </returns>
        /// Created By: NDDung (10/08/2022)
        public object FilterService(int pageSize, int pageNumber, string? employeeFilter);

        public int DeleteEmployeesWereSelectedService(List<Guid> lstId);

        /// <summary>
        /// Lấy toàn bộ mã nhân viên
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (11/08/2022)
        public string GetNewCodeService();

        public string GetToken(string username, string password);
    }
}

using DATN.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.DAL.Interfaces
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>
        /// Hàm phân trang tìm kiếm
        /// </summary>
        /// <param name="pageSize">Mã nhân viên</param>
        /// <param name="pageNumber">Mã nhân viên</param>
        /// <param name="employeeFilter">Mã nhân viên</param>
        /// <returns>
        ///     TotalPage: Tổng số trang 
        ///     TotalRecord: Tổng số bản ghi
        ///     CurrentPage: Vị trí của trang hiện tại
        ///     Data: Danh sách nhân viên phù hợp với
        /// </returns>
        /// Created By: NDDung (10/08/2022)
        public object FilterEmployee(int pageSize, int pageNumber, string? employeeFilter);

        /// <summary>
        /// Kiểm tra mã có bị trùng hay không
        /// </summary>
        /// <param name="id">Khóa chính của bảng nhân viên</param>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        ///     true Mã đã tồn tại;
        ///     false Mã chưa tồn tại
        /// </returns>
        /// Created By: NDDung (02/08/2022)  
        public bool CheckEmployeeCode(Guid? id, string? employeeCode);

        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: NDDung (11/08/2022)
        public IEnumerable<string> GetAllEmployeeCode();

        public int DeleteEmployeesWereSelected(List<Guid> lstId);

        /// <summary>
        /// Kiểm tra Email có đúng định dạng không
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     true email đúng định dạng
        ///     false email không đúng định dạng
        /// </returns>
        /// Created By: NDDung (02/08/2022)
        public bool CheckEmail(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Employee GetAccount(string username, string password);

    }
}

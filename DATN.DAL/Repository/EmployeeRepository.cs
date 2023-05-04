using Dapper;
using DATN.Common.Entities;
using DATN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.DAL.Repository
{
    public class EmployeeRepository:BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Phương thức phân trang tìm kiếm
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
        public Object FilterEmployee(int pageSize, int pageNumber, string? employeeFilter)
        {
            var storeName = "Proc_FilterEmployee";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("pageSize", pageSize);
            // thực hiện tính vị trí bắt đầu để truyền vào offset trong proc
            var pageNumberBegin = (pageNumber - 1) * pageSize;
            dynamicParam.Add("pageNumberBegin", pageNumberBegin);
            dynamicParam.Add("filterText", employeeFilter);
            // tìm kiếm tổng số bản ghi phù hợp với employeeFilter
            var sqlCommand = $"SELECT EmployeeId FROM Employee WHERE EmployeeCode LIKE '%{employeeFilter}%' OR EmployeeName LIKE '%{employeeFilter}%' ";
            var TotalRecord = _connection.Query(sqlCommand).Count();
            var TotalPage = Math.Ceiling((double)TotalRecord / pageSize);
            var rowsEffect = _connection.Query<Employee>(sql: storeName, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //var temp = _connection.QueryMultiple // Trả về 1 danh sách các bảng
            var res = new
            {
                TotalRecord = TotalRecord,
                TotalPage = TotalPage,
                Data = rowsEffect,

            };
            return res;
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: NDDung (11/08/2022)
        public IEnumerable<string> GetAllEmployeeCode()
        {
            // khai báo sql Command:
            var sqlCommand = "SELECT EmployeeCode FROM Employee";

            // Thực hiện lấy dữ liệu:
            var data = _connection.Query<string>(sqlCommand);
            return data;
        }

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
        public bool CheckEmployeeCode(Guid? id, string? employeeCode)
        {
            var sqlCheck = "SELECT EmployeeCode FROM Employee Where EmployeeCode = @EmployeeCode And EmployeeId != @id";
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);
            parameters.Add("@id", id.ToString());
            var res = _connection.QueryFirstOrDefault<string>(sqlCheck, param: parameters);
            if (res != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Phương thức phân trang tìm kiếm
        /// </summary>
        /// <param name="lstId">list mã nhân viên</param>
        /// <returns>
        ///     số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (10/08/2022)
        public int DeleteEmployeesWereSelected(List<Guid> lstId)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                var storeName = "Proc_DeleteEmployeeWereSelected";

                var values = "";
                foreach (var id in lstId)
                {
                    values += $"'{id}',";

                }
                values = values[..^1];
                var dynamicParam = new DynamicParameters();
                dynamicParam.Add("@arrayIdText", values);
                var rowsEffect = _connection.Execute(sql: storeName, param: dynamicParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                if (rowsEffect == 0)
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }
                return rowsEffect;
            }
        }

        public Employee GetAccount(string username, string password)
        {
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("$account_username", username);
            dynamicParam.Add("$accout_password", password);

            List<Employee> dataQuery = (List<Employee>)_connection.Query<Employee>(sql: "Proc_GetAccountIdRole", param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            Employee data = dataQuery.FirstOrDefault() ?? new Employee();
            return data;
        }

        /// <summary>
        /// Kiểm tra Email có đúng định dạng không
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     true email đúng định dạng
        ///     false email không đúng định dạng
        /// </returns>
        /// Created By: NDDung (02/08/2022)
        public bool CheckEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

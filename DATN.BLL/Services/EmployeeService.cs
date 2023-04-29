using DATN.BLL.Interfaces;
using DATN.BLL.Services;
using DATN.Common.Entities;
using DATN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Common.HandleMethod;
using DATN.Common.Resources;
using DATN.Common;

namespace DATN.BLL.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        IEmployeeRepository employeeRepository;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="repository">Tham số được tiêm vào</param>
        /// Created By: NDDung (13/09/2022)
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            employeeRepository = repository;
        }

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
        public object FilterService(int pageSize, int pageNumber, string? employeeFilter)
        {
            if (pageSize == 0)
            {
                pageSize = 100000;
            }
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            if (employeeFilter == null)
            {
                employeeFilter = "";
            }
            var res = employeeRepository.FilterEmployee(pageSize, pageNumber, employeeFilter);
            return res;

        }

        /// <summary>
        /// Lấy toàn bộ mã nhân viên
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (11/08/2022)
        public string GetNewCodeService()
        {
            var res = employeeRepository.GetAllEmployeeCode().ToList<string>();
            var temp = HandleFormat.SplitNumberFromString(res);
            string newCode = HandleFormat.FindMaxNumber(temp);

            newCode = String.Concat("NV-", newCode);
            return newCode;
        }

        /// <summary>
        /// Xóa những nhân viên đã được chọn
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (16/08/2022)
        public int DeleteEmployeesWereSelectedService(List<Guid> lstId)
        {
            var res = employeeRepository.DeleteEmployeesWereSelected(lstId);
            return res;
        }

        /// <summary>
        /// Phương thức Validate thông tin Employee
        /// </summary>
        /// <param name="id">id của employee(có thể null)</param>
        /// <param name="entity">thông tin của employee</param>
        /// <returns>
        ///     true: Không có sai sót trong thông tin của nhân viên
        ///     false: Có lỗi sai  
        /// </returns>
        protected override bool ValidateCustom(Guid? id, Employee entity)
        {
            var isExist = employeeRepository.CheckEmployeeCode(id, entity?.EmployeeCode);
            if (isExist == true)
            {
                isValidCustom = false;
                listMsgErrors.Add("EmployeeCodeIsExist", ResourceVN.ValiedateError_EmployeeCodeIsExist);
            }
            if (entity?.Email != null && entity.Email != "")
            {
                var isValidEmail = employeeRepository.CheckEmail(entity.Email);
                if (isValidEmail != true)
                {
                    isValidCustom = false;
                    listMsgErrors.Add("EmailNotValid", ResourceVN.ValidateError_EmailNotValid);
                }
            }

            return isValidCustom;
        }

        public override int InsertService(Employee entity)
        {
            // Xử lý nghiệp vụ
            var isValid = Validate(entity);
            isValidCustom = ValidateCustom(null, entity);
            if (isValid == true && isValidCustom == true)
            {
                var res = employeeRepository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAException(ResourceVN.Error_InputNotValid, listMsgErrors);
            }
        }
    }
}

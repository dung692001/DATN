using DATN.BLL.Interfaces;
using DATN.BLL.Services;
using DATN.Common.Entities;
using DATN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository departmentRepository;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="repository">Tham số được tiêm vào</param>
        /// Created By: NDDung (13/09/2022)
        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            departmentRepository = repository;
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
        public object FilterService(int pageSize, int pageNumber, string? textFilter)
        {
            if (pageSize == 0)
            {
                pageSize = 100000;
            }
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            if (textFilter == null)
            {
                textFilter = "";
            }
            var res = departmentRepository.FilterDepartment(pageSize, pageNumber, textFilter);
            return res;

        }
    }
}

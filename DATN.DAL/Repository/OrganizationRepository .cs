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
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        /// <summary>
        /// Phương thức phân trang tìm kiếm
        /// </summary>
        /// <param name="pageSize">Mã nhân viên</param>
        /// <param name="pageNumber">Mã nhân viên</param>
        /// <param name="textFilter">Mã nhân viên</param>
        /// <returns>
        ///     TotalPage: Tổng số trang 
        ///     TotalRecord: Tổng số bản ghi
        ///     CurrentPage: Vị trí của trang hiện tại
        ///     Data: Danh sách nhân viên phù hợp với
        /// </returns>
        /// Created By: NDDung (10/08/2022)
        public Object FilterOrganization(int pageSize, int pageNumber, string? textFilter)
        {
            var storeName = "Proc_FilterOrganization";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("pageSize", pageSize);
            // thực hiện tính vị trí bắt đầu để truyền vào offset trong proc
            var pageNumberBegin = (pageNumber - 1) * pageSize;
            dynamicParam.Add("pageNumberBegin", pageNumberBegin);
            dynamicParam.Add("filterText", textFilter);
            // tìm kiếm tổng số bản ghi phù hợp với textFilter
            var sqlCommand = $"SELECT OrganizationId FROM Organization WHERE OrganizationCode LIKE '%{textFilter}%' OR OrganizationName LIKE '%{textFilter}%' ";
            var TotalRecord = _connection.Query(sqlCommand).Count();
            var TotalPage = Math.Ceiling((double)TotalRecord / pageSize);
            var rowsEffect = _connection.Query<Organization>(sql: storeName, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //var temp = _connection.QueryMultiple // Trả về 1 danh sách các bảng
            var res = new
            {
                TotalRecord = TotalRecord,
                TotalPage = TotalPage,
                Data = rowsEffect,

            };
            return res;
        }
    }
}

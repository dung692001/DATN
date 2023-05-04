using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.DAL.Interfaces
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (17/04/2023)
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy toàn bộ dữ liệu theo id
        /// </summary>
        /// param name="id" id của bản ghi muốn lấy
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (13/09/2022)
        MISAEntity GetById(Guid id);

        /// <summary>
        /// Thêm 1 bản ghi vào bảng
        /// </summary>
        /// <param name="entity">Thông tin bản ghi muốn thêm</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (10/09/2022)
        int Insert(MISAEntity entity);

        /// <summary>
        /// Sửa 1 bản ghi 
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi</param>
        /// <param name="entity">Thông tin bản ghi muốn sửa</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (10/09/2022)
        int Update(Guid id, MISAEntity entity);

        /// <summary>
        /// Xóa 1 bản ghi tại bảng
        /// </summary>
        /// <param name="id">id bản ghi muốn xóa</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (08/08/2022)
        int DeleteById(Guid? id);
    }
}

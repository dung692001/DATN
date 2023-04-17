using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Interfaces
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (02/09/2022)
        public IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// param name="id" id của bản ghi muốn lấy
        /// <returns>Dữ liệu lấy được</returns>
        /// Created By: NDDung (13/09/2022)
        public MISAEntity GetByIdService(Guid id);

        /// <summary>
        /// Phương thức xử lý nghiệp vụ thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng được thêm</param>
        /// <returns>Phản hồi của phương thức Insert trong repository</returns>
        /// Created By: NDDung (07/09/2022)
        public int InsertService(MISAEntity entity);

        /// <summary>
        /// Phương thức xử lý nghiệp vụ sửa bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng được sửa</param>
        /// <returns>Phản hồi của phương thức Update trong repository</returns>
        /// Created By: NDDung (07/09/2022)
        public int UpdateService(Guid id, MISAEntity entity);


    }
}

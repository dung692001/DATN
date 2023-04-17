using DATN.Common;
using DATN.Common.Resources;
using DATN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Services
{
    public class BaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> repository;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="repository">Tham số được tiêm vào</param>
        /// Created By: NDDung (02/09/2022)
        public BaseService(IBaseRepository<MISAEntity> _repository)
        {
            repository = _repository;
        }
        protected bool isValidCustom = true;
        protected Dictionary<string, string> listMsgErrors = new Dictionary<string, string>();

        /// <summary>
        /// Lấy toàn bộ dữ liệu của 1 bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created By: NDDung (02/09/2022)
        public IEnumerable<MISAEntity> GetAll()
        {
            var res = repository.GetAll();
            return res;
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// param name="id" id của bản ghi muốn lấy
        /// <returns>Dữ liệu lấy được</returns>
        /// Created By: NDDung (13/09/2022)
        public MISAEntity GetByIdService(Guid id)
        {
            return repository.GetById(id);
        }


        /// <summary>
        /// Phương thức xử lý nghiệp vụ thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng được thêm</param>
        /// <returns>Phản hồi của phương thức Insert trong repository</returns>
        /// Created By: NDDung (07/09/2022)
        public virtual int InsertService(MISAEntity entity)
        {
            // Xử lý nghiệp vụ
            var isValid = Validate(entity);
            isValidCustom = ValidateCustom(null, entity);
            if (isValid == true && isValidCustom == true)
            {
                var res = repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAException(ResourceVN.Error_InputNotValid, listMsgErrors);
            }
        }

        /// <summary>
        /// Phương thức xử lý nghiệp vụ sửa bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng được thêm</param>
        /// <returns>Phản hồi của phương thức Update trong repository</returns>
        /// Created By: NDDung (07/09/2022)
        public virtual int UpdateService(Guid id, MISAEntity entity)
        {
            // Xử lý nghiệp vụ
            var isValid = Validate(entity);
            isValidCustom = ValidateCustom(id, entity);
            if (isValid == true && isValidCustom == true)
            {
                var res = repository.Update(id, entity);
                return res;
            }
            else
            {
                throw new MISAException(ResourceVN.Error_InputNotValid, listMsgErrors);
            }
        }

        /// <summary>
        /// Validate nhưng thông tin bắt buộc không được để trống
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>
        ///     true: Đã điền đầy đủ thông tin có attr required
        ///     false: chưa điền đử thông tin có attr required
        /// </returns>
        /// Created By: NDDung (09/08/2022)
        protected bool Validate(MISAEntity entity)
        {
            var isValid = true;
            // validate chung:
            // Kiểm tra các thông tin bắt buộc nhập:
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(entity);
                // xem property có Attr là MISARequire không
                var misaRequired = property.IsDefined(typeof(MISARequired), false);
                if (misaRequired == true && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    var arrayPropNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                    propName = (arrayPropNameDisplay as PropNameDisplay).PropName;
                    listMsgErrors.Add($"{propName}", $"Thông tin {propName} không được phép để trống");
                }
                var length = 0;
                var misaLength = property.IsDefined(typeof(MISALength), false);
                if (misaLength == true && (value != null && value.ToString() != String.Empty))
                {
                    var arrayMISALength = property.GetCustomAttributes(typeof(MISALength), false).FirstOrDefault();
                    length = (arrayMISALength as MISALength).LengthInput;
                    if (value.ToString().Length > length)
                    {
                        isValid = false;
                        var arrayPropNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                        propName = (arrayPropNameDisplay as PropNameDisplay).PropName;
                        listMsgErrors.Add($"{propName}", $"Độ dài tối đa là {length}");
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Phương thức validate custom
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <param name="entity">các thuộc tính của đối tượng</param>
        /// <returns></returns>
        /// Created By: NDDung (09/08/2022)
        public virtual bool ValidateCustom(Guid? id, MISAEntity entity)
        {
            return true;
        }
    }
}

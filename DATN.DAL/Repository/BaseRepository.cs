using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DATN.Common;
using DATN.DAL.Interfaces;
using MySqlConnector;

namespace DATN.DAL.Repository
{
    public class BaseRepository<MISAEntity> : IDisposable, IBaseRepository<MISAEntity>
    {
        protected readonly string _connectionString = "";
        protected MySqlConnection _connection;
        // Tên Entity truyền vào
        protected string className = "";

        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// Created By: NDDung (02/09/2022)
        public BaseRepository()
        {
            // Khai báo thông tin database
            _connectionString = "Host=localhost; " +
                    "Port=3306; " +
                    "Database=DATN.QLNS.NDDUNG; " +
                    "User Id = root; " +
                    "Password = 692001Dung";
            // Kết nối đến database
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
            // Lấy tên bảng từ MISAEntity
            className = typeof(MISAEntity).Name;
        }

        public void Dispose()
        {
            _connection.Dispose();
            _connection.Close();
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// Created By: NDDung (02/09/2022)
        public IEnumerable<MISAEntity> GetAll()
        {
            // khai báo store Name
            var storeName = $"Proc_GetAll{className}";

            // Thực hiện lấy dữ liệu:
            var data = _connection.Query<MISAEntity>(sql: storeName, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi </param>
        /// <returns>Đối tượng có id tương ứng</returns>
        /// Created By: NDDung (13/09/2022)
        public virtual MISAEntity GetById(Guid id)
        {
            // khai báo sql Command:
            var sqlCommand = $"Select * FROM {className} WHERE {className}Id = @id";

            // Thực hiện lấy dữ liệu:
            // Khai báo params:
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            // Thực hiện lấy dữ liệu:
            var data = _connection.QueryFirstOrDefault<MISAEntity>(sql: sqlCommand, param: parameters);
            return data;
        }

        /// <summary>
        /// Thêm 1 bản ghi vào bảng
        /// </summary>
        /// <param name="entity">Thông tin bản ghi muốn thêm</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (07/09/2022)
        public virtual int Insert(MISAEntity entity)
        {
            // Thực hiện transaction:
            using (var transaction = _connection.BeginTransaction())
            {
                var storeName = $"Proc_Insert{className}";

                // Đọc các tham số đầu vào của store:
                var sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = storeName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = entity?.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity?.GetType()?.GetProperty(propName)?.GetValue(entity);
                        // Thực hiện gán giá trị cho các param
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, propName);
                    }
                }

                var rowsEffect = _connection.Execute(sql: storeName, param: dynamicParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                if (rowsEffect == 0)
                {
                    transaction.Rollback();
                }
                transaction.Commit();
                return rowsEffect;
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi 
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi</param>
        /// <param name="entity">Thông tin bản ghi muốn sửa</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (07/09/2022)
        public virtual int Update(Guid id, MISAEntity entity)
        {
            // Thực hiện transaction:
            using (var transaction = _connection.BeginTransaction())
            {
                var propPK = typeof(MISAEntity).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PrimaryKey)));
                foreach (var prop in propPK)
                {
                    prop.SetValue(entity, id);
                }
                var storeName = $"Proc_Update{className}s";

                // Đọc các tham số đầu vào của store:
                var sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = storeName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = entity?.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity?.GetType()?.GetProperty(propName)?.GetValue(entity);
                        // Thực hiện gán giá trị cho các param
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, propName);
                    }
                }

                var rowsEffect = _connection.Execute(storeName, param: dynamicParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                transaction.Commit();
                if (rowsEffect == 0)
                {
                    transaction.Rollback();
                }
                return rowsEffect;
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi tại bảng
        /// </summary>
        /// <param name="id">id bản ghi muốn xóa</param>
        /// <returns>
        ///     Trả về số bản ghi thay đổi
        /// </returns>
        /// Created By: NDDung (08/08/2022)
        public int DeleteById(Guid? id)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                // khai báo sql Command:
                var sqlCommand = $"Delete FROM {className} WHERE {className}Id = @id";

                // Thực hiện lấy dữ liệu:
                // Khai báo params:
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                // Thực hiện lấy dữ liệu:
                var data = _connection.Execute(sql: sqlCommand, transaction: transaction, param: parameters);
                transaction.Commit();
                if (data == 0)
                {
                    transaction.Rollback();
                }
                return data;
            }
        }
    }
}

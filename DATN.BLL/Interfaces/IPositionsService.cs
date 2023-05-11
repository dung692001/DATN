using DATN.BLL.Interfaces;
using DATN.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BLL.Interfaces
{
    public interface IPositionsService : IBaseService<Positions>
    {
        public object FilterService(int pageSize, int pageNumber, string? textFilter);
    }
}

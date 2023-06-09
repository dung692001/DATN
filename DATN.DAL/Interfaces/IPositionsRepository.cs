﻿using DATN.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.DAL.Interfaces
{
    public interface IPositionsRepository : IBaseRepository<Positions>
    {
        public Object FilterPositions(int pageSize, int pageNumber, string? textFilter);
    }
}

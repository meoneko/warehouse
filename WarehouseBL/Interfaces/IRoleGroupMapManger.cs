﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.DataContracts;

namespace WarehouseBL.Interfaces
{
    interface IRoleGroupMapManger
    {
        int CreateRoleGroupMap(RoleGroupMap roleGroupMap);
        IList<RoleGroupMap> GetRoleGroupMap();


    }
}

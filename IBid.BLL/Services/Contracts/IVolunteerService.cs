﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAllAdmins();

    }
}

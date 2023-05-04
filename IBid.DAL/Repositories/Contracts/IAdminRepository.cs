using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IAdminRepository<TModel> where TModel : class
    {
        Task<List<Admin>> GetAllAdmins();

    }
}

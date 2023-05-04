using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.DataContext;
using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IBid.DAL.Repositories
{
    public class AdminRepository<TModel> : IAdminRepository<TModel> where TModel: class
    {
        private readonly IbidContext _dbContext;

        public AdminRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            try
            {
                return await _dbContext.Set<Admin>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

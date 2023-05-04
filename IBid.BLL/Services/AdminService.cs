using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services.Contracts;

namespace IBid.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository<Admin> _adminRepository;

        public AdminService(IAdminRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            try
            {
                return await _adminRepository.GetAllAdmins();
            }
            catch
            {
                throw;
            }
        }

    }
}

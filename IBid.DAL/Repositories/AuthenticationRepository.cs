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
    public class AuthenticationRepository<TModel> : IAuthenticationRepository<TModel> where TModel : class
    {
        private readonly IbidContext _dbContext;

        public AuthenticationRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Admin GetAdminByEmail(string email)
        {
            return _dbContext.Admins.FirstOrDefault(u => u.Username == email);
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            return _dbContext.Volunteers.FirstOrDefault(u => u.Username == email);
        }

        public async Task<Volunteer> SignUpVolunteer(Volunteer newVolunteer)
        {
            await _dbContext.Volunteers.AddAsync(newVolunteer);
            await _dbContext.SaveChangesAsync();
            return newVolunteer;
        }
    }
}

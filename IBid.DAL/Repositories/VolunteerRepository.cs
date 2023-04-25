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
    public class VolunteerRepository<TModel> : IVolunteerRepository<TModel> where TModel: class
    {
        private readonly IbidContext _dbContext;

        public VolunteerRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Volunteer>> GetAllVolunteers()
        {
            try
            {
                return await _dbContext.Set<Volunteer>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Volunteer> GetVolunteerById(int id)
        {
            return await _dbContext.Volunteers.FindAsync(id);
        }

        public async Task UpdateVolunteer(Volunteer volunteer)
        { 
            _dbContext.Volunteers.Update(volunteer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVolunteer(int volunteerId)
        {
            var volunteer = await _dbContext.Volunteers.FindAsync(volunteerId);

            if (volunteer == null)
            {
                throw new ArgumentException($"Performance with id {volunteerId} does not exist.");
            }

            _dbContext.Volunteers.Remove(volunteer);
            await _dbContext.SaveChangesAsync();
        }
    }
}

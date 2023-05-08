using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services.Contracts;
using IBid.DAL;

namespace IBid.BLL.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository<Volunteer> _volunteerRepository;

        public VolunteerService(IVolunteerRepository<Volunteer> volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public async Task<List<Volunteer>> GetAllVolunteers()
        {
            try
            {
                return await _volunteerRepository.GetAllVolunteers();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Volunteer> GetVolunteerById(int id)
        {
            return await _volunteerRepository.GetVolunteerById(id);
        }

        public async Task DeleteVolunteer(int volunteerId)
        {
            await _volunteerRepository.DeleteVolunteer(volunteerId);
        }

        public async Task EditVolunteer(int id, string username, string password, string name)
        {
            var volunteer = await _volunteerRepository.GetVolunteerById(id);

            if (volunteer == null)
            {
                throw new ArgumentException(ConstantStrings.volunteerNotFound);
            }

            volunteer.VolunteerId = id;
            volunteer.Username = username;
            volunteer.Password = password;
            volunteer.Name = name;

            await _volunteerRepository.UpdateVolunteer(volunteer);
        }
    }
}

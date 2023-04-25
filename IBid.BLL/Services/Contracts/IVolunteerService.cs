using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IVolunteerService
    {
        Task<List<Volunteer>> GetAllVolunteers();
        Task<Volunteer> GetVolunteerById(int id);
        Task DeleteVolunteer(int volunteerId);
        Task EditVolunteer(int id, string username, string password, string name);
    }
}

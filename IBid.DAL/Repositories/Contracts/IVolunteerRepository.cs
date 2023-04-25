using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IVolunteerRepository<TModel> where TModel : class
    {
        Task<Volunteer> GetVolunteerById(int id);
        Task<List<Volunteer>> GetAllVolunteers();
        Task UpdateVolunteer(Volunteer volunteer);
        Task DeleteVolunteer(int volunteerId);

    }
}

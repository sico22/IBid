using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IAuthenticationRepository<TModel> where TModel : class
    {
        Admin GetAdminByEmail(string email);
        Volunteer GetVolunteerByEmail(string email);
        Task<Volunteer> SignUpVolunteer(Volunteer newVolunteer);

    }
}

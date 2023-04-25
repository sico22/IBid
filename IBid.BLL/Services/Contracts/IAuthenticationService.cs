using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IAuthenticationService
    {
        bool SignInAdmin(string email, string password);
        bool SignInVolunteer(string email, string password);
        Task<Volunteer> SignUpVolunteer(Volunteer newVolunteer);
    }
}

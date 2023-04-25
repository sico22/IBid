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
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository<Admin> _adminRepository;
        private readonly IAuthenticationRepository<Volunteer> _volunteerRepository;

        public AuthenticationService(IAuthenticationRepository<Admin> adminRepository, IAuthenticationRepository<Volunteer> volunteerRepository)
        {
            _adminRepository = adminRepository;
            _volunteerRepository = volunteerRepository; 
        }

        public bool SignInAdmin(string email, string password)
        {
            var user = _adminRepository.GetAdminByEmail(email);

            if (user == null)
            {
                return false;
            }

            if (password != user.Password)
            {
                return false;
            }

            return true;
        }

        public bool SignInVolunteer(string email, string password)
        {
            var user = _volunteerRepository.GetVolunteerByEmail(email);

            if (user == null)
            {
                return false;
            }

            Console.WriteLine(EncodeToBase64(password));
            Console.WriteLine(user.Password);

            if (EncodeToBase64(password) != user.Password)
            {
                return false;
            }

            return true;
        }

        public async Task<Volunteer> SignUpVolunteer(Volunteer newVolunteer)
        {
            try
            {
                newVolunteer.Password = EncodeToBase64(newVolunteer.Password);
                return await _volunteerRepository.SignUpVolunteer(newVolunteer);
            }
            catch
            {
                throw;
            }
        }

        public static string EncodeToBase64(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IBid.PL.Models
{
    public class VMLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}

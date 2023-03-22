using System;
using System.Net.NetworkInformation;
using ATM.BLL.Models;

namespace ATM.BLL.Interface
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(AdminLoginModel model);
        Task<Status> LoginAsync(CustomerLoginModel model);
        Task LogoutAsync();
        //Task<Status> RegisterAsync(RegistrationModel model);
    }
}


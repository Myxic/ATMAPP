using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ATM.BLL.Interface;
using ATM.BLL.Models;
using ATM.DAL.Model;
using System.Net.NetworkInformation;

namespace ATM.BLL.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<Customer> signInManager;
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(SignInManager<Customer> signInManager, UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
         
        }
        public async Task<Status> LoginAsync(CustomerLoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.CardNumber);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }
            return status;
        }

        public async Task<Status> LoginAsync(AdminLoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Password);

            if (model.CompanyName.ToUpper() != "WOWBANKADMIN")
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        //public async Task<Status> RegisterAsync(RegistrationModel model)
        //{
        //    var status = new Status();
        //    var userExists = await userManager.FindByNameAsync(model.UserName);
        //    if (userExists != null)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "User Already Exists";
        //        return status;
        //    }
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.UserName,
        //        Name = model.Name,
        //        EmailConfirmed = true,
        //        //PhoneNumberConfirmed = true,
        //    };

        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        status.StatusCode = 0;
                
        //        status.Message = "User creation failed";
        //        return status;
        //    }
        //    if (!await roleManager.RoleExistsAsync(model.Role))
        //        await roleManager.CreateAsync(new IdentityRole(model.Role));

        //    if (await roleManager.RoleExistsAsync(model.Role))
        //    {
        //        await userManager.AddToRoleAsync(user, model.Role);
        //    }

        //    status.StatusCode = 1;
        //    status.Message = "You have registered successfully";
        //    return status;

        //}
    }
}


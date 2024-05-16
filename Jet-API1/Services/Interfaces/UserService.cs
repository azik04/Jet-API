using Jet_API1.Helpers;
using Jet_API1.Response;
using Jet_API1.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jet_API1.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHelper _jwtHelper;
        public UserService(UserManager<IdentityUser> userManager, JwtHelper jwtHelper, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<BaseResponse<IdentityUser>> CreateRole()
        {
            try
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User"
                };
                IdentityRole role1 = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityRole role2 = new IdentityRole
                {
                    Name = "SuperAdmin"
                };
                await _roleManager.CreateAsync(role);
                await _roleManager.CreateAsync(role1);
                await _roleManager.CreateAsync(role2);
                return new BaseResponse<IdentityUser>
                {
                    Description = "Roles have been succesfully create",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<IdentityUser>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<ICollection<IdentityUser>>> GetAllUsers()
        {
            try
            {
                var data = await _userManager.Users.ToListAsync();

                if (data == null)
                {
                    return new BaseResponse<ICollection<IdentityUser>>
                    {
                        Description = "Users can't be found",
                        StatusCode = Enum.StatusCode.Error
                    };
                }
                return new BaseResponse<ICollection<IdentityUser>>
                {
                    Data = data,
                    Description = "Users have been found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ICollection<IdentityUser>>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<IdentityUser>> GetRole(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Description = "User not found",
                        StatusCode = Enum.StatusCode.UserNotFound
                    };
                }
                var result = await _userManager.GetRolesAsync(user);
                return new BaseResponse<IdentityUser>
                {
                    Data = user,
                    Description = "User Role has found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IdentityUser>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<IdentityUser>> LogIn(AccountVM account)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(account.UserName);
                if (user == null)
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Description = "User hasnt been found",
                        StatusCode = Enum.StatusCode.Error
                    };
                }
                var result = await _signInManager.PasswordSignInAsync(user, account.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                    };

                    foreach (var role in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var token = _jwtHelper.GetToken(authClaims);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return new BaseResponse<IdentityUser>
                    {
                        Data = user,
                        Description = tokenString,
                        StatusCode = Enum.StatusCode.Ok
                    };
                }
                else
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Description = "Name or Password is wrong",
                        StatusCode = Enum.StatusCode.Error
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse<IdentityUser>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<IdentityUser>> Register(AccountVM account)
        {
            try
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = account.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var data = await _userManager.CreateAsync(user, account.Password);
                var role = await _userManager.AddToRoleAsync(user, "User");
                if (data.Succeeded)
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Data = user,
                        Description = $"User:{user.UserName} has been succesfully ceate",
                        StatusCode = Enum.StatusCode.Ok
                    };
                }
                else
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Description = "Something went wrong",
                        StatusCode = Enum.StatusCode.Error
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse<IdentityUser>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<IdentityUser>> UpdateRole(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new BaseResponse<IdentityUser>
                    {
                        Description = "User not found",
                        StatusCode = Enum.StatusCode.Error
                    };
                }
                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Admin");
                return new BaseResponse<IdentityUser> 
                { 
                    Data = user,
                    Description =$"User:{user.UserName} Role has been update",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IdentityUser>
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }
    }
}

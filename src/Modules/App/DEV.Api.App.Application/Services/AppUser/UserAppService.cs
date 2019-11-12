using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using DEV.Api.App.Application.Interfaces;
using DEV.Api.App.Application.Services.AppUser.Input;
using DEV.Api.App.Application.Services.AppUser.ViewModel;
using DEV.API.App.Domain.Core.Auth;

namespace DEV.Api.App.Application.Services.AppUser
{
    public class UserAppService : IUserAppService
    {
        public UserViewModel Aunthenticated(UserInput user)
        {
            var token = new AuthToken().GenerateToken();
            var userViewModel = new UserViewModel()
            {
                Id = 1,
                Token = token.TokenGenerated,
                Username = user.Username
            };

            return userViewModel;
        }
    }
}
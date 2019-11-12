using DEV.Api.App.Application.Services.AppUser.Input;
using DEV.Api.App.Application.Services.AppUser.ViewModel;

namespace DEV.Api.App.Application.Interfaces
{
    public interface IUserAppService
    {
        UserViewModel Aunthenticated(UserInput user);
    }
}

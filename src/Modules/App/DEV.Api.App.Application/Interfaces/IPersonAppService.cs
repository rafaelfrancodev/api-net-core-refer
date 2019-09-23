using DEV.Api.App.Application.Interfaces.Base;
using DEV.Api.App.Application.Services.AppPerson.Input;
using DEV.Api.App.Application.Services.AppPerson.ViewModel;

namespace DEV.Api.App.Application.Interfaces
{
    public interface IPersonAppService : IAppServiceBase<PersonViewModel, PersonInput, int>
    {
    }
}

using DEV.Api.App.Application.Interfaces;
using DEV.Api.App.Application.Services.AppPerson.Input;
using DEV.Api.App.Application.Services.AppPerson.ViewModel;
using DEV.Api.App.Application.Services.AppUser.Input;
using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseControllerCore
    {
        private readonly IUserAppService _userAppService;

        public UserController(
            IUserAppService userAppService,
            INotificationHandler<DomainNotification> notification) : base(notification)
        {
            _userAppService = userAppService;
        }

        [HttpPost("authenticated")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]UserInput userInput)
        {
            var resultado = _userAppService.Aunthenticated(userInput);
            return OkOrNoContent(resultado);
        }

    }
}
using DEV.Api.App.Application.Interfaces;
using DEV.Api.App.Application.Services.AppPerson.Input;
using DEV.Api.App.Application.Services.AppPerson.ViewModel;
using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : BaseControllerCore
    {
        private readonly IPersonAppService _personAppService;

        public PersonController(
            IPersonAppService personAppService,
            INotificationHandler<DomainNotification> notification) : base(notification)
        {
            _personAppService = personAppService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(PersonViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var resultado = await _personAppService.GetAllAsync();
            return OkOrNotFound(resultado);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PersonViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var resultado = await _personAppService.GetByIdAsync(id);
            return OkOrNotFound(resultado);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] PersonInput input)
        {
            var resultado = await _personAppService.InsertAsync(input);
            return OkOrNoContent(resultado);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(PersonViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, PersonInput input)
        {
            var resultado = await _personAppService.UpdateAsync(id, input);
            return OkOrNoContent(resultado);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _personAppService.DeleteAsync(id);
            return OkOrNoContent((object)null);
        }
    }
}
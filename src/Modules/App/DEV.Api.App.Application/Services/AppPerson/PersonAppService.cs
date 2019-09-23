using AutoMapper;
using DEV.Api.App.Application.Interfaces;
using DEV.Api.App.Application.Services.AppPerson.Input;
using DEV.Api.App.Application.Services.AppPerson.ViewModel;
using DEV.API.App.Domain.Core.Inferfaces;
using DEV.API.App.Domain.Core.Message;
using DEV.API.App.Domain.Core.UoW.Base;
using DEV.API.App.Domain.Interfaces.Entities;
using DEV.API.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEV.Api.App.Application.Services.AppPerson
{
    public class PersonAppService : BaseValidationService, IPersonAppService
    {
        private readonly IPersonDomainService _personDomainService;
        private readonly IMapper _mapper;
        private readonly ISmartNotification _notification;

        public PersonAppService(
           IPersonDomainService personDomainService,
           IMapper mapper,
           ISmartNotification notification) : base(notification)
        {
            _personDomainService = personDomainService;
            _mapper = mapper;
            _notification = notification.Invoke();
        }

        public async Task<PersonViewModel> InsertAsync(PersonInput input)
        {
            var person = _mapper.Map<Person>(input);

            if (!person.IsValid())
            {
                NotifyErrorsAndValidation(person);
                return default(PersonViewModel);
            }

            var result = await _personDomainService.InsertAsync(person);
            return _mapper.Map<PersonViewModel>(result);
        }

        public async Task<PersonViewModel> UpdateAsync(int id, PersonInput input)
        {
            var person = _mapper.Map<Person>(input);

            if (!person.IsValid())
            {
                NotifyErrorsAndValidation(person);
                return default(PersonViewModel);
            }

            person.SetId(id);

            var result = await _personDomainService.UpdateAsync(id, person);        
            return _mapper.Map<PersonViewModel>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var person = await _personDomainService.GetByIdAsync(id);
                await _personDomainService.DeleteAsync(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }       

        public async Task<PersonViewModel> GetByIdAsync(int id)
        {
            var resultado = await _personDomainService.GetByIdAsync(id);

            if (resultado == null)
            {
                _notification.NewNotificationBadRequest(MessageKey.RegisterNotFound);
                return default(PersonViewModel);
            }

            return _mapper.Map<PersonViewModel>(resultado);
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllAsync()
        {
            var resultado = await _personDomainService.SelectAsync(x => x.Id > 0);

            if (resultado == null)
            {
                _notification.NewNotificationBadRequest(MessageKey.RegisterNotFound);
                return default(IEnumerable<PersonViewModel>);
            }

            return _mapper.Map<IEnumerable<PersonViewModel>>(resultado);
        }
    }
}

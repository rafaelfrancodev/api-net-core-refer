using AutoMapper;
using DEV.Api.App.Application.Services.AppPerson.Input;
using DEV.API.App.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace DEV.Api.App.Application.Map
{
    [ExcludeFromCodeCoverage]
    public class InputToEntityMapProfile : Profile
    {
        public InputToEntityMapProfile()
        {
            CreateMap<PersonInput, Person>()
                .ForMember(x => x.ValidationResult, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.IdUserChange, opt => opt.Ignore())
                .ForMember(x => x.Deleted, opt => opt.Ignore()
                );          
        }
    }
}

using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace DEV.Api.App.Application.Map
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(
                config => {
                    config.AddProfile<InputToEntityMapProfile>();
                }
            );
        }
    }
}

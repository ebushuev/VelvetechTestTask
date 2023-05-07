using AutoMapper;
using Todo.Common.Models.Domain;
using Todo.Common.Models.DTO;

namespace Todo.Bll.Configuration
{
    public static class BllMapperConfiguration
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Item, ItemDto>();
        }
    }
}

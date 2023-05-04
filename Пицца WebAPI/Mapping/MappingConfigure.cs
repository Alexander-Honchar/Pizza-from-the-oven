using AutoMapper;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;

namespace Pizza_WebAPI.Mapping
{
    public class MappingConfigure:Profile
    {
        public MappingConfigure()
        {
            CreateMap<Pizza, PizzaDTO>().ReverseMap();
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            CreateMap<SizePizza,SizePizzaDTO>().ReverseMap();
            CreateMap<CategoryPizza,CategoryPizzaDTO>().ReverseMap();
            CreateMap<PizzaKingSize, PizzaKingSizeDTO>().ReverseMap();
            CreateMap<CategoryMenu, CategoryMenuDTO>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Workers, WorkerDTO>().ReverseMap();
        }
    }
}

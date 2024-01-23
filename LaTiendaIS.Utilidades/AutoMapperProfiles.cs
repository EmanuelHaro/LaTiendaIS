using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;

namespace LaTiendaIS.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<Talle, TalleDTO>().ReverseMap();
            CreateMap<ColorArticulo, ColorArticuloDTO>().ReverseMap();
        }
    }
}
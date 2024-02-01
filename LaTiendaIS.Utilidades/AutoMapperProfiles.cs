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
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ColorArticulo, ColorArticuloDTO>().ReverseMap();
            CreateMap<Comprobante, ComprobanteDTO>().ReverseMap();
            CreateMap<CondicionTributaria, CondicionTributariaDTO>().ReverseMap();
            CreateMap<LineaDeVenta, LineaDeVentaDTO>().ReverseMap();
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<Pago, PagoDTO>().ReverseMap();
            CreateMap<PagoEfectivo, PagoEfectivoDTO>().ReverseMap();
            CreateMap<PagoConTarjeta, PagoConTarjetaDTO>().ReverseMap();
            CreateMap<PuntoDeVenta, PuntoDeVentaDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<Talle, TalleDTO>().ReverseMap();
        }
    }
}
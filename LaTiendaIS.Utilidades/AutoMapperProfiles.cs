using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;

namespace LaTiendaIS.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArticuloDTO, Articulo>().ReverseMap();
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<ColorArticuloDTO, ColorArticulo>().ReverseMap();
            CreateMap<ComprobanteDTO, Comprobante>().ReverseMap();
            CreateMap<CondicionTributariaDTO, CondicionTributaria>().ReverseMap();
            CreateMap<LineaDeVentaDTO, LineaDeVenta>().ReverseMap();
            CreateMap<MarcaDTO, Marca>().ReverseMap();
            CreateMap<PagoDTO, Pago>().ReverseMap();
            CreateMap<PagoEfectivoDTO, PagoEfectivo>().ReverseMap();
            CreateMap<PagoConTarjetaDTO, PagoConTarjeta>().ReverseMap();
            CreateMap<PuntoDeVentaDTO, PuntoDeVenta>().ReverseMap();
            CreateMap<StockDTO, Stock>().ReverseMap();
            CreateMap<SucursalDTO, Sucursal>().ReverseMap();
            CreateMap<TalleDTO, Talle>().ReverseMap();
            CreateMap<TipoDeComprobanteDTO, TipoDeComprobante>().ReverseMap();
            CreateMap<TipoTalleDTO, TipoTalle>().ReverseMap();
            CreateMap<VentaDTO, Venta>().ReverseMap();

            CreateMap<StockUpdateDto, StockDTO>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcVal) => srcVal != null));
        }
    }
}
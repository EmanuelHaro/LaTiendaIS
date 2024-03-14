using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaTiendaIS.ServiciosAPI.Contrato;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        [HttpGet]
        [Route("{IdCliente}")]
        public async Task<ActionResult> ObtenerCliente(int IdCliente)
        {
            var responseApi = new ResponseAPI<Cliente>();

            try
            {
                var cliente = await _clienteServicio.ObtenerCliente(IdCliente);

                if (cliente != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = cliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Anonimo")]
        public async Task<ActionResult> ObtenerClienteAnonimo()
        {
            var responseApi = new ResponseAPI<Cliente>();

            try
            {
                var cliente = await _clienteServicio.ObtenerClienteAnonimo();

                if (cliente != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = cliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPost]
        public async Task<ActionResult> AgregarCliente(Cliente cliente)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var clienteAgregado = await _clienteServicio.AgregarCliente(cliente);

                if (clienteAgregado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = clienteAgregado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Cliente";
                } 
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Ultima")]
        public async Task<ActionResult> ObtenerUltimaCliente()
        {
            var responseApi = new ResponseAPI<Cliente>();

            try
            {
                var cliente = await _clienteServicio.ObtenerUltimaCliente();

                if (cliente != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = cliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("CUIT/{cuit}")]
        public async Task<ActionResult> ObtenerClientePorCuit(string cuit)
        {
            var responseApi = new ResponseAPI<Cliente>();

            try
            {
                var cliente = await _clienteServicio.ObtenerClientePorCuit(cuit);

                if (cliente != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = cliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

    }
}
using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTiendaIS.ServiciosAPI.Contrato;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class CategoriaServicio: ICategoriaServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public CategoriaServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarCategoria(Categoria Categoria)
        {
            try
            {
                var dbCategoria = _mapper.Map<CategoriaDTO>(Categoria);

                var respModelo = await _unitofwork.Repository<CategoriaDTO>().Crear(dbCategoria);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Categoria");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Categoria> ObtenerCategoria(string descCategoria)
        {
            try
            {
                var dbCategoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.DescripcionCategoria == descCategoria).FirstOrDefaultAsync();
                if (dbCategoria == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<Categoria>(dbCategoria);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}

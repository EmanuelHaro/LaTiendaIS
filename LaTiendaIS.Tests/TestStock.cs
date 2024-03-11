using AutoMapper;
using LaTiendaIS.Server.Controllers;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTiendaIS.Utilidades;
using Microsoft.EntityFrameworkCore.Query;


namespace LaTiendaIS.Tests
{
    [TestClass]
    public class TestStock
    {

        private DBLaTiendaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DBLaTiendaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DBLaTiendaContext(options);

            // Agrega datos de prueba a los DbSet
            context.Articulo.AddRange(
                new ArticuloDTO { IdCodigo = 1, Descripcion = "Remera", Costo = 1000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f, CodigoTienda = 1000 , IdCategoria = 1, IdMarca = 1},
                new ArticuloDTO { IdCodigo = 2, Descripcion = "Pantalon", Costo = 2000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f, CodigoTienda = 1001, IdCategoria = 1, IdMarca = 1}
            );

            context.Stock.AddRange(
                new StockDTO { IdStock = 1,Cantidad = 40, IdArticulo = 1, IdTalle = 1, IdColor = 1 , IdSucursal = 1 },
                new StockDTO { IdStock = 2,Cantidad = 2, IdArticulo = 1, IdTalle = 2, IdColor = 2 , IdSucursal = 1},
                new StockDTO { IdStock = 3, Cantidad = 1, IdArticulo = 2, IdTalle = 1, IdColor = 1, IdSucursal = 1 }
            );


            var tipoTalle = new TipoTalleDTO { IdTipoTalle = 1 ,DescripcionTipoTalle = "Americano"};
            context.TipoTalle.Add(tipoTalle);

            var talle = new TalleDTO { IdTalle = 1, DescripcionTalle = "M" ,IdTipoTalle = 1,TipoTalle = tipoTalle};
            context.Talle.Add(talle);

            var color = new ColorArticuloDTO { IdColor = 1, DescripcionColor = "Rojo" };
            context.ColorArticulo.Add(color);

            context.SaveChanges();

            return context;
        }


        [TestMethod]
        public async Task ObtenerListaDeTalleYColorDelStockDelArticulo1000_DebeRetornarListaCorrectaAsync()
        {
            var config = new MapperConfiguration(x => {
                x.AddProfile(typeof(AutoMapperProfiles));
            });

            var mapper = config.CreateMapper();

            var context = GetInMemoryContext();

            var controller = new StockController(context, mapper);

            var result = await controller.ObtenerListaDeTalleYColorDelStock(1000);



            // Assert
            Assert.IsNotNull(result);


            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);


            var responseApi = okResult.Value as List<Stock>;
            Assert.IsNotNull(responseApi);



        }

        [TestMethod]
        public async Task ModificarCantidadDelStock()
        {
            var config = new MapperConfiguration(x => {
                x.AddProfile(typeof(AutoMapperProfiles));
            });

            var mapper = config.CreateMapper();

            var context = GetInMemoryContext();

            var controller = new StockController(context, mapper);

            var stockModificado = new Stock
            {
                Cantidad = 20
            };

            var result = await controller.ModificarCantidad(1000, "M", "Rojo", stockModificado);



            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var responseApi = okResult.Value as ResponseAPI<int>;
            Assert.IsNotNull(responseApi);
            Assert.IsTrue(responseApi.EsCorrecto);
            Assert.AreEqual(1, responseApi.Valor);



            var stockActualizado = context.Stock.FirstOrDefault(s => s.IdStock == 1);
            Assert.IsNotNull(stockActualizado);
            Assert.AreEqual(20, stockActualizado.Cantidad);



        }

    }

}

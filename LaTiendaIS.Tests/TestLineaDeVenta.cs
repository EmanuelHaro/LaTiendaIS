//using AutoMapper;
//using LaTiendaIS.Server.Controllers;
//using LaTiendaIS.Shared.Models;
//using LaTiendaIS.Shared;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LaTiendaIS.Utilidades;

//namespace LaTiendaIS.Tests
//{
//    [TestClass]
//    public class TestLineaDeVenta
//    {

//        [TestMethod]
//        public async Task ObtenerListaDeTalleYColorDelStockDelArticulo1000_DebeRetornarListaCorrectaAsync()
//        {
//            //NO IMPORTA PORQUE TA TODO ROTO
//            // Arrange
//            var config = new MapperConfiguration(x => x.AddProfile(typeof(AutoMapperProfiles)));
//            var mapper = config.CreateMapper();

//            var context = GetInMemoryContext();
//            var controller = new LineaDeVentaController(context, mapper);

//            var lineaDeVenta = new LineaDeVenta
//            {
//                IdArticulo = 1,
//                IdVenta = 1,
//                Cantidad = 2
//            };

//            // Act
//            var result = await controller.AgregarLineaDeVenta(lineaDeVenta);

//            // Assert
//            Assert.IsNotNull(result);
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            var responseApi = okResult.Value as ResponseAPI<int>;
//            Assert.IsNotNull(responseApi);
//            Assert.IsTrue(responseApi.EsCorrecto);
//            Assert.IsTrue(responseApi.Valor > 0);



//        }

//        private DBLaTiendaContext GetInMemoryContext()
//        {
//            var options = new DbContextOptionsBuilder<DBLaTiendaContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            var context = new DBLaTiendaContext(options);

//            // Agrega un artículo y una venta
//            var articulo = new ArticuloDTO
//            {
//                IdCodigo = 1,
//                Descripcion = "Remera",
//                Costo = 1000,
//                MargenDeGanacia = 15,
//                PorcentajeIVA = 0.21f,
//                CodigoTienda = 1000,
//                IdCategoria = 1,
//                IdMarca = 1
//            };

//            context.Articulo.Add(articulo);

//            var venta = new VentaDTO { IdVenta = 1, FechaVenta = DateTime.Now, Total = 13000 };

//            context.Venta.Add(venta);

//            context.SaveChanges();

//            return context;
//        }


//    }
//}

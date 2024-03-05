using AutoMapper;
using Azure;
using LaTiendaIS.Server.Controllers;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace LaTiendaIS.Tests
{
    [TestClass]
    public class TestListaDeTalleYColor
    {
        [TestMethod]
        public async Task ObtenerListaDeTalleYColorDelStock_DebeRetornarListaCorrectaAsync()
        {

            // Arrange
            var art1 = new Articulo { IdCodigo = 1, Costo = 1000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f };
            var art2 = new Articulo { IdCodigo = 2, Costo = 2000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f };


            var datosStock = new List<StockDTO>
        {
            new StockDTO { Cantidad = 1, IdArticulo = 1, IdTalle = 1, IdColor = 1 },
            new StockDTO { Cantidad = 1,IdArticulo = 1, IdTalle = 2, IdColor = 2 },
            // Agrega más datos según sea necesario
        }.AsQueryable();


            var listaStocks = new List<Stock>
        {
            new Stock { Cantidad = 1, IdArticulo = 1, IdTalle = 1, IdColor = 1 },
            new Stock { Cantidad = 1,IdArticulo = 1, IdTalle = 2, IdColor = 2 },
            // Agrega más datos según sea necesario
        }.AsQueryable();

            var mockSet = new Mock<DbSet<StockDTO>>();

            // 1. Crear mock del DbSet
            var mockStockSet = new Mock<DbSet<StockDTO>>();

            // 2. Configurar comportamiento
            mockStockSet.As<IQueryable<StockDTO>>()
                         .Setup(m => m.GetEnumerator())
                         .Returns(datosStock.GetEnumerator());
            // Retornar solo los datos filtrados

            mockSet.As<IQueryable<StockDTO>>()
                .Setup(m => m.GetEnumerator())
                .Returns(datosStock.GetEnumerator());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<string, string>(It.IsAny<string>())).Returns((string input) => input.ToUpper()); // Mocking the mapping behavior

            var mockContext = new Mock<DBLaTiendaContext>(new DbContextOptions<DBLaTiendaContext>());
            mockContext.Setup(x => x.Set<StockDTO>()).Returns(mockStockSet.Object);

            var controller = new ArticuloController(mockContext.Object, mapperMock.Object);

            // Act
            var result = await controller.ObtenerListaDeTalleYColorDelStock(art1.IdCodigo);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var responseApi = okResult.Value as ResponseAPI<List<Stock>>;
            Assert.IsNotNull(responseApi);
            Assert.IsTrue(responseApi.EsCorrecto);
            Assert.IsNotNull(responseApi.Valor);


        }
    }

       

}

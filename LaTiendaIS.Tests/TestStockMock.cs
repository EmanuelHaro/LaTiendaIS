using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Repositorio.Implementacio;
using Moq;
using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LaTiendaIS.Tests

{
    [TestClass]
    public class TestStockMock
    {
        private Mock<IStockServicio> _mockStockServicio;
        private StockController _stockController;

        [TestInitialize]
        public void Setup()
        {
            _mockStockServicio = new Mock<IStockServicio>();
            _stockController = new StockController(_mockStockServicio.Object);
        }



        [TestMethod]
        public async Task AgregarStock_DebeAgregarStockCorrectamente()
        {
            // Arrange
            var nuevoStock = new Stock
            {
                IdStock = 3,
                Cantidad = 15,
                Articulo = new Articulo
                {
                    IdCodigo = 2,
                    CodigoTienda = 1000,
                    Marca = new Marca { IdMarca = 2, DescripcionMarca = "Marca 2" },
                    Categoria = new Categoria { IdCategoria = 2, DescripcionCategoria = "Categoria 2" }
                },
                Talle = new Talle { IdTalle = 3, DescripcionTalle = "Talle 3" },
                Color = new ColorArticulo { IdColor = 3, DescripcionColor = "Color 3" }
            };

            bool stockAgregadoExitosamente = true;
            _mockStockServicio.Setup(s => s.AgregarStock(nuevoStock)).ReturnsAsync(stockAgregadoExitosamente);

            // Act
            var result = await _stockController.AgregarStock(nuevoStock);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var responseApi = okResult.Value as ResponseAPI<bool>;
            Assert.IsTrue(responseApi.EsCorrecto);
            Assert.IsTrue(responseApi.Valor);

            _mockStockServicio.Verify(s => s.AgregarStock(nuevoStock), Times.Once);
        }

        [TestMethod]
        public async Task ObtenerListaDeStockPorArticulo_DebeRetornarListaDeStock()
        {
            // Arrange
            int codigoTienda = 1000;

            var stocks = new List<Stock>
        {
            new Stock
            {
                IdStock = 1,
                Cantidad = 10,
                Articulo = new Articulo
                {
                    IdCodigo = 1,
                    CodigoTienda = codigoTienda,
                    Marca = new Marca { IdMarca = 1, DescripcionMarca = "Marca 1" },
                    Categoria = new Categoria { IdCategoria = 1, DescripcionCategoria = "Categoria 1" }
                },
                Talle = new Talle { IdTalle = 1, DescripcionTalle = "Talle 1" },
                Color = new ColorArticulo { IdColor = 1, DescripcionColor = "Color 1" }
            },
            new Stock
            {
                IdStock = 2,
                Cantidad = 5,
                Articulo = new Articulo
                {
                    IdCodigo = 1,
                    CodigoTienda = codigoTienda,
                    Marca = new Marca { IdMarca = 1, DescripcionMarca = "Marca 1" },
                    Categoria = new Categoria { IdCategoria = 1, DescripcionCategoria = "Categoria 1" }
                },
                Talle = new Talle { IdTalle = 2, DescripcionTalle = "Talle 2" },
                Color = new ColorArticulo { IdColor = 2, DescripcionColor = "Color 2" }
            }
        };

            _mockStockServicio.Setup(s => s.ObtenerListaDeStockPorArticulo(codigoTienda)).ReturnsAsync(stocks);

            // Act
            var result = await _stockController.ObtenerListaDeStockPorArticulo(codigoTienda);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Stock>));
            var returnedStocks = okResult.Value as List<Stock>;
            Assert.AreEqual(stocks.Count, returnedStocks.Count);
            CollectionAssert.AreEqual(stocks, returnedStocks);
        }

        [TestMethod]
        public async Task ModificarCantidadStock()
        {
            //arrange
            int codigoTienda = 1000;
            string talle = "Tale 1";
            string color = "Color 1";
            int nuevaCantidad = 20;

            var stockOriginal = new Stock
            {
                IdStock = 1,
                Cantidad = 10,
                Articulo = new Articulo
                {
                    IdCodigo = 1,
                    CodigoTienda = codigoTienda,
                    Marca = new Marca { IdMarca = 1, DescripcionMarca = "Marca 1" },
                    Categoria = new Categoria { IdCategoria = 1, DescripcionCategoria = "Categoria 1" }
                },
                Talle = new Talle { IdTalle = 1, DescripcionTalle = "Talle 1" },
                Color = new ColorArticulo { IdColor = 1, DescripcionColor = "Color 1" }
            };


            _mockStockServicio.Setup(s => s.ModificarCantidad(codigoTienda, talle, color, stockOriginal)).ReturnsAsync(true);

            //act

            var result = await _stockController.ModificarCantidad(codigoTienda, talle, color, stockOriginal);

            //assert

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var responseApi = okResult.Value as ResponseAPI<bool>;
            Assert.IsTrue(responseApi.EsCorrecto);
            Assert.IsTrue(responseApi.Valor);

            //verifica que el metodo modificarCantidad esta siendo mockeado correctamente
            _mockStockServicio.Verify(s => s.ModificarCantidad(codigoTienda, talle, color, stockOriginal), Times.Once);

        }





    }
}

using LaTiendaIS.Shared;

namespace LaTiendaIS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalcularPrecioDeVentaDeArticulo_ConCosto1000_Y_MargenDeGanancia15()
        {
            string mensaje = "";
            ArticuloDTO art1 = new ArticuloDTO();
            try
            {
                art1.Costo = 1000;
                art1.MargenDeGanacia = 15;
                art1.PorcentajeIVA = 0.21f;

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            Assert.AreEqual(art1.PrecioDeVenta, 1391.5);
        }
    }

    [TestClass]
    public class VentaTests
    {
        [TestMethod]
        public void CalcularTotalVenta_Cantidad1_Precio1000()
        {
            // Arrange
            var venta = new VentaDTO { IdVenta = 1, FechaVenta = DateTime.Now };
            List<LineaDeVentaDTO> lineadeventas = new List<LineaDeVentaDTO>();
            var lineaDeVenta1 = new LineaDeVentaDTO { IdLineaDeVenta = 1, IdVenta = 1, Cantidad = 1 };
            var lineaDeVenta2 = new LineaDeVentaDTO { IdLineaDeVenta = 2, IdVenta = 1, Cantidad = 1 };
            var art1 = new ArticuloDTO { IdCodigo = 1, Costo = 1000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f }; //1391.5
            var art2 = new ArticuloDTO { IdCodigo = 2, Costo = 2000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f }; //2783
            lineaDeVenta1.Articulo = art1;
            lineaDeVenta2.Articulo = art2;

            lineadeventas.Add(lineaDeVenta1);
            lineadeventas.Add(lineaDeVenta2);

            double totalVenta = 0;
            foreach (var ldv in lineadeventas)
            {
                totalVenta += ldv.Articulo.PrecioDeVenta;
            }


            // Assert
            Assert.AreEqual(4174.5, totalVenta);
        }

    }

    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void ActualizarStock_AgregarVenta_CantidadActualizada()
        {
            // Arrange
            var art1 = new ArticuloDTO { IdCodigo = 1, Descripcion="Remera",Costo = 1000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f }; //1391.5
            // Creamos una lista de stock con un solo elemento que es el artículo de prueba
            var listaStock = new List<StockDTO> {
                new StockDTO {
                    IdStock = 1,
                    IdArticulo = 1,
                    Articulo = art1,
                    Cantidad = 3
                }
            };

            var venta = new VentaDTO { IdVenta = 1 };
            var lineaDeVenta = new LineaDeVentaDTO { IdLineaDeVenta = 1, IdVenta = 1, IdArticulo = 1, Cantidad = 3 };

            // Simulamos la instancia de un servicio de stock
            var servicioStock = new ServicioStockMock(listaStock);

            // Act
            servicioStock.ActualizarStockAlAgregarVenta(venta, lineaDeVenta);

            // Assert
            Assert.AreEqual(0, listaStock[0].Cantidad); // Esperamos que la cantidad de stock se haya actualizado correctamente
        }
    }

    // Clase mock para simular un servicio de stock
    public class ServicioStockMock
    {
        private List<StockDTO> _listaStock;

        public ServicioStockMock(List<StockDTO> listaStock)
        {
            _listaStock = listaStock;
        }

        public void ActualizarStockAlAgregarVenta(VentaDTO venta, LineaDeVentaDTO lineaDeVenta)
        {
            // Actualizamos la cantidad de stock basada en la cantidad vendida
            var stock = _listaStock.Find(s => s.IdArticulo == lineaDeVenta.IdArticulo);
            if (stock != null)
            {
                stock.Cantidad -= lineaDeVenta.Cantidad;
            }
        }
    }
}
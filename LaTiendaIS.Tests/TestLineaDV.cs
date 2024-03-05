using LaTiendaIS.Server.Controllers;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LaTiendaIS.Tests
{
    [TestClass]
    public class TestLineaDV
    {
        [TestMethod]
        public async void AgregarLineaDeVenta()
        {
            // Arrange
            var art1 = new Articulo { IdCodigo = 1, Costo = 1000, MargenDeGanacia = 15, PorcentajeIVA = 0.21f };
            var venta1 = new Venta { IdVenta = 1,FechaVenta = DateTime.Now , Total = 1000 };

            var lineaDeVenta = new LineaDeVenta
            {
                Cantidad = 1,
                IdArticulo = 1,
                IdVenta = 1
            };
            var mockSet = new Mock<DbSet<LineaDeVentaDTO>>();

            var mockContext = new Mock<DBLaTiendaContext>();
            //mockContext.Setup(c => c.LineaDeVenta).Returns(mockSet);

            var controller = new LineaDeVentaController(mockContext.Object, null);

            // Act
            //var resultado = await controller.ObtenerListaDeTalleYColorDelStock(articuloId) as OkObjectResult;

        }
    }
}

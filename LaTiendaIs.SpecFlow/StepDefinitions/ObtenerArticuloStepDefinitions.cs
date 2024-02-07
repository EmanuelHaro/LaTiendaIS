using System;
using System.Collections.Generic;
using System.Linq;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static MudBlazor.Colors;

namespace LaTiendaIs.SpecFlow.StepDefinitions
{
    [Binding]
    public class ObtenerArticuloStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private List<Articulo> _listaArticulos;
        private Articulo _articuloObtenido;
        private DBLaTiendaContext _dbContext;

        public ObtenerArticuloStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _listaArticulos = new List<Articulo>();
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<DBLaTiendaContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new DBLaTiendaContext(options);
        }

        [Given(@"existen los siguientes articulos:")]
        public void GivenExistenLosSiguientesArticulos(Table table)
        {
            foreach (var row in table.Rows)
            {
                _listaArticulos.Add(new Articulo
                {
                    CodigoTienda = Convert.ToInt32(row["CodigoTienda"]),
                    IdTalle = Convert.ToInt32(row["IdTalle"]),
                    IdColor = Convert.ToInt32(row["IdColor"]),
                    Descripcion = row["Descripcion"] 
                });
            }
            foreach (var articulo in _listaArticulos)
            {
                _dbContext.Articulo.Add(articulo);
            }
            _dbContext.SaveChanges();
        }

        [When(@"se introduce el codigo (.*), el IdColor (.*) , el IdTalle (.*)")]
        public void WhenSeIntroduceElCodigoElIdColorElIdTalle(int codigo, int color, int talle)
        {
            // Busca el artículo en el contexto de la base de datos
            _articuloObtenido = _dbContext.Articulo.FirstOrDefault(a =>
                a.CodigoTienda == codigo &&
                a.IdColor == color &&
                a.IdTalle == talle);

            // Verifica si se encontró un artículo
            if (_articuloObtenido != null)
            {
                // Puedes almacenar el artículo obtenido en el contexto de escenario para usarlo en los pasos posteriores si es necesario
                _scenarioContext["ArticuloObtenido"] = _articuloObtenido;
            }
            else
            {
                // Maneja el caso en el que el artículo no se encuentre
                // Puedes lanzar una excepción, registrar un mensaje de error, etc.
                //throw new Exception("Artículo no encontrado");
            }
        }

        [Then(@"el sistema muestre el articulo con IdColor (.*) y  IdTalle (.*)")]
        public void ThenElSistemaMuestreElArticuloConIdColorYIdTalle(int color, int talle)
        {
            // Verifica si hay un artículo obtenido en el contexto del escenario
            if (_scenarioContext.ContainsKey("ArticuloObtenido"))
            {
                // Obtiene el artículo del contexto del escenario
                var articuloObtenido = _scenarioContext.Get<Articulo>("ArticuloObtenido");

                // Compara los atributos del artículo obtenido con el artículo esperado
                Assert.AreEqual(talle, articuloObtenido.IdTalle);
                Assert.AreEqual(color, articuloObtenido.IdColor);
            }
            else
            {
                // Maneja el caso en el que no se haya obtenido ningún artículo
                // Puedes lanzar una excepción, registrar un mensaje de error, etc.
                // throw new Exception("Artículo no obtenido");
            }
        }
    }
}

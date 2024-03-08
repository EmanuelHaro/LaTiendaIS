using LaTiendaIs.SpecFlow.Server;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using static System.Net.WebRequestMethods;

namespace LaTiendaIs.SpecFlow.StepDefinitions
{
    [Binding]
    public class ObtenerStockDelArticuloStepDefinitions
    {
        private List<Articulo> _listaArticulos ;
        private List<Stock> _stock;
        private List<Stock> _stockResultado;
        private HttpClient _httpClient;


        [BeforeTestRun]
        public static void Init()
        {
            //arrancar el servidor
        }

        public ObtenerStockDelArticuloStepDefinitions()
        {
            _listaArticulos = new List<Articulo>();
            _stock = new List<Stock>();
            _stockResultado= new List<Stock>();
            _httpClient = ServidorHelper.ArrancarServidor();
        }

        [Given(@"Existe el siguiente articulo:")]
        public void GivenExisteElSiguienteArticulo(Table table)
        {
            
            foreach (var row in table.Rows)
            {
                _listaArticulos.Add(new Articulo
                {
                    CodigoTienda = Convert.ToInt32(row["CodigoTienda"]),
                    Descripcion = row["Descripcion"].ToString(),
                    Costo = Convert.ToDouble(row["Costo"]),
                    MargenDeGanacia = Convert.ToSingle(row["MargenDeGanacia"]),
                    PorcentajeIVA = Convert.ToSingle(row["PorcentajeIVA"])
                });
            }
        }

        [Given(@"el Stock:")]
        public void GivenElStock(Table table)
        {
            foreach (var row in table.Rows)
            {
                Sucursal sucursal = new Sucursal();
                sucursal.Nombre = row["Sucursal"];

                Articulo art = new Articulo();
                art.CodigoTienda = Convert.ToInt32(row["Articulo"]);

                Talle talle =new Talle();
                talle.DescripcionTalle = (row["Talle"]);

                ColorArticulo color = new ColorArticulo();
                color.DescripcionColor = row["Color"];

                _stock.Add(new Stock
                {
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    Sucursal = sucursal,
                    Articulo = art,
                    Talle = talle,
                    Color = color
                });
            }
        }

        [When(@"se introduce el codigo (.*)")]
        public async Task WhenSeIntroduceElCodigo(int idArticulo)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Stock>>($"api/Articulo/Stock/{idArticulo}");
            if (result!= null)
                _stock =  result;
            else
                throw new Exception("El resultado de la llamada a la Api es nulo");

        }

        [Then(@"el sistema muestre la lista de Stock del articulo:")]
        public void ThenElSistemaMuestreLaListaDeStockDelArticulo(Table table)
        {
            throw new NotImplementedException();
        }
    }
}

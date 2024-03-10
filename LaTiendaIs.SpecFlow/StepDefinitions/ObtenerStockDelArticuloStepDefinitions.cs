using LaTiendaIs.SpecFlow.Server;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using NUnit.Framework;
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
        private Marca _marca;
        private Categoria _categoria;
        private List<Talle> _listaTalle;
        private List<ColorArticulo> _listaColorArticulo;
        private Sucursal _sucursal;

        private int codigo;

        [BeforeTestRun]
        public static void Init()
        {
            //arrancar el servidor
        }

        public ObtenerStockDelArticuloStepDefinitions()
        {
            _listaArticulos = new List<Articulo>();
            _marca = new Marca();
            _categoria = new Categoria();   
            _listaTalle = new List<Talle>();
            _listaColorArticulo = new List<ColorArticulo>();
            _sucursal = new Sucursal();
            _stock = new List<Stock>();
            _stockResultado= new List<Stock>();
            _httpClient = ServidorHelper.ArrancarServidor();
        }

        [Given(@"Existe el siguiente articulo:")]
        public async Task GivenExisteElSiguienteArticulo(Table table)
        {
            _marca.DescripcionMarca = table.Rows[1]["Marca"].ToString();
            _categoria.DescripcionCategoria = table.Rows[1]["Categoria"].ToString();

            var resultMarca = await _httpClient.PostAsJsonAsync<Marca>($"api/Marca",_marca);
            if (!resultMarca.IsSuccessStatusCode)
                throw new Exception("El resultado de la llamada a la Api es nulo");

            var resultCategoria = await _httpClient.PostAsJsonAsync<Categoria>($"api/Categoria", _categoria);
            if (!resultCategoria.IsSuccessStatusCode)
                throw new Exception("El resultado de la llamada a la Api es nulo");


            var resultObtenerMarca = await _httpClient.GetFromJsonAsync<Marca>($"api/Marca/{_marca.DescripcionMarca}");
            if (resultObtenerMarca != null)
                _marca = resultObtenerMarca;
            else
                throw new Exception("El resultado de la llamada a la Api es nulo");

            var resultObtenerCategoria = await _httpClient.GetFromJsonAsync<Categoria>($"api/Categoria/{_categoria.DescripcionCategoria}");
            if (resultObtenerCategoria != null)
                _categoria = resultObtenerCategoria;
            else
                throw new Exception("El resultado de la llamada a la Api es nulo");


            foreach (var row in table.Rows)
            {
                _listaArticulos.Add(new Articulo
                {
                    CodigoTienda = Convert.ToInt32(row["CodigoTienda"]),
                    Descripcion = row["Descripcion"].ToString(),
                    Costo = Convert.ToDouble(row["Costo"]),
                    MargenDeGanacia = Convert.ToSingle(row["MargenDeGanacia"]),
                    PorcentajeIVA = Convert.ToSingle(row["PorcentajeIVA"]),
                    IdMarca = _marca.IdMarca,
                    IdCategoria = _categoria.IdCategoria
                });
            }

            foreach(var art in _listaArticulos)
            {
                var resultArt = await _httpClient.PostAsJsonAsync<Articulo>($"api/Articulo", art);
                if (!resultArt.IsSuccessStatusCode)
                    throw new Exception("El resultado de la llamada a la Api es nulo");
            }

        }

        [Given(@"el Stock:")]
        public async Task GivenElStock(Table table)
        {

            //SUCURSAL
            _sucursal.Nombre = table.Rows[1]["Sucursal"].ToString();
            var resultSucursal = await _httpClient.PostAsJsonAsync<Sucursal>($"api/Sucursal", _sucursal);
            if (!resultSucursal.IsSuccessStatusCode)
                throw new Exception("El resultado de la llamada a la Api es nulo");

            var resultObtenerSucursal = await _httpClient.GetFromJsonAsync<Sucursal>($"api/Sucursal/{_sucursal.Nombre}");
            if (resultObtenerSucursal != null)
                _sucursal = resultObtenerSucursal;
            else
                throw new Exception("El resultado de la llamada a la Api es nulo");

            //COLOR
            foreach (var row in table.Rows)
            {
                var col = new ColorArticulo
                {
                    DescripcionColor = row["Color"].ToString()
                };

                if (!_listaColorArticulo.Any(c => c.DescripcionColor == col.DescripcionColor))
                {
                    _listaColorArticulo.Add(col);
                }
            }

            foreach (var color in _listaColorArticulo)
            {
                var resultColor = await _httpClient.PostAsJsonAsync<ColorArticulo>($"api/ColorArticulo", color);
                if (!resultColor.IsSuccessStatusCode)
                    throw new Exception("El resultado de la llamada a la Api es nulo");
            }

            //TALLE
            foreach (var row in table.Rows)
            {
                var talle = new Talle
                {
                    DescripcionTalle = row["Talle"].ToString()
                };

                if (!_listaTalle.Any(c => c.DescripcionTalle == talle.DescripcionTalle))
                {
                    _listaTalle.Add(talle);
                }
            }

            foreach (var talle in _listaTalle)
            {
                var resultTalle = await _httpClient.PostAsJsonAsync<Talle>($"api/Talle", talle);
                if (!resultTalle.IsSuccessStatusCode)
                    throw new Exception("El resultado de la llamada a la Api es nulo");
            }


            
            foreach (var row in table.Rows)
            {
                Articulo art1 = new Articulo();
                Talle talle1 = new Talle();
                ColorArticulo color1 = new ColorArticulo();

                art1.CodigoTienda = Convert.ToInt32(row["Articulo"]);
                talle1.DescripcionTalle = row["Talle"].ToString();
                color1.DescripcionColor = row["Color"].ToString();

                _stock.Add(new Stock
                {
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    Sucursal = _sucursal,
                    Articulo = art1,
                    Talle = talle1,
                    Color = color1 
                });
            }

            foreach (var stock in _stock)
            {
                var sucursal = await _httpClient.GetFromJsonAsync<Sucursal>($"api/Sucursal/{stock.Sucursal.Nombre}");
                var articulo = await _httpClient.GetFromJsonAsync<Articulo>($"api/Articulo/{stock.Articulo.CodigoTienda}");
                var talle = await _httpClient.GetFromJsonAsync<Talle>($"api/Talle/{stock.Talle.DescripcionTalle}");
                var color = await _httpClient.GetFromJsonAsync<ColorArticulo>($"api/ColorArticulo/{stock.Color.DescripcionColor}");

                if(sucursal != null && articulo != null && talle != null && color !=null)
                {
                    stock.IdSucursal = sucursal.IdSucursal;
                    stock.IdArticulo = articulo.IdCodigo;
                    stock.IdTalle = talle.IdTalle;
                    stock.IdColor = color.IdColor;
                    stock.Color = null;
                    stock.Articulo = null;
                    stock.Sucursal = null;
                    stock.Talle = null;
                        
                    var resultStock = await _httpClient.PostAsJsonAsync<Stock>($"api/Stock", stock);
                    if (!resultStock.IsSuccessStatusCode)
                        throw new Exception("El resultado de la llamada a la Api es nulo");
                }           
            }

        }

        [When(@"se introduce el codigo (.*)")]
        public async Task WhenSeIntroduceElCodigo(int idArticulo)
        {
            var listaStock = await _httpClient.GetFromJsonAsync<List<Stock>>($"api/Stock");


            var result = await _httpClient.GetFromJsonAsync<List<Stock>>($"api/Stock/{idArticulo}");
            if (result!= null)
                _stockResultado =  result;
            else
                throw new Exception("El resultado de la llamada a la Api es nulo");

        }

        [Then(@"el sistema muestre la lista de Stock del articulo:")]
        public void ThenElSistemaMuestreLaListaDeStockDelArticulo(Table table)
        {
            var expectedStock = new List<Stock>();

            foreach (var row in table.Rows)
            {
                
                Articulo art1 = new Articulo();
                Talle talle1 = new Talle();
                ColorArticulo color1 = new ColorArticulo();

                art1.CodigoTienda = Convert.ToInt32(row["Articulo"]);
                talle1.DescripcionTalle = row["Talle"].ToString();
                color1.DescripcionColor = row["Color"].ToString();

                expectedStock.Add(new Stock
                {
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    Sucursal = _sucursal,
                    Articulo = art1,
                    Talle = talle1,
                    Color = color1
                });
            }



            // Assert that the actual and expected stock lists match
            Assert.AreEqual(expectedStock.Count, _stockResultado.Count);


            // Assert individual properties for more detailed verification
            for (int i = 0; i < expectedStock.Count; i++)
            {
                Assert.AreEqual(expectedStock[i].Cantidad, _stockResultado[i].Cantidad);
                Assert.AreEqual(expectedStock[i].Articulo.CodigoTienda, _stockResultado[i].Articulo.CodigoTienda);
                Assert.AreEqual(expectedStock[i].Talle.DescripcionTalle, _stockResultado[i].Talle.DescripcionTalle);
                Assert.AreEqual(expectedStock[i].Color.DescripcionColor, _stockResultado[i].Color.DescripcionColor);
            }

        }
    }
}

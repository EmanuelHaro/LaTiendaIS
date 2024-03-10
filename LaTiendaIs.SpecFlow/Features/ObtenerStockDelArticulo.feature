Feature: ObtenerStockDelArticulo

A short summary of the feature

@tag1
Scenario: ObtenerListaDeStockDelArticulo
	Given Existe el siguiente articulo:
	| CodigoTienda | Descripcion | Costo | MargenDeGanacia | PorcentajeIVA | Marca		  | Categoria				 |
	| 1000         | Remera      | 1000  | 15              | 0.21          | Adidas       | Ropa Deportiva           |
	| 1001         | Pantalon    | 4000  | 20              | 0.21          | Adidas       | Ropa Deportiva           |
	And el Stock:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 2        | Barrio Norte | 1000     | S     | Rojo  |
	| 2        | Barrio Norte | 1000     | M     | Rojo  |
	| 2        | Barrio Norte | 1000     | L     | Azul  |
	| 1        | Barrio Norte | 1001     | S     | Rojo  |

	When se introduce el codigo 1000
	Then el sistema muestre la lista de Stock del articulo:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 2        | Barrio Norte | 1000     | S     | Rojo  |
	| 2        | Barrio Norte | 1000     | M     | Rojo  |
	| 2        | Barrio Norte | 1000     | L     | Azul  |

Feature: ObtenerStockDelArticulo

A short summary of the feature

@tag1
Scenario: ObtenerListaDeStockDelArticulo
	Given Existe el siguiente articulo:
	| CodigoTienda | Descripcion | Costo | MargenDeGanacia | PorcentajeIVA |
	| 1000         | Remera      | 1000  | 15              | 0.21          |
	| 1001         | Pantalon    | 4000  | 20              | 0.21          |
	And el Stock:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 2        | Barrio Norte | 1000     | S     | Rojo  |
	| 2        | Barrio Norte | 1000     | M     | Rojo  |
	| 1        | Barrio Norte | 1000     | L     | Azul  |
	| 1        | Barrio Norte | 1001     | S     | Rojo  |
	| 1        | Barrio Norte | 1001     | M     | Rojo  |
	| 1        | Barrio Norte | 1001     | M     | Azul  |
	
	When se introduce el codigo 1000
	Then el sistema muestre la lista de Stock del articulo:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 2        | Barrio Norte | 1000     | S     | Rojo  |
	| 2        | Barrio Norte | 1000     | M     | Rojo  |
	| 1        | Barrio Norte | 1000     | L     | Azul  |

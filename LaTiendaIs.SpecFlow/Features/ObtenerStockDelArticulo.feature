Feature: ObtenerStockDelArticulo

A short summary of the feature

@tag1
Scenario: ObtenerListaDeStockDelArticulo
	Given Existe el siguiente articulo:
	| CodigoTienda | Descripcion | Costo | MargenDeGanacia | PorcentajeIVA |
	| 1000         | Remera      | 1000  | 15              | 0.21          |
	And el Stock:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 1        | Barrio Norte | 1000     | XL    | Rojo  |
	| 1        | Barrio Norte | 1000     | XL    | Azul  |
	| 1        | Barrio Norte | 1000     | L     | Rojo  |
	| 1        | Barrio Norte | 1001     | L     | Rojo  |
	
	When se introduce el codigo 1000
	Then el sistema muestre la lista de Stock del articulo:
	| Cantidad | Sucursal     | Articulo | Talle | Color |
	| 1        | Barrio Norte | 1000     | XL    | Rojo  |
	| 1        | Barrio Norte | 1000     | XL    | Azul  |
	| 1        | Barrio Norte | 1000     | L     | Rojo  |

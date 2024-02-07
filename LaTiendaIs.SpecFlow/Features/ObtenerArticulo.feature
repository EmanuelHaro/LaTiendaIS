Feature: ObtenerArticulo

A short summary of the feature

@tag1
Scenario: ObtenerArticuloConTalleYColorConExito
	Given existen los siguientes articulos:
	| CodigoTienda | IdColor | IdTalle | Descripcion |
	| 1000         | 1       | 2       | Remera      |
	| 1000         | 1       | 3       | Remera      |
	| 1000         | 2       | 3       | Remera      |
	When se introduce el codigo 1000, el IdColor 2 , el IdTalle 3
	Then el sistema muestre el articulo con IdColor 2 y  IdTalle 3

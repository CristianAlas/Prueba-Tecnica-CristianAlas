/*
Documentación Técnica: Análisis y Decisiones del Esquema de Base de Datos

1. Objetivo del Sistema:
   Se solicita el desarrollo de una base de datos para gestionar información de productos, categorías y ventas, 
   orientada a mostrar productos vendidos en el año 2019 filtrados por categoría.

2. Diseño del Esquema:
    Se crearon tres tablas relacionadas entre sí para representar las entidades básicas:
    
    - Categoria: almacena las categorías de productos (PK: CodigoCategoria).
    - Producto: contiene los productos que pertenecen a una categoría (PK: CodigoProducto, FK: CodigoCategoria).
    - Venta: registra las ventas realizadas, enlazando productos y fechas (PK: CodigoVenta, FK: CodigoProducto).

    Las claves foráneas fueron definidas para garantizar la integridad referencial y permitir consultas cruzadas.
*/

use EvaluacionVentas;

--Tabla Categoria
CREATE TABLE Categoria (
    CodigoCategoria INT PRIMARY KEY,
    Nombre VARCHAR(100)
);
go

--Tabla Producto
CREATE TABLE Producto (
    CodigoProducto INT PRIMARY KEY,
    Nombre VARCHAR(100),
    CodigoCategoria INT,
    FOREIGN KEY (CodigoCategoria) REFERENCES Categoria(CodigoCategoria)
);
go

--Tabla Venta
CREATE TABLE Venta (
    CodigoVenta INT PRIMARY KEY,
    Fecha DATE,
    CodigoProducto INT,
    FOREIGN KEY (CodigoProducto) REFERENCES Producto(CodigoProducto)
);
go

/*
3. Datos de Prueba:
    Se insertaron registros de ejemplo en cada tabla, incluyendo ventas realizadas en distintos años,
    para validar correctamente la lógica de filtrado por año y por categoría.
*/

-- Insertar categorías
INSERT INTO Categoria (CodigoCategoria, Nombre)
VALUES 
(1, 'Ropa'),
(2, 'Electrónica'),
(3, 'Juguetes'),
(4, 'Hogar'),
(5, 'Libros'),
(6, 'Deportes'),
(7, 'Calzado');
go 

-- Insertar productos
INSERT INTO Producto (CodigoProducto, Nombre, CodigoCategoria)
VALUES 
(1, 'Camiseta', 1),
(2, 'Pantalón', 1),
(3, 'Televisor', 2),
(4, 'Celular', 2),
(5, 'Muñeca', 3),
(6, 'Cama', 4),
(7, 'Libro de Ciencia', 5),
(8, 'Balón de fútbol', 6),
(9, 'Tablet', 2),
(10, 'Zapatos', 1),
(11, 'Lámpara', 4),
(12, 'Pelota saltarina', 3),
(13, 'Zapatos Botines', 7);
go 

-- Insertar ventas
INSERT INTO Venta (CodigoVenta, Fecha, CodigoProducto)
VALUES 
(1, '2019-03-15', 1),
(2, '2019-05-10', 2),
(3, '2019-11-23', 3),
(4, '2020-01-10', 4),
(5, '2019-06-05', 5),
(6, '2021-04-01', 2),
(7, '2020-03-15', 6),
(8, '2019-07-15', 7),
(9, '2019-08-21', 8),
(10, '2019-09-10', 9),
(11, '2018-12-31', 1),     
(12, '2019-01-01', 10),    
(13, '2019-12-31', 11),    
(14, '2020-01-01', 12),
(15, '2022-06-15', 13);
go

/*
4. Consulta para el Filtro:
    Se desarrolló una consulta para llenar buscar las categorías que hayan tenido ventas en 2019.
    Se utilizó DISTINCT para eliminar duplicados y se filtró por fechas entre 2019-01-01 y 2019-12-31.
*/

SELECT DISTINCT C.CodigoCategoria, C.Nombre
FROM Venta V
JOIN Producto P ON V.CodigoProducto = P.CodigoProducto
JOIN Categoria C ON P.CodigoCategoria = C.CodigoCategoria
WHERE V.Fecha >= '2019-01-01' AND V.Fecha < '2020-01-01'

/*
5. Consulta Categoría del producto de la última venta realizada:
    Se agregó la consulta que obtiene el nombre de la categoría del producto de la última venta realizada,
    ordenando por fecha descendente y tomando el primer resultado.
*/
SELECT TOP 1 
    C.Nombre AS Categoria,
    P.Nombre AS Producto,
    V.Fecha
FROM Venta V
JOIN Producto P ON V.CodigoProducto = P.CodigoProducto
JOIN Categoria C ON P.CodigoCategoria = C.CodigoCategoria
ORDER BY V.Fecha DESC;

/*
 6. Decisiones:
    - Las fechas fueron colocadas de forma distribuida entre 2019 y otros años para permitir validaciones.
    - Se utilizó el tipo DATE para simplificar el manejo de fechas en las consultas.
    - El diseño es escalable para incluir más datos o funcionalidades, como detalles de venta o usuarios.
*/
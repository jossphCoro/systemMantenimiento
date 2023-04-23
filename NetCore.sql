-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-04-2023 a las 00:22:28
-- Versión del servidor: 8.0.31
-- Versión de PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `computo`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `accesorios`
--

CREATE TABLE `accesorios` (
  `accesorioId` int NOT NULL,
  `accesorio` varchar(100) DEFAULT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  `costo` double DEFAULT NULL,
  `cantidad` bigint DEFAULT NULL,
  `estado` varchar(30) DEFAULT 'Disponible'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `accesorios`
--

INSERT INTO `accesorios` (`accesorioId`, `accesorio`, `descripcion`, `costo`, `cantidad`, `estado`) VALUES
(2, 'audifono', 'Inalambrico', 11, 22, 'Disponible'),
(4, 'tester', 'manipualdo', 23, 44, 'Disponible');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `categoriaId` int NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `tipo` varchar(150) NOT NULL,
  `descripcion` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`categoriaId`, `nombre`, `tipo`, `descripcion`) VALUES
(3, 'Instalaciones Tecnicas', 'venta', 'Routers Mikrotik'),
(4, 'Equipos Oficina', 'Comodato', 'Uso sucursal 1'),
(5, 'Materiles', 'Instalacion', 'Uso instalaciones');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `productoId` int NOT NULL,
  `producto` varchar(100) NOT NULL,
  `costo` double NOT NULL,
  `serie` int NOT NULL,
  `stock` int NOT NULL,
  `estado` varchar(20) NOT NULL DEFAULT 'Disponible'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`productoId`, `producto`, `costo`, `serie`, `stock`, `estado`) VALUES
(19, 'Cable UTP', 324, 2342323, 100, 'Agotado'),
(20, 'asdas', 123, 123, 312, 'Disponible'),
(21, 'asd', 4, 34, 243, 'Disponible'),
(22, 'sad', 34, 324, 234, 'Disponible'),
(23, 'ASD', 324, 234, 324, 'Disponible'),
(24, 'asd', 321, 123, 213, 'Disponible'),
(25, 'asd', 234, 234, 234, 'Disponible'),
(26, 'sda', 23, 234, 234, 'Disponible'),
(27, 'asd', 234, 234, 234, 'Disponible'),
(28, 'Desde la otra Mac', 34, 324, 234, 'Disponible'),
(29, 'sdf', 2343, 234, 234, 'Agotado'),
(30, 'asd', 324, 342, 234, 'Disponible');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `accesorios`
--
ALTER TABLE `accesorios`
  ADD PRIMARY KEY (`accesorioId`);

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`categoriaId`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`productoId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `accesorios`
--
ALTER TABLE `accesorios`
  MODIFY `accesorioId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `categoriaId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `productoId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

CREATE DATABASE IF NOT EXISTS `xrestadoscuentas`;
USE xrestadoscuentas;


CREATE TABLE `campos`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_formato` varchar(50) NULL,
  `nombre` varchar(100) NULL,
  `tipo` varchar(10) NULL,
  `idcampo` varchar(50) NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `condicion`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_formato` varchar(50) NULL,
  `id_tipo_operacion` int NULL,
  `tipo` int NULL,
  `cadena` varchar(200) NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `configuracion`  (
  `id` int NOT NULL,
  `folder_in` varchar(1000) NULL,
  `folder_out` varchar(1000) NULL,
  `copia_origen` int NULL DEFAULT 0,
  PRIMARY KEY (`id`)
);

CREATE TABLE `fechas`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_formato` varchar(50) NULL,
  `tipo` varchar(50) NULL,
  `inicio` varchar(200) NULL,
  `fin` varchar(200) NULL,
  `dia_length` int NULL,
  `mes_length` int NULL,
  `anio_length` int NULL,
  `separador` varchar(10) NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `ficheros_procesados`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(1000) NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `formato`  (
  `id_formato` varchar(50) NOT NULL,
  `banco` varchar(50) NULL,
  `algoritmo` varchar(50) NULL,
  `cadena` varchar(200) NULL,
  `idcamposdescripcion` varchar(255) NULL,
  PRIMARY KEY (`id_formato`)
);

CREATE TABLE `prefijos`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_formato` varchar(50) NULL,
  `tipo` varchar(50) NULL,
  `inicio` varchar(200) NULL,
  `fin` varchar(200) NULL,
  `separador` varchar(10) NULL,
  `size` int NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `tipo_operacion`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_formato` varchar(50) NULL,
  `tipo` int NULL,
  PRIMARY KEY (`id`)
);


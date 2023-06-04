CREATE DATABASE test
CHARACTER SET utf8mb4
COLLATE utf8mb4_0900_ai_ci;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE test;

--
-- Create table `cliente`
--
CREATE TABLE cliente (
  id int NOT NULL AUTO_INCREMENT,
  nombre varchar(100) NOT NULL,
  apellido varchar(100) NOT NULL,
  fecha_nacimiento date NOT NULL,
  direccion varchar(250) DEFAULT NULL,
  telefono bigint DEFAULT NULL,
  email varchar(200) DEFAULT NULL,
  dni bigint NOT NULL,
  cuit bigint DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `dni` on table `cliente`
--
ALTER TABLE cliente
ADD UNIQUE INDEX dni (dni);

--
-- Create table `usuario_acceso`
--
CREATE TABLE usuario_acceso (
  id int NOT NULL AUTO_INCREMENT,
  usuario varchar(10) NOT NULL,
  password varchar(20) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;


ALTER TABLE test.usuario_acceso
ADD UNIQUE INDEX usuario (usuario);
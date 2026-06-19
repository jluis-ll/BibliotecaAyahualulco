CREATE DATABASE  IF NOT EXISTS `proyectobiblioteca` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `proyectobiblioteca`;
-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: proyectobiblioteca
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `autor`
--

DROP TABLE IF EXISTS `autor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `autor` (
  `idAutor` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  PRIMARY KEY (`idAutor`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autor`
--

LOCK TABLES `autor` WRITE;
/*!40000 ALTER TABLE `autor` DISABLE KEYS */;
INSERT INTO `autor` VALUES (1,'Gabriel García Márquez'),(2,'Miguel de Cervantes'),(3,'Antoine de Saint-Exupéry'),(4,'J.K. Rowling'),(5,'George Orwell'),(6,'Jane Austen'),(7,'Homero'),(8,'J.R.R. Tolkien'),(9,'Julio Cortázar'),(10,'Isaac Asimov');
/*!40000 ALTER TABLE `autor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bibliotecario`
--

DROP TABLE IF EXISTS `bibliotecario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bibliotecario` (
  `idBibliotecario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `correoElectronico` varchar(100) NOT NULL,
  `Contraseña` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idBibliotecario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bibliotecario`
--

LOCK TABLES `bibliotecario` WRITE;
/*!40000 ALTER TABLE `bibliotecario` DISABLE KEYS */;
INSERT INTO `bibliotecario` VALUES (1,'Carlos Mendoza','carlos.mendoza@biblioteca.com','CarlosMendozaAdmin01!'),(2,'Laura Hernández','laura.hernandez@biblioteca.com','LauraHernandezAdmin02#'),(3,'Miguel Torres','miguel.torres@biblioteca.com','MiguelTorresAdmin03$');
/*!40000 ALTER TABLE `bibliotecario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `copia_libro`
--

DROP TABLE IF EXISTS `copia_libro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `copia_libro` (
  `idCopia` int NOT NULL AUTO_INCREMENT,
  `estadoCopia` varchar(50) NOT NULL,
  `folioLibro` int NOT NULL,
  PRIMARY KEY (`idCopia`),
  KEY `folioLibro` (`folioLibro`),
  CONSTRAINT `copia_libro_ibfk_1` FOREIGN KEY (`folioLibro`) REFERENCES `libro` (`folioLibro`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `copia_libro`
--

LOCK TABLES `copia_libro` WRITE;
/*!40000 ALTER TABLE `copia_libro` DISABLE KEYS */;
INSERT INTO `copia_libro` VALUES (21,'Disponible',1),(22,'Prestado',1),(23,'Disponible',2),(24,'En reparación',2),(25,'Disponible',3),(26,'Prestado',3),(27,'Disponible',4),(28,'Disponible',4),(29,'Prestado',5),(30,'Disponible',5);
/*!40000 ALTER TABLE `copia_libro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credencial`
--

DROP TABLE IF EXISTS `credencial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `credencial` (
  `matriculaCredencial` int NOT NULL AUTO_INCREMENT,
  `numero` int NOT NULL,
  PRIMARY KEY (`matriculaCredencial`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credencial`
--

LOCK TABLES `credencial` WRITE;
/*!40000 ALTER TABLE `credencial` DISABLE KEYS */;
INSERT INTO `credencial` VALUES (1,1001),(2,1002),(3,1003),(4,1004),(5,1005),(6,1006),(7,1007),(8,1008),(9,1009),(10,1010);
/*!40000 ALTER TABLE `credencial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `editorial`
--

DROP TABLE IF EXISTS `editorial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `editorial` (
  `idEditorial` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  PRIMARY KEY (`idEditorial`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `editorial`
--

LOCK TABLES `editorial` WRITE;
/*!40000 ALTER TABLE `editorial` DISABLE KEYS */;
INSERT INTO `editorial` VALUES (1,'Penguin Random House'),(2,'Planeta'),(3,'HarperCollins'),(4,'Alfaguara'),(5,'McGraw-Hill'),(6,'Pearson'),(7,'Anaya'),(8,'Santillana'),(9,'Océano'),(10,'Fondo de Cultura Económica');
/*!40000 ALTER TABLE `editorial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `libro`
--

DROP TABLE IF EXISTS `libro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `libro` (
  `folioLibro` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `isbn` int NOT NULL,
  `condicionLibro` varchar(50) NOT NULL,
  `numeroPaginas` int NOT NULL,
  `paisPublicacion` varchar(50) NOT NULL,
  `numeroCopias` int NOT NULL,
  `idEditorial` int NOT NULL,
  `idUbicacion` int NOT NULL,
  PRIMARY KEY (`folioLibro`),
  KEY `idEditorial` (`idEditorial`),
  KEY `idUbicacion` (`idUbicacion`),
  CONSTRAINT `libro_ibfk_1` FOREIGN KEY (`idEditorial`) REFERENCES `editorial` (`idEditorial`),
  CONSTRAINT `libro_ibfk_2` FOREIGN KEY (`idUbicacion`) REFERENCES `ubicacion` (`idUbicacion`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `libro`
--

LOCK TABLES `libro` WRITE;
/*!40000 ALTER TABLE `libro` DISABLE KEYS */;
INSERT INTO `libro` VALUES (1,'Cien Años de Soledad',978100001,'Nuevo',417,'Colombia',5,1,1),(2,'Don Quijote de la Mancha',978100002,'Bueno',863,'España',3,2,2),(3,'El Principito',978100003,'Nuevo',120,'Francia',4,3,3),(4,'Harry Potter y la Piedra Filosofal',978100004,'Nuevo',320,'Reino Unido',6,4,4),(5,'1984',978100005,'Usado',328,'Reino Unido',2,5,5),(6,'Orgullo y Prejuicio',978100006,'Bueno',279,'Reino Unido',3,6,6),(7,'Crónica de una Muerte Anunciada',978100007,'Regular',122,'Colombia',2,7,7),(8,'La Odisea',978100008,'Usado',500,'Grecia',1,8,8),(9,'El Hobbit',978100009,'Nuevo',310,'Reino Unido',7,9,9),(10,'Rayuela',978100010,'Bueno',600,'Argentina',2,10,10),(11,'Libro1',232542,'Nuevo',4546,'zimbawe',4,10,11);
/*!40000 ALTER TABLE `libro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `libro_autor`
--

DROP TABLE IF EXISTS `libro_autor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `libro_autor` (
  `folioLibro` int NOT NULL,
  `idAutor` int NOT NULL,
  PRIMARY KEY (`folioLibro`,`idAutor`),
  KEY `idAutor` (`idAutor`),
  CONSTRAINT `libro_autor_ibfk_1` FOREIGN KEY (`folioLibro`) REFERENCES `libro` (`folioLibro`),
  CONSTRAINT `libro_autor_ibfk_2` FOREIGN KEY (`idAutor`) REFERENCES `autor` (`idAutor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `libro_autor`
--

LOCK TABLES `libro_autor` WRITE;
/*!40000 ALTER TABLE `libro_autor` DISABLE KEYS */;
INSERT INTO `libro_autor` VALUES (1,1),(7,1),(2,2),(3,3),(4,4),(5,5),(6,6),(8,7),(9,8),(10,9),(11,9);
/*!40000 ALTER TABLE `libro_autor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pasillo`
--

DROP TABLE IF EXISTS `pasillo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pasillo` (
  `idPasillo` int NOT NULL AUTO_INCREMENT,
  `nomPasillo` varchar(100) NOT NULL,
  PRIMARY KEY (`idPasillo`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pasillo`
--

LOCK TABLES `pasillo` WRITE;
/*!40000 ALTER TABLE `pasillo` DISABLE KEYS */;
INSERT INTO `pasillo` VALUES (1,'Pasillo A'),(2,'Pasillo B'),(3,'Pasillo C'),(4,'Pasillo D'),(5,'Pasillo E'),(6,'Pasillo F'),(7,'Pasillo G'),(8,'Pasillo H'),(9,'Pasillo I'),(10,'Pasillo J');
/*!40000 ALTER TABLE `pasillo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prestamos`
--

DROP TABLE IF EXISTS `prestamos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prestamos` (
  `numPrestamo` int NOT NULL AUTO_INCREMENT,
  `fechaInicio` date NOT NULL,
  `fechaEntrega` date NOT NULL,
  `estatusPrestamo` varchar(50) NOT NULL,
  `folioLibro` int NOT NULL,
  `numSocio` int NOT NULL,
  `idBibliotecario` int DEFAULT NULL,
  PRIMARY KEY (`numPrestamo`),
  KEY `folioLibro` (`folioLibro`),
  KEY `numSocio` (`numSocio`),
  KEY `fk_prestamo_bibliotecario` (`idBibliotecario`),
  CONSTRAINT `fk_prestamo_bibliotecario` FOREIGN KEY (`idBibliotecario`) REFERENCES `bibliotecario` (`idBibliotecario`),
  CONSTRAINT `prestamos_ibfk_1` FOREIGN KEY (`folioLibro`) REFERENCES `libro` (`folioLibro`),
  CONSTRAINT `prestamos_ibfk_2` FOREIGN KEY (`numSocio`) REFERENCES `socio` (`numSocio`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prestamos`
--

LOCK TABLES `prestamos` WRITE;
/*!40000 ALTER TABLE `prestamos` DISABLE KEYS */;
INSERT INTO `prestamos` VALUES (1,'2026-05-01','2026-05-10','Entregado',1,1,1),(2,'2026-05-03','2026-05-12','Prestado',2,2,1),(3,'2026-05-05','2026-05-15','Entregado',3,3,2),(4,'2026-05-06','2026-05-16','Prestado',4,4,3),(5,'2026-05-08','2026-05-18','Retrasado',5,5,2),(6,'2026-05-10','2026-05-20','Prestado',6,6,2),(7,'2026-05-12','2026-05-22','Entregado',7,7,2),(8,'2026-05-14','2026-05-24','Prestado',8,8,3),(9,'2026-05-16','2026-05-26','Retrasado',9,9,3),(10,'2026-05-18','2026-05-28','Prestado',10,10,3);
/*!40000 ALTER TABLE `prestamos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reserva`
--

DROP TABLE IF EXISTS `reserva`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reserva` (
  `idReserva` int NOT NULL AUTO_INCREMENT,
  `numSocio` int NOT NULL,
  `folioLibro` int NOT NULL,
  `fechaReserva` date NOT NULL,
  PRIMARY KEY (`idReserva`),
  KEY `fk_reserva_socio` (`numSocio`),
  KEY `fk_reserva_libro` (`folioLibro`),
  CONSTRAINT `fk_reserva_libro` FOREIGN KEY (`folioLibro`) REFERENCES `libro` (`folioLibro`),
  CONSTRAINT `fk_reserva_socio` FOREIGN KEY (`numSocio`) REFERENCES `socio` (`numSocio`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reserva`
--

LOCK TABLES `reserva` WRITE;
/*!40000 ALTER TABLE `reserva` DISABLE KEYS */;
INSERT INTO `reserva` VALUES (1,1,3,'2026-06-03'),(2,2,5,'2026-06-03'),(3,3,1,'2026-06-04'),(4,4,8,'2026-06-04'),(5,5,2,'2026-06-05'),(6,6,7,'2026-06-05'),(7,7,4,'2026-06-06'),(8,8,6,'2026-06-06'),(9,9,9,'2026-06-07'),(10,10,10,'2026-06-07');
/*!40000 ALTER TABLE `reserva` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sancion`
--

DROP TABLE IF EXISTS `sancion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sancion` (
  `folio` int NOT NULL AUTO_INCREMENT,
  `limitePago` date NOT NULL,
  `descripcion` varchar(200) NOT NULL,
  `montoSancion` int NOT NULL,
  `numPrestamo` int NOT NULL,
  `idBibliotecario` int NOT NULL,
  PRIMARY KEY (`folio`),
  KEY `numPrestamo` (`numPrestamo`),
  CONSTRAINT `sancion_ibfk_1` FOREIGN KEY (`numPrestamo`) REFERENCES `prestamos` (`numPrestamo`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sancion`
--

LOCK TABLES `sancion` WRITE;
/*!40000 ALTER TABLE `sancion` DISABLE KEYS */;
INSERT INTO `sancion` VALUES (1,'2026-06-05','Entrega tardía de libro',50,1,1),(2,'2026-06-08','Daño menor en portada',75,2,2),(3,'2026-06-10','Retraso de 5 días',100,3,3),(4,'2026-06-12','Pérdida de separador institucional',30,4,1),(5,'2026-06-15','Retraso de 10 días',150,5,2),(6,'2026-06-18','Daño en páginas internas',120,6,3),(7,'2026-06-20','Entrega fuera de plazo',80,7,1),(8,'2026-06-22','Retraso de 7 días',110,8,2),(9,'2026-06-25','Libro devuelto en mal estado',200,9,3),(10,'2026-06-28','Retraso de 15 días',250,10,1);
/*!40000 ALTER TABLE `sancion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `socio`
--

DROP TABLE IF EXISTS `socio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `socio` (
  `numSocio` int NOT NULL AUTO_INCREMENT,
  `nombCompleto` varchar(100) NOT NULL,
  `direccion` varchar(200) NOT NULL,
  `correoElectronico` varchar(100) NOT NULL,
  `matriculaCredencial` int NOT NULL,
  `Contraseña` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`numSocio`),
  KEY `fk_socio_credencial` (`matriculaCredencial`),
  CONSTRAINT `fk_socio_credencial` FOREIGN KEY (`matriculaCredencial`) REFERENCES `credencial` (`matriculaCredencial`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `socio`
--

LOCK TABLES `socio` WRITE;
/*!40000 ALTER TABLE `socio` DISABLE KEYS */;
INSERT INTO `socio` VALUES (1,'Ana López','Calle Juárez 12','ana.lopez@gmail.com',1,'Libro2026!'),(2,'Luis Martínez','Av. Central 45','luis.martinez@gmail.com',2,'Lectura2026#'),(3,'María Hernández','Col. Reforma 78','maria.hernandez@gmail.com',3,'Biblioteca01$'),(4,'Jorge Ramírez','Calle Hidalgo 23','jorge.ramirez@gmail.com',4,'Pagina123@'),(5,'Sofía Torres','Av. Universidad 90','sofia.torres@gmail.com',5,'Novela456!'),(6,'Daniel Cruz','Col. Centro 15','daniel.cruz@gmail.com',6,'Autor789#'),(7,'Fernanda Ruiz','Calle Independencia 56','fernanda.ruiz@gmail.com',7,'Socio321$'),(8,'Ricardo Gómez','Av. Las Palmas 101','ricardo.gomez@gmail.com',8,'Prestamo654@'),(9,'Valeria Flores','Col. Los Pinos 34','valeria.flores@gmail.com',9,'Catalogo987!'),(10,'Miguel Sánchez','Calle Morelos 88','miguel.sanchez@gmail.com',10,'Usuario159#');
/*!40000 ALTER TABLE `socio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `telefono`
--

DROP TABLE IF EXISTS `telefono`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `telefono` (
  `idTelefono` int NOT NULL AUTO_INCREMENT,
  `numero` varchar(15) NOT NULL,
  `numSocio` int NOT NULL,
  PRIMARY KEY (`idTelefono`),
  KEY `numSocio` (`numSocio`),
  CONSTRAINT `telefono_ibfk_1` FOREIGN KEY (`numSocio`) REFERENCES `socio` (`numSocio`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `telefono`
--

LOCK TABLES `telefono` WRITE;
/*!40000 ALTER TABLE `telefono` DISABLE KEYS */;
INSERT INTO `telefono` VALUES (1,'2281000001',1),(2,'2281000002',2),(3,'2281000003',3),(4,'2281000004',4),(5,'2281000005',5),(6,'2281000006',6),(7,'2281000007',7),(8,'2281000008',8),(9,'2281000009',9),(10,'2281000010',10);
/*!40000 ALTER TABLE `telefono` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tema`
--

DROP TABLE IF EXISTS `tema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tema` (
  `idTema` int NOT NULL AUTO_INCREMENT,
  `nombTema` varchar(100) NOT NULL,
  PRIMARY KEY (`idTema`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tema`
--

LOCK TABLES `tema` WRITE;
/*!40000 ALTER TABLE `tema` DISABLE KEYS */;
INSERT INTO `tema` VALUES (1,'Literatura'),(2,'Historia'),(3,'Ciencia'),(4,'Tecnología'),(5,'Matemáticas'),(6,'Arte'),(7,'Filosofía'),(8,'Medicina'),(9,'Programación'),(10,'Educación');
/*!40000 ALTER TABLE `tema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ubicacion`
--

DROP TABLE IF EXISTS `ubicacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ubicacion` (
  `idUbicacion` int NOT NULL AUTO_INCREMENT,
  `piso` varchar(50) NOT NULL,
  `idTema` int NOT NULL,
  `idPasillo` int NOT NULL,
  PRIMARY KEY (`idUbicacion`),
  KEY `fk_ubicacion_tema` (`idTema`),
  KEY `fk_ubicacion_pasillo` (`idPasillo`),
  CONSTRAINT `fk_ubicacion_pasillo` FOREIGN KEY (`idPasillo`) REFERENCES `pasillo` (`idPasillo`),
  CONSTRAINT `fk_ubicacion_tema` FOREIGN KEY (`idTema`) REFERENCES `tema` (`idTema`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ubicacion`
--

LOCK TABLES `ubicacion` WRITE;
/*!40000 ALTER TABLE `ubicacion` DISABLE KEYS */;
INSERT INTO `ubicacion` VALUES (1,'Planta Baja',1,1),(2,'Primer Piso',2,2),(3,'Segundo Piso',3,3),(4,'Planta Baja',4,4),(5,'Primer Piso',5,5),(6,'Segundo Piso',6,6),(7,'Planta Baja',7,7),(8,'Primer Piso',8,8),(9,'Segundo Piso',9,9),(10,'Planta Baja',10,10),(11,'Planta Baja',9,9);
/*!40000 ALTER TABLE `ubicacion` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-11 13:49:48

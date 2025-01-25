-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               11.4.4-MariaDB - mariadb.org binary distribution
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              12.8.0.6908
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Zrzut struktury bazy danych Emejzon
CREATE DATABASE IF NOT EXISTS `Emejzon` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `Emejzon`;

-- Zrzut struktury tabela Emejzon.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClientId` int(11) NOT NULL DEFAULT 0,
  `WorkerId` int(11) NOT NULL DEFAULT 0,
  `Products` varchar(1024) NOT NULL DEFAULT '0',
  `Status` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `Client` (`ClientId`),
  KEY `Worker` (`WorkerId`),
  CONSTRAINT `Client` FOREIGN KEY (`ClientId`) REFERENCES `users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Worker` FOREIGN KEY (`WorkerId`) REFERENCES `workers` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Eksport danych został odznaczony.

-- Zrzut struktury tabela Emejzon.products
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(64) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `Catergory` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Eksport danych został odznaczony.

-- Zrzut struktury tabela Emejzon.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(64) NOT NULL DEFAULT 'John',
  `Surname` varchar(64) NOT NULL DEFAULT 'Pork',
  `Email` varchar(128) NOT NULL DEFAULT 'example@invalid.com',
  `PhoneNumber` int(9) NOT NULL DEFAULT 111222333,
  `City` varchar(64) NOT NULL DEFAULT 'Poznan',
  `Address` varchar(256) NOT NULL DEFAULT 'Fredry 13',
  `Position` enum('Admin','Manager','Worker','Client') NOT NULL,
  `Password` varchar(512) NOT NULL DEFAULT 'password123',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Eksport danych został odznaczony.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               10.4.32-MariaDB - mariadb.org binary distribution
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Zrzut struktury bazy danych emejzon
CREATE DATABASE IF NOT EXISTS `emejzon` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `emejzon`;

-- Zrzut struktury tabela emejzon.products
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(64) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `Category` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzut struktury tabela emejzon.users
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzut struktury tabela emejzon.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClientId` int(11) NOT NULL,
  `WorkerId` int(11) DEFAULT NULL,
  `Status` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `ClientId` (`ClientId`),
  KEY `WorkerId` (`WorkerId`),
  CONSTRAINT `ClientId` FOREIGN KEY (`ClientId`) REFERENCES `users` (`Id`),
  CONSTRAINT `WorkerId` FOREIGN KEY (`WorkerId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzut struktury tabela emejzon.log
CREATE TABLE IF NOT EXISTS `log` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Date` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  `UserId` INT NOT NULL,
  `Message` VARCHAR(512) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzut struktury tabela emejzon.orderproducts
CREATE TABLE IF NOT EXISTS `orderproducts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `OrderId` (`OrderId`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`),
  CONSTRAINT `ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Eksport danych został odznaczony.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
﻿--
-- Script was generated by Devart dbForge Studio 2019 for MySQL, Version 8.2.23.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 2021.12.10 10:50:44
-- Server version: 5.5.5-10.4.6-MariaDB
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

DROP DATABASE IF EXISTS moveyourbody;

CREATE DATABASE IF NOT EXISTS moveyourbody
	CHARACTER SET utf8
	COLLATE utf8_hungarian_ci;

--
-- Set default database
--
USE moveyourbody;

--
-- Create table `location`
--
CREATE TABLE IF NOT EXISTS location (
  id INT(11) NOT NULL AUTO_INCREMENT,
  city_name VARCHAR(50) NOT NULL,
  county_name VARCHAR(255) NOT NULL,
  address_name VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create table `user`
--
CREATE TABLE IF NOT EXISTS user (
  id INT(11) NOT NULL AUTO_INCREMENT,
  email VARCHAR(320) NOT NULL,
  full_name VARCHAR(255) NOT NULL,
  password VARCHAR(30) NOT NULL,
  phone_number VARCHAR(12) NOT NULL,
  trainer BIT(1) NOT NULL DEFAULT b'0',
  location_id INT(11) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create index `phone_number` on table `user`
--
ALTER TABLE user 
  ADD UNIQUE INDEX phone_number(phone_number);

--
-- Create foreign key
--
ALTER TABLE user 
  ADD CONSTRAINT FK_user_city_id FOREIGN KEY (location_id)
    REFERENCES location(id) ON DELETE NO ACTION;

--
-- Create table `category`
--
CREATE TABLE IF NOT EXISTS category (
  name VARCHAR(100) NOT NULL
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create index `name` on table `category`
--
ALTER TABLE category 
  ADD UNIQUE INDEX name(name);

--
-- Create table `training`
--
CREATE TABLE IF NOT EXISTS training (
  id INT(11) NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  category VARCHAR(100) NOT NULL,
  trainer_id INT(11) NOT NULL,
  min_member INT(3) NOT NULL,
  max_member INT(3) NOT NULL,
  description VARCHAR(255) NOT NULL,
  contact_phone VARCHAR(12) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create foreign key
--
ALTER TABLE training 
  ADD CONSTRAINT FK_training_category FOREIGN KEY (category)
    REFERENCES category(name) ON DELETE NO ACTION;

--
-- Create foreign key
--
ALTER TABLE training 
  ADD CONSTRAINT FK_training_trainer_id FOREIGN KEY (trainer_id)
    REFERENCES user(id) ON DELETE NO ACTION;

--
-- Create table `training_session`
--
CREATE TABLE IF NOT EXISTS training_session (
  id INT(11) NOT NULL,
  training_id INT(11) DEFAULT NULL,
  date DATETIME DEFAULT NULL,
  place VARCHAR(255) DEFAULT NULL,
  price DECIMAL(10, 2) DEFAULT NULL,
  minutes INT(11) DEFAULT NULL,
  location_id INT(11) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create foreign key
--
ALTER TABLE training_session 
  ADD CONSTRAINT FK_training_session_city_id FOREIGN KEY (location_id)
    REFERENCES location(id) ON DELETE NO ACTION;

--
-- Create foreign key
--
ALTER TABLE training_session 
  ADD CONSTRAINT FK_training_session_training_id FOREIGN KEY (training_id)
    REFERENCES training(id) ON DELETE NO ACTION;

--
-- Create table `applicant`
--
CREATE TABLE IF NOT EXISTS applicant (
  training_session_id INT(11) DEFAULT NULL,
  user_id INT(11) DEFAULT NULL
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create foreign key
--
ALTER TABLE applicant 
  ADD CONSTRAINT FK_applicant_training_session_id FOREIGN KEY (training_session_id)
    REFERENCES training_session(id) ON DELETE NO ACTION;

--
-- Create foreign key
--
ALTER TABLE applicant 
  ADD CONSTRAINT FK_applicant_user_id FOREIGN KEY (user_id)
    REFERENCES user(id) ON DELETE NO ACTION;

--
-- Create table `tag`
--
CREATE TABLE IF NOT EXISTS tag (
  id INT(11) NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  colpur VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create table `tag-training`
--
CREATE TABLE IF NOT EXISTS `tag-training` (
  tag_id INT(11) DEFAULT NULL,
  training_id INT(11) DEFAULT NULL
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_hungarian_ci;

--
-- Create foreign key
--
ALTER TABLE `tag-training` 
  ADD CONSTRAINT `FK_tag-training_tag_id` FOREIGN KEY (tag_id)
    REFERENCES tag(id) ON DELETE NO ACTION;

--
-- Create foreign key
--
ALTER TABLE `tag-training` 
  ADD CONSTRAINT `FK_tag-training_training_id` FOREIGN KEY (training_id)
    REFERENCES training(id) ON DELETE NO ACTION;

-- 
-- Dumping data for table location
--
-- Table moveyourbody.location does not contain any data (it is empty)

-- 
-- Dumping data for table user
--
-- Table moveyourbody.user does not contain any data (it is empty)

-- 
-- Dumping data for table category
--
-- Table moveyourbody.category does not contain any data (it is empty)

-- 
-- Dumping data for table training
--
-- Table moveyourbody.training does not contain any data (it is empty)

-- 
-- Dumping data for table tag
--
-- Table moveyourbody.tag does not contain any data (it is empty)

-- 
-- Dumping data for table training_session
--
-- Table moveyourbody.training_session does not contain any data (it is empty)

-- 
-- Dumping data for table `tag-training`
--
-- Table moveyourbody.`tag-training` does not contain any data (it is empty)

-- 
-- Dumping data for table applicant
--
-- Table moveyourbody.applicant does not contain any data (it is empty)

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
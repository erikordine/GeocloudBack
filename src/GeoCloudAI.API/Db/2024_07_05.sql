-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: geocloudai
-- ------------------------------------------------------
-- Server version	8.0.33

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
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `account` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(40) NOT NULL,
  `company` varchar(40) NOT NULL,
  `employees` varchar(10) NOT NULL,
  `acessMaxAttempts` int NOT NULL,
  `validityUserPassword` int NOT NULL,
  `validityInviteUser` int NOT NULL,
  `validityInviteProject` int NOT NULL,
  `guid` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `guid_UNIQUE` (`guid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (1,'Essencis Labs','Essencis Labs','6-10',5,30,10,10,'50b97e45-526c-4a8d-bddf-2d82e2f1b669'),(2,'Jpd Tech','Jpd Tech','1-5',5,30,15,15,'d9620a2c-f477-4050-8a6c-65dd230749f2');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `company` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `typeId` int DEFAULT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Company_ibfk_1` (`accountId`),
  KEY `Company_ibfk_2` (`typeId`),
  KEY `Company_ibfk_3` (`userId`),
  CONSTRAINT `Company_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Company_ibfk_2` FOREIGN KEY (`typeId`) REFERENCES `companytype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Company_ibfk_3` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company`
--

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` VALUES (1,1,'Abc Drilling',NULL,1,NULL,NULL,1,'2024-04-18'),(2,1,'Geo New',NULL,2,NULL,NULL,1,'2024-04-18'),(3,1,'Trans Rock',NULL,3,NULL,NULL,1,'2024-04-18'),(4,2,'New Age',NULL,5,NULL,NULL,2,'2024-04-18');
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `companytype`
--

DROP TABLE IF EXISTS `companytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companytype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `CompanyType_ibfk_1` (`accountId`),
  CONSTRAINT `CompanyType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companytype`
--

LOCK TABLES `companytype` WRITE;
/*!40000 ALTER TABLE `companytype` DISABLE KEYS */;
INSERT INTO `companytype` VALUES (1,1,'Drilling',NULL),(2,1,'Geology',NULL),(3,1,'Transport',NULL),(4,2,'Drilling',NULL),(5,2,'Geology',NULL);
/*!40000 ALTER TABLE `companytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coreshed`
--

DROP TABLE IF EXISTS `coreshed`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coreshed` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `CoreShed_ibfk_1` (`accountId`),
  CONSTRAINT `CoreShed_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coreshed`
--

LOCK TABLES `coreshed` WRITE;
/*!40000 ALTER TABLE `coreshed` DISABLE KEYS */;
INSERT INTO `coreshed` VALUES (1,1,'Core Shed 1',NULL),(2,1,'Core Shed 2',NULL),(3,1,'Core Shed 3',NULL),(4,2,'Core Shed 4',NULL),(5,2,'Core Shed 5',NULL);
/*!40000 ALTER TABLE `coreshed` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `country` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(60) NOT NULL,
  `acronym2` varchar(2) NOT NULL,
  `acronym3` varchar(3) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=250 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES (1,'Afghanistan','AF','AFG'),(2,'Aland Islands','AX','ALA'),(3,'Albania','AL','ALB'),(4,'Algeria','DZ','DZA'),(5,'American Samoa','AS','ASM'),(6,'Andorra','AD','AND'),(7,'Angola','AO','AGO'),(8,'Anguilla','AI','AIA'),(9,'Antarctica','AQ','ATA'),(10,'Antigua and Barbuda','AG','ATG'),(11,'Argentina','AR','ARG'),(12,'Armenia','AM','ARM'),(13,'Aruba','AW','ABW'),(14,'Australia','AU','AUS'),(15,'Austria','AT','AUT'),(16,'Azerbaijan','AZ','AZE'),(17,'Bahamas (the)','BS','BHS'),(18,'Bahrain','BH','BHR'),(19,'Bangladesh','BD','BGD'),(20,'Barbados','BB','BRB'),(21,'Belarus','BY','BLR'),(22,'Belgium','BE','BEL'),(23,'Belize','BZ','BLZ'),(24,'Benin','BJ','BEN'),(25,'Bermuda','BM','BMU'),(26,'Bhutan','BT','BTN'),(27,'Bolivia (Plurinational State of)','BO','BOL'),(28,'Bonaire, Sint Eustatius and Saba','BQ','BES'),(29,'Bosnia and Herzegovina','BA','BIH'),(30,'Botswana','BW','BWA'),(31,'Bouvet Island','BV','BVT'),(32,'Brazil','BR','BRA'),(33,'British Indian Ocean Territory (the)','IO','IOT'),(34,'Brunei Darussalam','BN','BRN'),(35,'Bulgaria','BG','BGR'),(36,'Burkina Faso','BF','BFA'),(37,'Burundi','BI','BDI'),(38,'Cabo Verde','CV','CPV'),(39,'Cambodia','KH','KHM'),(40,'Cameroon','CM','CMR'),(41,'Canada','CA','CAN'),(42,'Cayman Islands (the)','KY','CYM'),(43,'Central African Republic (the)','CF','CAF'),(44,'Chad','TD','TCD'),(45,'Chile','CL','CHL'),(46,'China','CN','CHN'),(47,'Christmas Island','CX','CXR'),(48,'Cocos (Keeling) Islands (the)','CC','CCK'),(49,'Colombia','CO','COL'),(50,'Comoros (the)','KM','COM'),(51,'Congo (the Democratic Republic of the)','CD','COD'),(52,'Congo (the)','CG','COG'),(53,'Cook Islands (the)','CK','COK'),(54,'Costa Rica','CR','CRI'),(55,'Côte d\'Ivoire','CI','CIV'),(56,'Croatia','HR','HRV'),(57,'Cuba','CU','CUB'),(58,'Curaçao','CW','CUW'),(59,'Cyprus','CY','CYP'),(60,'Czechia','CZ','CZE'),(61,'Denmark','DK','DNK'),(62,'Djibouti','DJ','DJI'),(63,'Dominica','DM','DMA'),(64,'Dominican Republic (the)','DO','DOM'),(65,'Ecuador','EC','ECU'),(66,'Egypt','EG','EGY'),(67,'El Salvador','SV','SLV'),(68,'Equatorial Guinea','GQ','GNQ'),(69,'Eritrea','ER','ERI'),(70,'Estonia','EE','EST'),(71,'Eswatini','SZ','SWZ'),(72,'Ethiopia','ET','ETH'),(73,'Falkland Islands (the) [Malvinas]','FK','FLK'),(74,'Faroe Islands (the)','FO','FRO'),(75,'Fiji','FJ','FJI'),(76,'Finland','FI','FIN'),(77,'France','FR','FRA'),(78,'French Guiana','GF','GUF'),(79,'French Polynesia','PF','PYF'),(80,'French Southern Territories (the)','TF','ATF'),(81,'Gabon','GA','GAB'),(82,'Gambia (the)','GM','GMB'),(83,'Georgia','GE','GEO'),(84,'Germany','DE','DEU'),(85,'Ghana','GH','GHA'),(86,'Gibraltar','GI','GIB'),(87,'Greece','GR','GRC'),(88,'Greenland','GL','GRL'),(89,'Grenada','GD','GRD'),(90,'Guadeloupe','GP','GLP'),(91,'Guam','GU','GUM'),(92,'Guatemala','GT','GTM'),(93,'Guernsey','GG','GGY'),(94,'Guinea','GN','GIN'),(95,'Guinea-Bissau','GW','GNB'),(96,'Guyana','GY','GUY'),(97,'Haiti','HT','HTI'),(98,'Heard Island and McDonald Islands','HM','HMD'),(99,'Holy See (the)','VA','VAT'),(100,'Honduras','HN','HND'),(101,'Hong Kong','HK','HKG'),(102,'Hungary','HU','HUN'),(103,'Iceland','IS','ISL'),(104,'India','IN','IND'),(105,'Indonesia','ID','IDN'),(106,'Iran (Islamic Republic of)','IR','IRN'),(107,'Iraq','IQ','IRQ'),(108,'Ireland','IE','IRL'),(109,'Isle of Man','IM','IMN'),(110,'Israel','IL','ISR'),(111,'Italy','IT','ITA'),(112,'Jamaica','JM','JAM'),(113,'Japan','JP','JPN'),(114,'Jersey','JE','JEY'),(115,'Jordan','JO','JOR'),(116,'Kazakhstan','KZ','KAZ'),(117,'Kenya','KE','KEN'),(118,'Kiribati','KI','KIR'),(119,'Korea (the Democratic People\'s Republic of)','KP','PRK'),(120,'Korea (the Republic of)','KR','KOR'),(121,'Kuwait','KW','KWT'),(122,'Kyrgyzstan','KG','KGZ'),(123,'Lao People\'s Democratic Republic (the)','LA','LAO'),(124,'Latvia','LV','LVA'),(125,'Lebanon','LB','LBN'),(126,'Lesotho','LS','LSO'),(127,'Liberia','LR','LBR'),(128,'Libya','LY','LBY'),(129,'Liechtenstein','LI','LIE'),(130,'Lithuania','LT','LTU'),(131,'Luxembourg','LU','LUX'),(132,'Macao','MO','MAC'),(133,'Madagascar','MG','MDG'),(134,'Malawi','MW','MWI'),(135,'Malaysia','MY','MYS'),(136,'Maldives','MV','MDV'),(137,'Mali','ML','MLI'),(138,'Malta','MT','MLT'),(139,'Marshall Islands (the)','MH','MHL'),(140,'Martinique','MQ','MTQ'),(141,'Mauritania','MR','MRT'),(142,'Mauritius','MU','MUS'),(143,'Mayotte','YT','MYT'),(144,'Mexico','MX','MEX'),(145,'Micronesia (Federated States of)','FM','FSM'),(146,'Moldova (the Republic of)','MD','MDA'),(147,'Monaco','MC','MCO'),(148,'Mongolia','MN','MNG'),(149,'Montenegro','ME','MNE'),(150,'Montserrat','MS','MSR'),(151,'Morocco','MA','MAR'),(152,'Mozambique','MZ','MOZ'),(153,'Myanmar','MM','MMR'),(154,'Namibia','NA','NAM'),(155,'Nauru','NR','NRU'),(156,'Nepal','NP','NPL'),(157,'Netherlands (the)','NL','NLD'),(158,'New Caledonia','NC','NCL'),(159,'New Zealand','NZ','NZL'),(160,'Nicaragua','NI','NIC'),(161,'Niger (the)','NE','NER'),(162,'Nigeria','NG','NGA'),(163,'Niue','NU','NIU'),(164,'Norfolk Island','NF','NFK'),(165,'Northern Mariana Islands (the)','MP','MNP'),(166,'Norway','NO','NOR'),(167,'Oman','OM','OMN'),(168,'Pakistan','PK','PAK'),(169,'Palau','PW','PLW'),(170,'Palestine, State of','PS','PSE'),(171,'Panama','PA','PAN'),(172,'Papua New Guinea','PG','PNG'),(173,'Paraguay','PY','PRY'),(174,'Peru','PE','PER'),(175,'Philippines (the)','PH','PHL'),(176,'Pitcairn','PN','PCN'),(177,'Poland','PL','POL'),(178,'Portugal','PT','PRT'),(179,'Puerto Rico','PR','PRI'),(180,'Qatar','QA','QAT'),(181,'Republic of North Macedonia','MK','MKD'),(182,'Réunion','RE','REU'),(183,'Romania','RO','ROU'),(184,'Russian Federation (the)','RU','RUS'),(185,'Rwanda','RW','RWA'),(186,'Saint Barthélemy','BL','BLM'),(187,'Saint Helena, Ascension and Tristan da Cunha','SH','SHN'),(188,'Saint Kitts and Nevis','KN','KNA'),(189,'Saint Lucia','LC','LCA'),(190,'Saint Martin (French part)','MF','MAF'),(191,'Saint Pierre and Miquelon','PM','SPM'),(192,'Saint Vincent and the Grenadines','VC','VCT'),(193,'Samoa','WS','WSM'),(194,'San Marino','SM','SMR'),(195,'Sao Tome and Principe','ST','STP'),(196,'Saudi Arabia','SA','SAU'),(197,'Senegal','SN','SEN'),(198,'Serbia','RS','SRB'),(199,'Seychelles','SC','SYC'),(200,'Sierra Leone','SL','SLE'),(201,'Singapore','SG','SGP'),(202,'Sint Maarten (Dutch part)','SX','SXM'),(203,'Slovakia','SK','SVK'),(204,'Slovenia','SI','SVN'),(205,'Solomon Islands','SB','SLB'),(206,'Somalia','SO','SOM'),(207,'South Africa','ZA','ZAF'),(208,'South Georgia and the South Sandwich Islands','GS','SGS'),(209,'South Sudan','SS','SSD'),(210,'Spain','ES','ESP'),(211,'Sri Lanka','LK','LKA'),(212,'Sudan (the)','SD','SDN'),(213,'Suriname','SR','SUR'),(214,'Svalbard and Jan Mayen','SJ','SJM'),(215,'Sweden','SE','SWE'),(216,'Switzerland','CH','CHE'),(217,'Syrian Arab Republic','SY','SYR'),(218,'Taiwan (Province of China)','TW','TWN'),(219,'Tajikistan','TJ','TJK'),(220,'Tanzania, United Republic of','TZ','TZA'),(221,'Thailand','TH','THA'),(222,'Timor-Leste','TL','TLS'),(223,'Togo','TG','TGO'),(224,'Tokelau','TK','TKL'),(225,'Tonga','TO','TON'),(226,'Trinidad and Tobago','TT','TTO'),(227,'Tunisia','TN','TUN'),(228,'Turkey','TR','TUR'),(229,'Turkmenistan','TM','TKM'),(230,'Turks and Caicos Islands (the)','TC','TCA'),(231,'Tuvalu','TV','TUV'),(232,'Uganda','UG','UGA'),(233,'Ukraine','UA','UKR'),(234,'United Arab Emirates (the)','AE','ARE'),(235,'United Kingdom of Great Britain and Northern Ireland (the)','GB','GBR'),(236,'United States Minor Outlying Islands (the)','UM','UMI'),(237,'United States of America (the)','US','USA'),(238,'Uruguay','UY','URY'),(239,'Uzbekistan','UZ','UZB'),(240,'Vanuatu','VU','VUT'),(241,'Venezuela (Bolivarian Republic of)','VE','VEN'),(242,'Viet Nam','VN','VNM'),(243,'Virgin Islands (British)','VG','VGB'),(244,'Virgin Islands (U.S.)','VI','VIR'),(245,'Wallis and Futuna','WF','WLF'),(246,'Western Sahara','EH','ESH'),(247,'Yemen','YE','YEM'),(248,'Zambia','ZM','ZMB'),(249,'Zimbabwe','ZW','ZWE');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deposit`
--

DROP TABLE IF EXISTS `deposit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deposit` (
  `id` int NOT NULL AUTO_INCREMENT,
  `regionId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `alternativeNames` varchar(40) DEFAULT NULL,
  `state` varchar(40) DEFAULT NULL,
  `city` varchar(40) DEFAULT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(11,8) DEFAULT NULL,
  `geologicalDistrict` varchar(40) DEFAULT NULL,
  `discoveryBy` varchar(40) DEFAULT NULL,
  `discoveryYear` int DEFAULT NULL,
  `resource` int DEFAULT NULL,
  `reserve` int DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `depositTypeId` int DEFAULT NULL,
  `oreGeneticTypeId` int DEFAULT NULL,
  `oreGeneticTypeSubId` int DEFAULT NULL,
  `metalGroupId` int DEFAULT NULL,
  `metalGroupSubId` int DEFAULT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `deposit_ibfk_2` (`depositTypeId`),
  KEY `deposit_ibfk_3` (`oreGeneticTypeId`),
  KEY `deposit_ibfk_4` (`oreGeneticTypeSubId`),
  KEY `deposit_ibfk_5` (`metalGroupId`),
  KEY `deposit_ibfk_6` (`metalGroupSubId`),
  KEY `deposit_ibfk_7` (`userId`),
  KEY `deposit_ibfk_1` (`regionId`),
  CONSTRAINT `deposit_ibfk_1` FOREIGN KEY (`regionId`) REFERENCES `region` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_2` FOREIGN KEY (`depositTypeId`) REFERENCES `deposittype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_3` FOREIGN KEY (`oreGeneticTypeId`) REFERENCES `oregenetictype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_4` FOREIGN KEY (`oreGeneticTypeSubId`) REFERENCES `oregenetictypesub` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_5` FOREIGN KEY (`metalGroupId`) REFERENCES `metalgroup` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_6` FOREIGN KEY (`metalGroupSubId`) REFERENCES `metalgroupsub` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `deposit_ibfk_7` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deposit`
--

LOCK TABLES `deposit` WRITE;
/*!40000 ALTER TABLE `deposit` DISABLE KEYS */;
INSERT INTO `deposit` VALUES (1,1,'Deposit 1',NULL,'Minas Gerais','São José do Jacaré',-18.87307236,-43.06571666,NULL,NULL,NULL,NULL,NULL,NULL,1,1,1,1,1,'jpg','jpg',1,'2024-08-24'),(2,1,'Deposit 2',NULL,'MInas Gerais','Tombadouro',-18.65462146,-43.76814533,NULL,NULL,NULL,NULL,NULL,NULL,2,3,NULL,NULL,NULL,'jpg','jpg',2,'2024-08-24'),(3,2,'Deposit 3',NULL,NULL,NULL,-6.82613080,-50.09100651,NULL,NULL,NULL,NULL,NULL,NULL,2,NULL,NULL,NULL,NULL,'jpg','jpg',2,'2024-08-24'),(4,3,'Deposit 4',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-08-24');
/*!40000 ALTER TABLE `deposit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deposittype`
--

DROP TABLE IF EXISTS `deposittype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deposittype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `DepositType_ibfk_1` (`accountId`),
  CONSTRAINT `DepositType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deposittype`
--

LOCK TABLES `deposittype` WRITE;
/*!40000 ALTER TABLE `deposittype` DISABLE KEYS */;
INSERT INTO `deposittype` VALUES (1,1,'Metallogenic',NULL),(2,1,'Diamond',NULL),(3,1,'Coal',NULL),(4,2,'General',NULL);
/*!40000 ALTER TABLE `deposittype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillbox`
--

DROP TABLE IF EXISTS `drillbox`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillbox` (
  `id` int NOT NULL AUTO_INCREMENT,
  `drillHoleId` int NOT NULL,
  `number` int NOT NULL,
  `amountCores` int DEFAULT NULL,
  `code` varchar(40) DEFAULT NULL,
  `uuid` varchar(50) DEFAULT NULL,
  `startDepth` decimal(10,0) DEFAULT NULL,
  `endDepth` decimal(10,0) DEFAULT NULL,
  `description` varchar(40) DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `typeId` int DEFAULT NULL,
  `statusId` int DEFAULT NULL,
  `materialId` int DEFAULT NULL,
  `coreShedId` int DEFAULT NULL,
  `shelves` varchar(40) DEFAULT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `drillbox_ibfk_2` (`typeId`),
  KEY `drillbox_ibfk_3` (`statusId`),
  KEY `drillbox_ibfk_4` (`materialId`),
  KEY `drillbox_ibfk_5` (`coreShedId`),
  KEY `drillbox_ibfk_6` (`userId`),
  KEY `drillbox_ibfk_1` (`drillHoleId`),
  CONSTRAINT `drillbox_ibfk_1` FOREIGN KEY (`drillHoleId`) REFERENCES `drillhole` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillbox_ibfk_2` FOREIGN KEY (`typeId`) REFERENCES `drillboxtype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillbox_ibfk_3` FOREIGN KEY (`statusId`) REFERENCES `drillboxstatus` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillbox_ibfk_4` FOREIGN KEY (`materialId`) REFERENCES `drillboxmaterial` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillbox_ibfk_5` FOREIGN KEY (`coreShedId`) REFERENCES `coreshed` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillbox_ibfk_6` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillbox`
--

LOCK TABLES `drillbox` WRITE;
/*!40000 ALTER TABLE `drillbox` DISABLE KEYS */;
INSERT INTO `drillbox` VALUES (1,1,1,4,'Drill Box 1','teste_uuid',0,4,'string','string',1,1,1,1,'shelve 001.10','jpg',1,'2024-04-26'),(2,2,1,4,'Drill Box 2','teste_uuid',0,4,'string','string',1,1,1,1,'shelve 001.11','jpg',1,'2024-04-26'),(3,3,1,4,'Drill Box 3','teste_uuid',0,4,'string','string',1,1,1,1,'shelve 001.12','jpg',1,'2024-04-26'),(4,3,2,NULL,'Drill Box 4',NULL,4,8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg',1,'2024-03-26'),(5,5,1,NULL,'Drill Box 5',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg',1,'2024-03-26'),(6,5,2,NULL,'Drill Box 6',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg',2,'2024-03-26'),(7,9,1,NULL,'Drill Box 7',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-03-26'),(8,9,2,NULL,'Drill Box 8',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-03-26'),(9,9,3,NULL,'Drill Box 9',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-03-26'),(10,9,4,NULL,'Drill Box 10',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-03-26'),(11,6,1,NULL,'Drill Box 11',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-05-03'),(12,10,1,NULL,'Drill Box 12',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg',2,'2024-05-03'),(13,10,2,NULL,'Drill Box 13',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg',2,'2024-05-03');
/*!40000 ALTER TABLE `drillbox` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillboxactivitytype`
--

DROP TABLE IF EXISTS `drillboxactivitytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillboxactivitytype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillBoxActivityType_ibfk_1` (`accountId`),
  CONSTRAINT `DrillBoxActivityType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillboxactivitytype`
--

LOCK TABLES `drillboxactivitytype` WRITE;
/*!40000 ALTER TABLE `drillboxactivitytype` DISABLE KEYS */;
INSERT INTO `drillboxactivitytype` VALUES (1,1,'Drill Box Activity Type 1',NULL),(2,1,'Drill Box Activity Type 2',NULL),(3,1,'Drill Box Activity Type 3',NULL),(4,2,'Drill Box Activity Type 4',NULL),(5,2,'Drill Box Activity Type 5',NULL);
/*!40000 ALTER TABLE `drillboxactivitytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillboxmaterial`
--

DROP TABLE IF EXISTS `drillboxmaterial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillboxmaterial` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillBoxMaterial_ibfk_1` (`accountId`),
  CONSTRAINT `DrillBoxMaterial_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillboxmaterial`
--

LOCK TABLES `drillboxmaterial` WRITE;
/*!40000 ALTER TABLE `drillboxmaterial` DISABLE KEYS */;
INSERT INTO `drillboxmaterial` VALUES (1,1,'Plastic'),(2,1,'Wood'),(3,1,'Waxed Cardboard'),(4,2,'Plastic'),(5,2,'Wood');
/*!40000 ALTER TABLE `drillboxmaterial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillboxstatus`
--

DROP TABLE IF EXISTS `drillboxstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillboxstatus` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillBoxStatus_ibfk_1` (`accountId`),
  CONSTRAINT `DrillBoxStatus_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillboxstatus`
--

LOCK TABLES `drillboxstatus` WRITE;
/*!40000 ALTER TABLE `drillboxstatus` DISABLE KEYS */;
INSERT INTO `drillboxstatus` VALUES (1,1,'Drill Box Status 1',NULL),(2,1,'Drill Box Status 2',NULL),(3,1,'Drill Box Status 3',NULL),(4,2,'Drill Box Status 4',NULL),(5,2,'Drill Box Status 5',NULL);
/*!40000 ALTER TABLE `drillboxstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillboxtype`
--

DROP TABLE IF EXISTS `drillboxtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillboxtype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillBoxType_ibfk_1` (`accountId`),
  CONSTRAINT `DrillBoxType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillboxtype`
--

LOCK TABLES `drillboxtype` WRITE;
/*!40000 ALTER TABLE `drillboxtype` DISABLE KEYS */;
INSERT INTO `drillboxtype` VALUES (1,1,'BQ'),(2,1,'NQ'),(3,1,'HQ'),(4,2,'PQ'),(5,2,'BQ');
/*!40000 ALTER TABLE `drillboxtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillhole`
--

DROP TABLE IF EXISTS `drillhole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillhole` (
  `id` int NOT NULL AUTO_INCREMENT,
  `regionId` int NOT NULL,
  `depositId` int DEFAULT NULL,
  `mineId` int DEFAULT NULL,
  `mineAreaId` int DEFAULT NULL,
  `name` varchar(40) NOT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(11,8) DEFAULT NULL,
  `elevation` decimal(10,0) DEFAULT NULL,
  `length` decimal(10,0) DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `typeId` int DEFAULT NULL,
  `drillingTypeId` int DEFAULT NULL,
  `contractorId` int DEFAULT NULL,
  `drillerId` int DEFAULT NULL,
  `startDate` date DEFAULT NULL,
  `endDate` date DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `drillhole_ibfk_1` (`regionId`),
  KEY `drillhole_ibfk_2` (`depositId`),
  KEY `drillhole_ibfk_3` (`mineId`),
  KEY `drillhole_ibfk_4` (`mineAreaId`),
  KEY `drillhole_ibfk_5` (`typeId`),
  KEY `drillhole_ibfk_6` (`drillingTypeId`),
  KEY `drillhole_ibfk_7` (`contractorId`),
  KEY `drillhole_ibfk_8` (`drillerId`),
  KEY `drillhole_ibfk_9` (`userId`),
  CONSTRAINT `drillhole_ibfk_1` FOREIGN KEY (`regionId`) REFERENCES `region` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_2` FOREIGN KEY (`depositId`) REFERENCES `deposit` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_3` FOREIGN KEY (`mineId`) REFERENCES `mine` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_4` FOREIGN KEY (`mineAreaId`) REFERENCES `minearea` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_5` FOREIGN KEY (`typeId`) REFERENCES `drillholetype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_6` FOREIGN KEY (`drillingTypeId`) REFERENCES `drillingtype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_7` FOREIGN KEY (`contractorId`) REFERENCES `company` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_8` FOREIGN KEY (`drillerId`) REFERENCES `company` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `drillhole_ibfk_9` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillhole`
--

LOCK TABLES `drillhole` WRITE;
/*!40000 ALTER TABLE `drillhole` DISABLE KEYS */;
INSERT INTO `drillhole` VALUES (1,2,NULL,NULL,NULL,'Drill Hole 1',-6.32408538,-50.04710382,125,180,'test add',1,1,1,2,'2024-05-21','2024-05-28',1,'2024-04-26'),(2,2,3,NULL,NULL,'Drill Hole 2',-6.88612350,-50.30503214,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2024-02-01','2024-03-01',1,'2024-04-26'),(3,2,3,4,NULL,'Drill Hole 3',-6.97337206,-49.83856603,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'2024-04-26'),(4,2,3,4,5,'Drill Hole 4',-7.21867089,-49.43795396,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'2024-04-26'),(5,2,3,4,5,'Drill Hole 5',-7.11511623,-49.33368507,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,'2024-04-26'),(6,3,NULL,NULL,NULL,'Drill Hole 6',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-26'),(7,3,4,5,NULL,'Drill Hole 7',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-26'),(8,3,4,5,6,'Drill Hole 8',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-26'),(9,3,4,5,6,'Drill Hole 9',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-26'),(10,1,2,3,3,'Drill Hole 10',-18.40983083,-43.59254015,NULL,NULL,NULL,NULL,NULL,NULL,1,'2024-03-05','2024-05-30',2,'2024-04-26'),(11,1,2,3,3,'Drill Hole 11',-18.44631120,-43.43888073,NULL,NULL,NULL,NULL,NULL,NULL,2,'2024-02-06',NULL,2,'2024-04-26');
/*!40000 ALTER TABLE `drillhole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillholetype`
--

DROP TABLE IF EXISTS `drillholetype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillholetype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  `diameter` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillHoleType_ibfk_1` (`accountId`),
  CONSTRAINT `DrillHoleType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillholetype`
--

LOCK TABLES `drillholetype` WRITE;
/*!40000 ALTER TABLE `drillholetype` DISABLE KEYS */;
INSERT INTO `drillholetype` VALUES (1,1,'BQ',48),(2,1,'NQ',55),(3,1,'HQ',69),(4,1,'PQ',92),(5,2,'BQ',48);
/*!40000 ALTER TABLE `drillholetype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drillingtype`
--

DROP TABLE IF EXISTS `drillingtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drillingtype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `DrillingType_ibfk_1` (`accountId`),
  CONSTRAINT `DrillingType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drillingtype`
--

LOCK TABLES `drillingtype` WRITE;
/*!40000 ALTER TABLE `drillingtype` DISABLE KEYS */;
INSERT INTO `drillingtype` VALUES (1,1,'Auger Drilling',NULL),(2,1,'Rotary Air Blasting',NULL),(3,1,'Aircore',NULL),(4,1,'Reverse Circulation Drilling',NULL),(5,1,'Diamond Core Drilling',NULL),(6,1,'Blast Hole Drilling',NULL),(7,2,'Auger Drilling',NULL),(8,2,'Rotary Air Blasting',NULL);
/*!40000 ALTER TABLE `drillingtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `id` int NOT NULL AUTO_INCREMENT,
  `companyId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `roleId` int DEFAULT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Employee_ibfk_1` (`companyId`),
  KEY `Employee_ibfk_2` (`roleId`),
  KEY `Employee_ibfk_3` (`userId`),
  CONSTRAINT `Employee_ibfk_1` FOREIGN KEY (`companyId`) REFERENCES `company` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Employee_ibfk_2` FOREIGN KEY (`roleId`) REFERENCES `employeerole` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Employee_ibfk_3` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,1,'Employee 1',1,NULL,1,'2024-04-19'),(2,1,'Employee 2',2,NULL,1,'2024-04-19'),(3,2,'Employee 3',4,NULL,1,'2024-04-19'),(4,2,'Employee 4',NULL,NULL,1,'2024-04-19'),(5,3,'Employee 5',NULL,NULL,1,'2024-04-19'),(6,4,'Employee 6',5,NULL,2,'2024-04-19');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employeerole`
--

DROP TABLE IF EXISTS `employeerole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employeerole` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `EmployeeRole_ibfk_1` (`accountId`),
  CONSTRAINT `EmployeeRole_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employeerole`
--

LOCK TABLES `employeerole` WRITE;
/*!40000 ALTER TABLE `employeerole` DISABLE KEYS */;
INSERT INTO `employeerole` VALUES (1,1,'Administrator',NULL),(2,1,'Geologist',NULL),(3,1,'Chemical',NULL),(4,1,'Driver',NULL),(5,2,'Administrator',NULL),(6,2,'Geologist',NULL);
/*!40000 ALTER TABLE `employeerole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `functionality`
--

DROP TABLE IF EXISTS `functionality`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `functionality` (
  `id` int NOT NULL AUTO_INCREMENT,
  `typeId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Functionality_ibfk_1` (`typeId`),
  CONSTRAINT `Functionality_ibfk_1` FOREIGN KEY (`typeId`) REFERENCES `functionalitytype` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `functionality`
--

LOCK TABLES `functionality` WRITE;
/*!40000 ALTER TABLE `functionality` DISABLE KEYS */;
INSERT INTO `functionality` VALUES (1,1,'Account Add'),(2,1,'Account Update'),(3,1,'Account List'),(4,1,'Account Delete'),(5,2,'Region Add'),(6,2,'Region Update'),(7,2,'Region Delete'),(8,2,'Region List');
/*!40000 ALTER TABLE `functionality` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `functionalitytype`
--

DROP TABLE IF EXISTS `functionalitytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `functionalitytype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `functionalitytype`
--

LOCK TABLES `functionalitytype` WRITE;
/*!40000 ALTER TABLE `functionalitytype` DISABLE KEYS */;
INSERT INTO `functionalitytype` VALUES (1,'Administrative'),(2,'Operational');
/*!40000 ALTER TABLE `functionalitytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `metalgroup`
--

DROP TABLE IF EXISTS `metalgroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `metalgroup` (
  `id` int NOT NULL AUTO_INCREMENT,
  `oreGeneticTypeId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `MetalGroup_ibfk_1_idx` (`oreGeneticTypeId`),
  CONSTRAINT `MetalGroup_ibfk_1` FOREIGN KEY (`oreGeneticTypeId`) REFERENCES `oregenetictype` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `metalgroup`
--

LOCK TABLES `metalgroup` WRITE;
/*!40000 ALTER TABLE `metalgroup` DISABLE KEYS */;
INSERT INTO `metalgroup` VALUES (1,1,'Metal Group 1'),(2,1,'Metal Group 2'),(3,3,'Metal Group 3'),(4,4,'Metal Group 4'),(5,5,'Metal Group 5');
/*!40000 ALTER TABLE `metalgroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `metalgroupsub`
--

DROP TABLE IF EXISTS `metalgroupsub`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `metalgroupsub` (
  `id` int NOT NULL AUTO_INCREMENT,
  `metalgroupId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `metalgroupSub_ibfk_1` (`metalgroupId`),
  CONSTRAINT `metalgroupSub_ibfk_1` FOREIGN KEY (`metalgroupId`) REFERENCES `metalgroup` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `metalgroupsub`
--

LOCK TABLES `metalgroupsub` WRITE;
/*!40000 ALTER TABLE `metalgroupsub` DISABLE KEYS */;
INSERT INTO `metalgroupsub` VALUES (1,1,'Metal Group Sub 1'),(2,1,'Metal Group Sub 2'),(3,2,'Metal Group Sub 3'),(4,5,'Metal Group Sub 4');
/*!40000 ALTER TABLE `metalgroupsub` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mine`
--

DROP TABLE IF EXISTS `mine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mine` (
  `id` int NOT NULL AUTO_INCREMENT,
  `depositId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(11,8) DEFAULT NULL,
  `startYear` int DEFAULT NULL,
  `endYear` int DEFAULT NULL,
  `resource` int DEFAULT NULL,
  `reserve` int DEFAULT NULL,
  `oreMined` int DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `sizeId` int DEFAULT NULL,
  `statusId` int DEFAULT NULL,
  `statusPreviousId` int DEFAULT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `mine_ibfk_1` (`depositId`),
  KEY `mine_ibfk_2` (`sizeId`),
  KEY `mine_ibfk_3` (`statusId`),
  KEY `mine_ibfk_4` (`statusPreviousId`),
  KEY `mine_ibfk_5` (`userId`),
  CONSTRAINT `mine_ibfk_1` FOREIGN KEY (`depositId`) REFERENCES `deposit` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mine_ibfk_2` FOREIGN KEY (`sizeId`) REFERENCES `minesize` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mine_ibfk_3` FOREIGN KEY (`statusId`) REFERENCES `minestatus` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mine_ibfk_4` FOREIGN KEY (`statusPreviousId`) REFERENCES `minestatus` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mine_ibfk_5` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mine`
--

LOCK TABLES `mine` WRITE;
/*!40000 ALTER TABLE `mine` DISABLE KEYS */;
INSERT INTO `mine` VALUES (1,1,'Mine 1',-18.99257991,-42.99432301,2020,2022,NULL,NULL,NULL,NULL,1,1,NULL,'jpg','jpg',1,'2024-04-08'),(2,2,'Mine 2',-18.77948520,-43.88624997,2010,NULL,NULL,NULL,NULL,NULL,1,2,NULL,'jpg','jpg',1,'2024-04-08'),(3,2,'Mine 3',-18.54529026,-43.67730742,2021,2024,NULL,NULL,NULL,NULL,2,1,NULL,'jpg','jpg',2,'2024-04-08'),(4,3,'Mine 4',-6.83158499,-49.74527281,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg','jpg',2,'2024-04-08'),(5,4,'Mine 5',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-08'),(6,4,'Mine 6',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-08');
/*!40000 ALTER TABLE `mine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `minearea`
--

DROP TABLE IF EXISTS `minearea`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `minearea` (
  `id` int NOT NULL AUTO_INCREMENT,
  `mineId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(11,8) DEFAULT NULL,
  `startYear` int DEFAULT NULL,
  `endYear` int DEFAULT NULL,
  `resource` int DEFAULT NULL,
  `reserve` int DEFAULT NULL,
  `oreMined` int DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `typeId` int DEFAULT NULL,
  `statusId` int DEFAULT NULL,
  `shapeId` int DEFAULT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `mineArea_ibfk_1` (`mineId`),
  KEY `mineArea_ibfk_2` (`typeId`),
  KEY `mineArea_ibfk_3` (`statusId`),
  KEY `mineArea_ibfk_4` (`shapeId`),
  KEY `mineArea_ibfk_5` (`userId`),
  CONSTRAINT `mineArea_ibfk_1` FOREIGN KEY (`mineId`) REFERENCES `mine` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mineArea_ibfk_2` FOREIGN KEY (`typeId`) REFERENCES `mineareatype` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mineArea_ibfk_3` FOREIGN KEY (`statusId`) REFERENCES `mineareastatus` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mineArea_ibfk_4` FOREIGN KEY (`shapeId`) REFERENCES `mineareashape` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mineArea_ibfk_5` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `minearea`
--

LOCK TABLES `minearea` WRITE;
/*!40000 ALTER TABLE `minearea` DISABLE KEYS */;
INSERT INTO `minearea` VALUES (1,1,'Mine Area 1',-19.09642985,-43.10958515,NULL,NULL,NULL,NULL,NULL,NULL,1,1,1,'jpg','jpg',1,'2024-04-08'),(2,2,'Mine Area 2',-18.80028685,-44.03705491,NULL,NULL,NULL,NULL,NULL,NULL,1,2,2,'jpg','jpg',1,'2024-04-08'),(3,3,'Mine Area 3',-18.48799356,-43.54863746,NULL,NULL,NULL,NULL,NULL,NULL,2,2,2,'jpg','jpg',2,'2024-04-08'),(4,4,'Mine Area 4',-6.66247629,-49.48185665,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg','jpg',2,'2024-04-08'),(5,4,'Mine Area 5',-7.10421438,-49.44892963,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'jpg','jpg',1,'2024-04-08'),(6,5,'Mine Area 6',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-08'),(7,6,'Mine Area 7',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-08'),(8,6,'Mine Area 8',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-04-08');
/*!40000 ALTER TABLE `minearea` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mineareashape`
--

DROP TABLE IF EXISTS `mineareashape`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mineareashape` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `mineareashape_ibfk_1` (`accountId`),
  CONSTRAINT `mineareashape_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mineareashape`
--

LOCK TABLES `mineareashape` WRITE;
/*!40000 ALTER TABLE `mineareashape` DISABLE KEYS */;
INSERT INTO `mineareashape` VALUES (1,1,'Mine Area Shape 1',NULL),(2,1,'Mine Area Shape 2',NULL),(3,2,'Mine Area Shape 3',NULL);
/*!40000 ALTER TABLE `mineareashape` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mineareastatus`
--

DROP TABLE IF EXISTS `mineareastatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mineareastatus` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `mineareastatus_ibfk_1` (`accountId`),
  CONSTRAINT `mineareastatus_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mineareastatus`
--

LOCK TABLES `mineareastatus` WRITE;
/*!40000 ALTER TABLE `mineareastatus` DISABLE KEYS */;
INSERT INTO `mineareastatus` VALUES (1,1,'Mine Area Status 1',NULL),(2,1,'Mine Area Status 2',NULL),(3,2,'Mine Area Status 3',NULL);
/*!40000 ALTER TABLE `mineareastatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mineareatype`
--

DROP TABLE IF EXISTS `mineareatype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mineareatype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `mineareatype_ibfk_1` (`accountId`),
  CONSTRAINT `mineareatype_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mineareatype`
--

LOCK TABLES `mineareatype` WRITE;
/*!40000 ALTER TABLE `mineareatype` DISABLE KEYS */;
INSERT INTO `mineareatype` VALUES (1,1,'Mine Area Type 1',NULL),(2,1,'Mine Area Type 2',NULL),(3,2,'Mine Area Type 3',NULL);
/*!40000 ALTER TABLE `mineareatype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `minesize`
--

DROP TABLE IF EXISTS `minesize`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `minesize` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `minesize_ibfk_1` (`accountId`),
  CONSTRAINT `minesize_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `minesize`
--

LOCK TABLES `minesize` WRITE;
/*!40000 ALTER TABLE `minesize` DISABLE KEYS */;
INSERT INTO `minesize` VALUES (1,1,'Mine Size 1',NULL),(2,1,'Mine Size 2',NULL),(3,2,'Mine Size 3',NULL);
/*!40000 ALTER TABLE `minesize` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `minestatus`
--

DROP TABLE IF EXISTS `minestatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `minestatus` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `minestatus_ibfk_1` (`accountId`),
  CONSTRAINT `minestatus_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `minestatus`
--

LOCK TABLES `minestatus` WRITE;
/*!40000 ALTER TABLE `minestatus` DISABLE KEYS */;
INSERT INTO `minestatus` VALUES (1,1,'Mine Status 1',NULL),(2,1,'Mine Status 2',NULL),(3,2,'Mine Status 3',NULL);
/*!40000 ALTER TABLE `minestatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `oregenetictype`
--

DROP TABLE IF EXISTS `oregenetictype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `oregenetictype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `depositTypeId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `OreGeneticType_ibfk_1` (`depositTypeId`),
  CONSTRAINT `OreGeneticType_ibfk_1` FOREIGN KEY (`depositTypeId`) REFERENCES `deposittype` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oregenetictype`
--

LOCK TABLES `oregenetictype` WRITE;
/*!40000 ALTER TABLE `oregenetictype` DISABLE KEYS */;
INSERT INTO `oregenetictype` VALUES (1,1,'Ore Genetic Type 1'),(2,1,'Ore Genetic Type 2'),(3,2,'Ore Genetic Type 3'),(4,3,'Ore Genetic Type 4'),(5,4,'Ore Genetic Type 5');
/*!40000 ALTER TABLE `oregenetictype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `oregenetictypesub`
--

DROP TABLE IF EXISTS `oregenetictypesub`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `oregenetictypesub` (
  `id` int NOT NULL AUTO_INCREMENT,
  `oreGeneticTypeId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `OreGeneticTypeSub_ibfk_1` (`oreGeneticTypeId`),
  CONSTRAINT `OreGeneticTypeSub_ibfk_1` FOREIGN KEY (`oreGeneticTypeId`) REFERENCES `oregenetictype` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oregenetictypesub`
--

LOCK TABLES `oregenetictypesub` WRITE;
/*!40000 ALTER TABLE `oregenetictypesub` DISABLE KEYS */;
INSERT INTO `oregenetictypesub` VALUES (1,1,'Ore Genetic Type Sub 1'),(2,2,'Ore Genetic Type Sub 2'),(3,2,'Ore Genetic Type Sub 3'),(4,3,'Ore Genetic Type Sub 4'),(5,4,'Ore Genetic Type Sub 5'),(6,5,'Ore Genetic Type Sub 6');
/*!40000 ALTER TABLE `oregenetictypesub` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profile`
--

DROP TABLE IF EXISTS `profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `profile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `profile_ibfk_1` (`accountId`),
  CONSTRAINT `profile_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profile`
--

LOCK TABLES `profile` WRITE;
/*!40000 ALTER TABLE `profile` DISABLE KEYS */;
INSERT INTO `profile` VALUES (1,1,'Administrator',NULL),(2,1,'Geologist',NULL),(3,1,'Chemical',NULL),(4,2,'Administrator',NULL);
/*!40000 ALTER TABLE `profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profilerole`
--

DROP TABLE IF EXISTS `profilerole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `profilerole` (
  `profileId` int NOT NULL,
  `roleId` int NOT NULL,
  PRIMARY KEY (`profileId`,`roleId`),
  KEY `profileRole_ibfk_1` (`profileId`),
  KEY `profileRole_ibfk_2` (`roleId`),
  CONSTRAINT `profileRole_ibfk_1` FOREIGN KEY (`profileId`) REFERENCES `profile` (`id`),
  CONSTRAINT `profileRole_ibfk_2` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profilerole`
--

LOCK TABLES `profilerole` WRITE;
/*!40000 ALTER TABLE `profilerole` DISABLE KEYS */;
INSERT INTO `profilerole` VALUES (1,2),(1,3),(2,1),(2,4);
/*!40000 ALTER TABLE `profilerole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `project` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `startDate` date DEFAULT NULL,
  `endDate` date DEFAULT NULL,
  `summary` varchar(40) DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `typeId` int DEFAULT NULL,
  `statusId` int DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `project_ibfk_1` (`accountId`),
  KEY `project_ibfk_4` (`userId`),
  KEY `project_ibfk_2` (`typeId`),
  KEY `project_ibfk_3` (`statusId`),
  CONSTRAINT `project_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`),
  CONSTRAINT `project_ibfk_2` FOREIGN KEY (`typeId`) REFERENCES `projecttype` (`id`),
  CONSTRAINT `project_ibfk_3` FOREIGN KEY (`statusId`) REFERENCES `projectstatus` (`id`),
  CONSTRAINT `project_ibfk_4` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project`
--

LOCK TABLES `project` WRITE;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` VALUES (1,1,'Project 1','2024-01-01','2024-01-31','test summary','test comments',1,1,1,'2024-01-01'),(2,1,'Project 2',NULL,NULL,NULL,NULL,NULL,NULL,1,'2024-01-10'),(3,1,'Project 3',NULL,NULL,NULL,NULL,NULL,NULL,2,'2024-01-12'),(4,2,'Project 4',NULL,NULL,NULL,NULL,NULL,NULL,3,'2024-01-21');
/*!40000 ALTER TABLE `project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `projectstatus`
--

DROP TABLE IF EXISTS `projectstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `projectstatus` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `ProjectStatus_ibfk_1_idx` (`accountId`),
  CONSTRAINT `ProjectStatus_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projectstatus`
--

LOCK TABLES `projectstatus` WRITE;
/*!40000 ALTER TABLE `projectstatus` DISABLE KEYS */;
INSERT INTO `projectstatus` VALUES (1,1,'Created',NULL),(2,1,'Started',NULL),(3,1,'Paused',NULL),(4,1,'Finished',NULL);
/*!40000 ALTER TABLE `projectstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `projecttype`
--

DROP TABLE IF EXISTS `projecttype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `projecttype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `ProjectType_ibfk_1` (`accountId`),
  CONSTRAINT `ProjectType_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projecttype`
--

LOCK TABLES `projecttype` WRITE;
/*!40000 ALTER TABLE `projecttype` DISABLE KEYS */;
INSERT INTO `projecttype` VALUES (1,1,'Analysis',NULL),(2,1,'Extraction',NULL),(3,1,'Laboratory',NULL),(4,2,'Standard',NULL);
/*!40000 ALTER TABLE `projecttype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `region`
--

DROP TABLE IF EXISTS `region`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `region` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `countryId` int NOT NULL,
  `state` varchar(40) DEFAULT NULL,
  `city` varchar(40) DEFAULT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(11,8) DEFAULT NULL,
  `comments` varchar(40) DEFAULT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `userId` int NOT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `region_ibfk_1` (`accountId`),
  KEY `region_ibfk_2` (`countryId`),
  KEY `region_ibfk_3` (`userId`),
  CONSTRAINT `region_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `region_ibfk_2` FOREIGN KEY (`countryId`) REFERENCES `country` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `region_ibfk_3` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `region`
--

LOCK TABLES `region` WRITE;
/*!40000 ALTER TABLE `region` DISABLE KEYS */;
INSERT INTO `region` VALUES (1,1,'Region 1',32,'Minas Gerais','Alvorada de Minas',-18.73373700,-43.36455400,'Volcanic region','jpg','jpg',1,'2024-04-08'),(2,1,'Region 2',32,'Pará','Canaã dos Carajás',-6.52062100,-49.91759900,NULL,'jpg','jpeg',1,'2024-04-08'),(3,2,'Region 3',11,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,'2024-04-08');
/*!40000 ALTER TABLE `region` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id` int NOT NULL AUTO_INCREMENT,
  `accountId` int NOT NULL,
  `name` varchar(40) NOT NULL,
  `imgType` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `Role_ibfk_1` (`accountId`),
  CONSTRAINT `Role_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `account` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,1,'Role 1',NULL),(2,1,'Role 2',NULL),(3,1,'Role 3',NULL),(4,2,'Role 4',NULL),(5,2,'Role 5',NULL);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit`
--

DROP TABLE IF EXISTS `unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `unit` (
  `id` int NOT NULL AUTO_INCREMENT,
  `typeId` int NOT NULL,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Unit_ibfk_1` (`typeId`),
  CONSTRAINT `Unit_ibfk_1` FOREIGN KEY (`typeId`) REFERENCES `unittype` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit`
--

LOCK TABLES `unit` WRITE;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` VALUES (1,1,'km'),(2,1,'m'),(3,1,'cm'),(4,1,'mm'),(5,2,'kg'),(6,2,'g'),(7,2,'mg'),(8,3,'h'),(9,3,'min'),(10,3,'sec');
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unittype`
--

DROP TABLE IF EXISTS `unittype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `unittype` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unittype`
--

LOCK TABLES `unittype` WRITE;
/*!40000 ALTER TABLE `unittype` DISABLE KEYS */;
INSERT INTO `unittype` VALUES (1,'Length'),(2,'Massiness'),(3,'Time'),(4,'Temperature'),(5,'Electric Current'),(6,'Light Intensity');
/*!40000 ALTER TABLE `unittype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `profileId` int NOT NULL,
  `firstName` varchar(40) NOT NULL,
  `lastName` varchar(40) NOT NULL,
  `phone` varchar(40) DEFAULT NULL,
  `email` varchar(60) NOT NULL,
  `password` varchar(40) NOT NULL,
  `countryId` int NOT NULL,
  `state` varchar(40) NOT NULL,
  `city` varchar(40) NOT NULL,
  `access` date NOT NULL,
  `attempts` int NOT NULL,
  `blocked` tinyint(1) NOT NULL,
  `imgTypeProfile` varchar(4) DEFAULT NULL,
  `imgTypeCover` varchar(4) DEFAULT NULL,
  `register` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user_ibfk_1_idx` (`profileId`),
  KEY `user_ibfk_2_idx` (`countryId`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`profileId`) REFERENCES `profile` (`id`),
  CONSTRAINT `user_ibfk_2` FOREIGN KEY (`countryId`) REFERENCES `country` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,1,'Luiz','DAmore','+55 11 98889-4623','luiz@luiz','12345678',32,'SP','S.Bernado do Campo','2024-02-22',0,0,'jpg','jpg','2024-02-22'),(2,2,'João','Fiori','+55 11 42223333','joao@joao','12345678',32,'SP','São Paulo','2024-02-27',0,0,NULL,NULL,'2024-02-27'),(3,4,'João Pedro','DAmore','+55 11 98888-7777','jpd@jpd','12345678',32,'SP','S.Bernado do Campo','2024-02-27',0,0,NULL,NULL,'2024-02-27');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-05 10:43:03

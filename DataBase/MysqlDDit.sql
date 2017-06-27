/*
SQLyog Ultimate v12.08 (64 bit)
MySQL - 5.7.17-log : Database - ddit
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`ddit` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `ddit`;

/*Table structure for table `button` */

DROP TABLE IF EXISTS `button`;

CREATE TABLE `button` (
  `BUTTON_ID` int(11) NOT NULL AUTO_INCREMENT,
  `BUTTON_OPID` longtext,
  `BUTTON_NAME` longtext,
  `BUTTON_OPERATION` longtext,
  `BUTTON_MAGIC` longtext,
  `BUTTON_DESCRIBE` longtext,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`BUTTON_ID`),
  UNIQUE KEY `BUTTON_ID` (`BUTTON_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `button` */

/*Table structure for table `dictionary` */

DROP TABLE IF EXISTS `dictionary`;

CREATE TABLE `dictionary` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DICCATEGORYID` int(11) NOT NULL,
  `DICVALUE` longtext,
  `ENABLED` tinyint(1) NOT NULL,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `DICCATEGORYID` (`DICCATEGORYID`),
  CONSTRAINT `Dictionary_DicCategory` FOREIGN KEY (`DICCATEGORYID`) REFERENCES `dictionarycategory` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `dictionary` */

/*Table structure for table `dictionarycategory` */

DROP TABLE IF EXISTS `dictionarycategory`;

CREATE TABLE `dictionarycategory` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CATEGORY` longtext,
  `ENABLED` tinyint(1) NOT NULL,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `dictionarycategory` */

/*Table structure for table `loginlog` */

DROP TABLE IF EXISTS `loginlog`;

CREATE TABLE `loginlog` (
  `LOGIN_ID` int(11) NOT NULL AUTO_INCREMENT,
  `LOGIN_NAME` longtext,
  `LOGIN_NICKER` longtext,
  `LOGIN_IP` longtext,
  `LOGIN_TIME` datetime NOT NULL,
  PRIMARY KEY (`LOGIN_ID`),
  UNIQUE KEY `LOGIN_ID` (`LOGIN_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `loginlog` */

insert  into `loginlog`(`LOGIN_ID`,`LOGIN_NAME`,`LOGIN_NICKER`,`LOGIN_IP`,`LOGIN_TIME`) values (1,'admin','管理员','::1','2017-06-27 16:02:31');

/*Table structure for table `menu` */

DROP TABLE IF EXISTS `menu`;

CREATE TABLE `menu` (
  `MENU_ID` int(11) NOT NULL AUTO_INCREMENT,
  `MENU_NAME` longtext,
  `MENU_URL` longtext,
  `MENU_PARENTID` int(11) DEFAULT NULL,
  `MENU_ORDER` int(11) NOT NULL,
  `MENU_ICON` longtext,
  `ISVISIBLE` int(11) NOT NULL,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`MENU_ID`),
  UNIQUE KEY `MENU_ID` (`MENU_ID`),
  KEY `MENU_PARENTID` (`MENU_PARENTID`),
  CONSTRAINT `Menu_Childs` FOREIGN KEY (`MENU_PARENTID`) REFERENCES `menu` (`MENU_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `menu` */

/*Table structure for table `menu_button` */

DROP TABLE IF EXISTS `menu_button`;

CREATE TABLE `menu_button` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `MENU_ID` int(11) NOT NULL,
  `BUTTON_ID` int(11) NOT NULL,
  `ORDERBY` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BUTTON_ID` (`BUTTON_ID`),
  KEY `MENU_ID` (`MENU_ID`),
  CONSTRAINT `MenuMappingButton_ButtonModel` FOREIGN KEY (`BUTTON_ID`) REFERENCES `button` (`BUTTON_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `MenuMappingButton_MenuModel` FOREIGN KEY (`MENU_ID`) REFERENCES `menu` (`MENU_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `menu_button` */

/*Table structure for table `message` */

DROP TABLE IF EXISTS `message`;

CREATE TABLE `message` (
  `MessageID` int(10) NOT NULL AUTO_INCREMENT,
  `MessageTitle` varchar(200) NOT NULL,
  `MessageText` text NOT NULL,
  `SendUser` int(10) NOT NULL,
  `RecUser` text NOT NULL,
  `SendTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `ExpirationTime` date DEFAULT NULL,
  `IsSendEmail` bit(1) NOT NULL,
  `SendEmailState` int(10) DEFAULT NULL,
  UNIQUE KEY `MessageID` (`MessageID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

/*Data for the table `message` */

/*Table structure for table `new` */

DROP TABLE IF EXISTS `new`;

CREATE TABLE `new` (
  `NewId` int(11) NOT NULL AUTO_INCREMENT,
  `NewTitle` longtext,
  `NewContent` longtext,
  `NewAuthor` longtext,
  `CreateTime` longtext,
  PRIMARY KEY (`NewId`),
  UNIQUE KEY `NewId` (`NewId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `new` */

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `ROLE_ID` int(11) NOT NULL AUTO_INCREMENT,
  `ROLE_NAME` longtext,
  `ROLE_DESCRIPTION` longtext,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ROLE_ID`),
  UNIQUE KEY `ROLE_ID` (`ROLE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `role` */

/*Table structure for table `role_button` */

DROP TABLE IF EXISTS `role_button`;

CREATE TABLE `role_button` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ROLE_ID` int(11) NOT NULL,
  `MENU_ID` int(11) NOT NULL,
  `BUTTON_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BUTTON_ID` (`BUTTON_ID`),
  KEY `ROLE_ID` (`ROLE_ID`),
  CONSTRAINT `RoleMappingButton_ButtonModel` FOREIGN KEY (`BUTTON_ID`) REFERENCES `button` (`BUTTON_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `RoleMappingButton_RoleModel` FOREIGN KEY (`ROLE_ID`) REFERENCES `role` (`ROLE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `role_button` */

/*Table structure for table `role_menu` */

DROP TABLE IF EXISTS `role_menu`;

CREATE TABLE `role_menu` (
  `ROLE_ID` int(11) NOT NULL,
  `MENU_ID` int(11) NOT NULL,
  PRIMARY KEY (`ROLE_ID`,`MENU_ID`),
  KEY `Role_MenuList_Target` (`MENU_ID`),
  CONSTRAINT `Role_MenuList_Source` FOREIGN KEY (`ROLE_ID`) REFERENCES `role` (`ROLE_ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Role_MenuList_Target` FOREIGN KEY (`MENU_ID`) REFERENCES `menu` (`MENU_ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `role_menu` */

/*Table structure for table `systeminfo` */

DROP TABLE IF EXISTS `systeminfo`;

CREATE TABLE `systeminfo` (
  `SYSTEM_ID` int(11) NOT NULL AUTO_INCREMENT,
  `SYSTEM_TITLE` longtext,
  `SYSTEM_COPYRIGHT` longtext,
  `SYSTEM_VERSION` longtext,
  `ISVALIDCODE` tinyint(1) NOT NULL,
  PRIMARY KEY (`SYSTEM_ID`),
  UNIQUE KEY `SYSTEM_ID` (`SYSTEM_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `systeminfo` */

/*Table structure for table `test` */

DROP TABLE IF EXISTS `test`;

CREATE TABLE `test` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `age` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `test` */

/*Table structure for table `user_message` */

DROP TABLE IF EXISTS `user_message`;

CREATE TABLE `user_message` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `MessageID` int(10) NOT NULL,
  `UserID` int(10) NOT NULL,
  `IsRead` bit(1) NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8;

/*Data for the table `user_message` */

insert  into `user_message`(`ID`,`MessageID`,`UserID`,`IsRead`) values (36,11,1,''),(37,12,1,''),(38,13,1,''),(39,14,1,''),(40,11,2,''),(41,12,2,'\0'),(42,14,2,'\0'),(43,16,1,''),(44,15,1,''),(45,18,1,''),(46,14,27,'\0');

/*Table structure for table `user_role` */

DROP TABLE IF EXISTS `user_role`;

CREATE TABLE `user_role` (
  `USER_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`USER_ID`,`ROLE_ID`),
  KEY `User_RoleList_Target` (`ROLE_ID`),
  CONSTRAINT `User_RoleList_Source` FOREIGN KEY (`USER_ID`) REFERENCES `userinfomation` (`USER_ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `User_RoleList_Target` FOREIGN KEY (`ROLE_ID`) REFERENCES `role` (`ROLE_ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `user_role` */

/*Table structure for table `userinfomation` */

DROP TABLE IF EXISTS `userinfomation`;

CREATE TABLE `userinfomation` (
  `USER_ID` int(11) NOT NULL AUTO_INCREMENT,
  `USER_NAME` longtext,
  `USER_PASSWORD` longtext,
  `USER_REALLYNAME` longtext,
  `HEADPORTRAIT` longtext,
  `DEPARTMENT_ID` int(11) NOT NULL,
  `ISENABLE` tinyint(1) NOT NULL,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  `REMARK` longtext,
  `MobilePhone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`USER_ID`),
  UNIQUE KEY `USER_ID` (`USER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `userinfomation` */

insert  into `userinfomation`(`USER_ID`,`USER_NAME`,`USER_PASSWORD`,`USER_REALLYNAME`,`HEADPORTRAIT`,`DEPARTMENT_ID`,`ISENABLE`,`CREATE_TIME`,`UPDATE_TIME`,`REMARK`,`MobilePhone`,`Email`) values (1,'admin','123456','管理员','',1,1,'2017-06-27 15:48:37','2017-06-27 15:48:37','','','');

/* Function  structure for function  `TruncateTime` */

/*!50003 DROP FUNCTION IF EXISTS `TruncateTime` */;
DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` FUNCTION `TruncateTime`(dateValue DateTime) RETURNS date
return Date(dateValue) */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

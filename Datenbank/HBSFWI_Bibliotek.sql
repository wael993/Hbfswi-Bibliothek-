-- create and use Database
IF DB_ID('HBSFWI_Bibleothek') IS NULL
create database HBSFWI_Bibleothek
go

USE HBSFWI_Bibleothek;
go
-- create tables
IF OBJECT_ID('ausleihe_Prozess') IS NOT NULL
  DROP TABLE ausleihe_Prozess;
GO

IF OBJECT_ID('Auftraege') IS NOT NULL
  DROP TABLE Auftraege;
GO

IF OBJECT_ID('Benutzer') IS NOT NULL
  DROP TABLE Benutzer;
GO


IF OBJECT_ID('Buecher') IS NOT NULL
  DROP TABLE Buecher;
GO



CREATE TABLE Benutzer (
	Benutzer_id int PRIMARY KEY,
	UserName NVARCHAR (50) NOT NULL,
	Passwort NVARCHAR (50) NOT NULL,
	Vorname NVARCHAR (50) NOT NULL,
	Nachname NVARCHAR (50) NOT NULL,
	Rolle int NOT NULL,
	Semester int,
	studiengang NVARCHAR (25)
);

CREATE TABLE Buecher (
	ISBN INT PRIMARY KEY ,
	Buch_Titel NVARCHAR (255) NOT NULL,
	Rechtung NVARCHAR (255) NOT NULL,
	Beschreibung NVARCHAR (255),
	Anzahl int
);


CREATE TABLE ausleihe_Prozess(
	Prozess_id INT identity(3000,1) PRIMARY KEY,
	Prozess_status nvarchar(20),
	Prozess_datum DATE NOT NULL,
	Benutzer_id int ,
	ISBN INT ,
	FachNr int,
	CONSTRAINT fk_Benutzer FOREIGN KEY (Benutzer_id)
					REFERENCES Benutzer(Benutzer_id),
	CONSTRAINT fk_Buch FOREIGN KEY (ISBN)
					REFERENCES Buecher(ISBN)

);


CREATE TABLE Auftraege(
	Auftrag_id INT identity(3000,1) PRIMARY KEY,
	Auftrag_status nvarchar(20),
	Auftrag_datum DATE NOT NULL,
	Benutzer_id int ,
	ISBN INT ,
    FachNr int,
	CONSTRAINT fk_A_Benutzer FOREIGN KEY (Benutzer_id)
					REFERENCES Benutzer(Benutzer_id),
	CONSTRAINT fk_A_Buch FOREIGN KEY (ISBN)
					REFERENCES Buecher(ISBN)
	
);

--Admin
INSERT INTO Benutzer(Benutzer_id,UserName,Passwort,Vorname,Nachname, Rolle)
			  VALUES(120600,'wael','12','wael','zobani',1)

--Mitarbeiter
INSERT INTO Benutzer(Benutzer_id,UserName,Passwort,Vorname,Nachname, Rolle)
			  VALUES(120700,'maxmstermann','123','max','mustermann',2)
INSERT INTO Benutzer(Benutzer_id,UserName,Passwort,Vorname,Nachname, Rolle)
			  VALUES(120701,'mustermanmax','123','mustermann','max',21)

--Student
INSERT INTO Benutzer(Benutzer_id,UserName,Passwort,Vorname,Nachname, Rolle, Semester,studiengang)
			  VALUES(120800,'LiamSmith','1234','Liam','Smith',3,1,'Systemintegration'),
					(120801,'OliviaJohnson','1234','Olivia','Johnson',3,2,'Systemintegration'),
					(120802,'NoahWilliams','1234','Noah','Williams',3,3,'Systemintegration'),
					(120803,'EmmaBrown','1234','Emma','Brown',3,4,'Systemintegration'),
					(120804,'OliverJones','1234','Oliver','Jones',3,1,'Systemintegration'),
					(120805,'CharlotteMiller','1234','Charlotte','Miller',3,2,'Systemintegration'),
					(120815,'AmeliaGarcia','1234','Amelia','Garcia',3,3,'Systemintegration'),

					(120806,'AvaDavis','123','Ava','Davis',3,1,'Anwendungsentwicklung'),
					(120807,'ElijahRodriguez','123','Elijah','Rodriguez',3,1,'Anwendungsentwicklung'),
					(120808,'JamesMartinez','123','James','Martinez',3,1,'Anwendungsentwicklung'),
					(120809,'WilliamHernandez','123','William','Hernandez',3,1,'Anwendungsentwicklung'),
					(120810,'SophiaLopez','123','Sophia','Lopez',3,1,'Anwendungsentwicklung'),
					(120811,'BenjaminGonzales','12','Benjamin','Gonzales',31,3,'Anwendungsentwicklung'),


					(1208011,'test','12','Olivia','Johnson',1,1,'Systemintegration');
--B�cher
INSERT INTO Buecher(ISBN,Buch_Titel,Rechtung,Anzahl,Beschreibung)
			  VALUES(98765111,'Hardware & Security','Systemintegration',7,'Hardware & Security: Werkzeuge, Pentesting, Pr�vention. So beugen Sie Hacking-Angriffen und Sicherheitsl�cken vor Gebundene Ausgabe'),
					(98765122,'Python 3','Systemintegration',8,'Python 3 Programmieren f�r Einsteiger: das fundierte und praxisrelevante Handbuch. Wie Sie als Anf�nger Programmieren lernen und schnell zum Python-Experten werden. Bonus: �bungen inkl. L�sungen '),
					(987651,'Algorithmen in Python','Systemintegration',9,'Algorithmen in Python: 32 Klassiker vom Damenproblem bis zu neuronalen Netzen Kindle Ausgabe'),
					(9876512,'IT-Systemintegration','Systemintegration',11,'Das vorliegende Buch behandelt M�glichkeiten der Herangehensweise an die IT-Systemzusammenf�hrung bei Unternehmensfusionen. '),
					(9876513,'Systemintegration- Virtual Umgebung','Systemintegration',1,'Mit Hilfe einer Open Inventor-Entwicklungsschnittstelle'),
					(9876514,'Einfache IT-Systeme','Systemintegration',21,'konzipiert f�r die Ausbildung zum informationstechnischen Assistenten/zur informationstechnischen Assistentin'),
					(9876515,'Vernetzte IT-Systeme','Systemintegration',13,'Vernetzte IT-Systeme: Sch�lerband'),
					(9876516,'Basiswissen IT-Berufe','Systemintegration',15,'die Neuauflage wurde um zahlreiche Inhalte zum Rechnungswesen und Controlling erweitertbietet alle kaufm�nnischen'),
					(9876517,' Fachstufe IT-Systeme','Systemintegration',17,'Ausgabe zu den neuen Lehrpl�nen 2020 / Sch�lerband'),
					(9876518,'Web-basierte Systemintegration','Systemintegration',1,'So �berf�hren Sie bestehende Anwendungssysteme in eine moderne Webarchitektur'),
					(9876519,'Systemintegration','Systemintegration',21,'Vom Transistor zur gro�integrierten Schaltung'),
					(9876511,'Web Systemintegration','Systemintegration',18,' Mit der Integrationstechnologie wer den bestehende Software-Komponenten gekapselt, um sie an das Web anbinden zu k�nnen'),
					(9876510,'Systemintegration im computergest�tzten','Systemintegration',13,'Moderne Kommunikationstechnologien finden im computergest�tzten Publizieren ein ideales Einsatzgebiet'),

					(887651,'Einstieg in SQL','Anwendungsentwicklung',17,'F�r alle wichtigen Datenbanksysteme: MySQL, PostgreSQL, MariaDB, MS SQL. �ber 600 Seiten. Ohne Vorwissen einsteigen'),
					(8876512,'Git','Anwendungsentwicklung',14,'Projektverwaltung f�r Entwickler und DevOps-Teams. Inkl. Praxistipps und Git-Kommandoreferenz'),
					(8876513,'Java','Anwendungsentwicklung',11,'Der leichte Java-Einstieg f�r Programmieranf�nger. Mit vielen Beispielen und �bungsaufgaben'),
					(8876514,'Java ist auch eine Insel','Anwendungsentwicklung',51,'Objects first - Eine Einf�hrung in Java'),
					(8876515,'Besser coden','Anwendungsentwicklung',2,'Best Practices f�r Clean Code. Das ideale Buch f�r die professionelle Softwareentwicklung'),
					(8876516,'Einf�hrung in die Softwaretechnik','Anwendungsentwicklung',10,'Das Buch f�hrt in die Grundlagen der Softwaretechnik ein. Dabei liegt sein Fokus auf der systematischen und modellbasierten Software- und Systementwicklung '),
					(8876517,'Algorithmen und C++','Anwendungsentwicklung',13,'Von der Diskreten Mathematik zum fertigen Programm - Lern- und Arbeitsbuch f�r Informatiker und Mathematiker'),
					(8876518,'Eigene Apps programmieren','Anwendungsentwicklung',21,'Schritt f�r Schritt zur eigenen App mit LiveCode. Spielend Programmieren lernen ohne Vorkenntnisse!'),
					(8876519,'Essenz der Informatik','Anwendungsentwicklung',16,'Mit diesem Buch meistern Sie die Grundlagen der Informatik.Dieses Buch beschreibt das weite Universum der Informatik und Informationstechnologie. '),
					(8876511,'Programmieren ganz einfach','Anwendungsentwicklung',17,'Die Basics f�r Einsteiger Schritt f�r Schritt'),
					(8876510,'Programmierung in C','Anwendungsentwicklung',9,'Eigene Projekte selbst entwickeln und verstehen');



--					use TOnline_Bibleothek
--go
--select * from Benutzer where Rolle=2 or Rolle=21
--select * from Buecher
--select * from ausleihe_Prozess 

--select * from Auftraege
﻿-- Patients and Doctors table
CREATE TABLE THE_SetSubj (
    acSubject CHAR(6) NOT NULL PRIMARY KEY,      -- Šifra
    acName2 VARCHAR(255) NOT NULL,               -- Prezime i ime
    AdBirthDate VARCHAR(255) NOT NULL,         -- Datum rođenja
    acFieldSC VARCHAR(255) NULL,             -- Telefon
    acAddress VARCHAR(255) NULL,                 -- Adresa
    acFieldSA VARCHAR(255) NULL,                 -- Mesto rođenja (nullable)
    acFieldSD VARCHAR(255) NULL,                 -- Ime oca (nullable)
    acFieldSE VARCHAR(255) NULL,                 -- Mesto rođenja oca (nullable)
    acFieldSF INT NULL,                          -- Godište oca (nullable)
    acFieldSG VARCHAR(255) NULL,                 -- Ime majke (nullable)
    acFieldSH VARCHAR(255) NULL,                 -- Mesto rođenja majke (nullable)
    acFieldSI INT NULL,                          -- Godište majke (nullable)
    acSupplier CHAR(1) NOT NULL DEFAULT 'F'      -- Supplier (T for True, F for False)
);



-- Medical reports table
CREATE TABLE _css_Nalaz (
    acNalaz CHAR(6) NOT NULL PRIMARY KEY,     -- Šifra nalaza
    acSubject CHAR(6) NOT NULL,               -- Šifra pacijenta (foreign key)
    adDate DATETIME NOT NULL,                     -- Datum nalaza
    acDescription TEXT,                        -- Opis nalaza
    acDG VARCHAR(500),                        -- Dijagnoza
    acTH VARCHAR(255),                        -- TH
    acKontrola VARCHAR(255),                  -- Kontrola
    acLekar CHAR(6) NOT NULL,                 -- Šifra lekara (foreign key)
    Anamneza VARCHAR(MAX),    
    
    FOREIGN KEY (acSubject) REFERENCES THE_SetSubj(acSubject),
    FOREIGN KEY (acLekar) REFERENCES THE_SetSubj(acSubject)
);

CREATE TABLE SchemaTable (
    SchemaName NVARCHAR(50) NOT NULL PRIMARY KEY, -- Unique identifier
    SchemaValue NVARCHAR(MAX) NOT NULL -- Stores large text with new lines
);

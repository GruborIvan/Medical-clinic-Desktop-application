CREATE TABLE THE_SetSubj (
    acSubject CHAR(6) NOT NULL PRIMARY KEY,      -- Šifra
    acName2 VARCHAR(255) NOT NULL,                -- Prezime i ime
    acAddress VARCHAR(255) NOT NULL,              -- Adresa
    acFieldSA VARCHAR(255) NOT NULL,              -- Mesto rođenja
    acFieldSC VARCHAR(255) NOT NULL,              -- Telefon
    acFieldSD VARCHAR(255) NOT NULL,              -- Ime oca
    acFieldSE VARCHAR(255) NOT NULL,              -- Mesto rođenja oca
    acFieldSF INT NOT NULL,                       -- Godište oca
    acFieldSG VARCHAR(255) NOT NULL,              -- Ime majke
    acFieldSH VARCHAR(255) NOT NULL,              -- Mesto rođenja majke
    acFieldSI INT NOT NULL                        -- Godište majke
);
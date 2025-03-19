-- Insert dummy data into THE_SetSubj
INSERT INTO THE_SetSubj (acSubject, acName2, acAddress, acFieldSA, acFieldSC, acFieldSD, acFieldSE, acFieldSF, acFieldSG, acFieldSH, acFieldSI)
VALUES
('000001', 'Marko Marković', 'Neka Ulica 12, Beograd', 'Beograd', '0651234567', 'Petar Marković', 'Požarevac', 1965, 'Jelena Marković', 'Novi Sad', 1970),
('000002', 'Ana Jovanović', 'Maksim Gorkog 33, Novi Sad', 'Novi Sad', '0649876543', 'Miloš Jovanović', 'Niš', 1972, 'Ivana Jovanović', 'Zrenjanin', 1975),
('000003', 'Ivana Petrović', 'Bulevar Oslobođenja 45, Niš', 'Niš', '0631122334', 'Milan Petrović', 'Kragujevac', 1968, 'Tatjana Petrović', 'Subotica', 1972),
('000004', 'Nikola Simić', 'Trg Republike 22, Kragujevac', 'Kragujevac', '0625566778', 'Vladimir Simić', 'Beograd', 1958, 'Maja Simić', 'Čačak', 1960);


-- Insert dummy data into _css_Nalaz
INSERT INTO _css_Nalaz (acNalaz, acSubject, adDate, acDescription, acDG, acTH, acKontrola, acLekar)
VALUES 
('N00001', '000001', '2025-03-04', 'Pacijent se žali na bol u grudima.', 'I20.0', 'Aspirin', 'Kontrola za 7 dana', '000002'),
('N00002', '000002', '2025-03-04', 'Pregled zbog visokog pritiska.', 'I10', 'Enalapril', 'Kontrola za 14 dana', '000003'),
('N00003', '000004 ', '2025-03-04', 'Rutinski pregled.', 'Z00.0', 'Nema terapije', 'Nije potrebna kontrola', '000001');


-- Insert real data into SchemaTable
INSERT INTO SchemaTable (SchemaName, SchemaValue)
VALUES 
('UZ ABDOMENA I BUBREGA',
'Jetra je normalnih dimenzija, homogene strukture.
Žučna kesa jasnih kontura, bez prisustva intraluminalnog patološkog sadržaja.
Žučni putevi nisu prošireni.
Slezina je normalne veličine, homogene strukture.
Pankreas je normalne veličine, jasnih kontura, homogene strukture.
U projekciji na obe nadbubrežne žlezde ne vide se patološke promene.
Oba bubrega su normalne veličine i oblika, održane širine parenhima, jasne kortikomedularne granice, bez znakova dilatacije pijelokaliksnog sistema.
Mokraćna bešika umereno distendirana urinom, bez prisustva grubljih intraluminalnih patoloških promena.
Ne vidi se prisustvo slobodne tečnosti u abdomenu i maloj karlici.
Mezenterijalni limfonodusi se vide kao pojedinačni nisu signifikantno uvećani.
Retroperitonealni limfonodusi dostupni UZ pregledu nisu signifikantno uvećani.
Apendiks uredne morfologije.
Debljina crevnog zida je normalna.
')
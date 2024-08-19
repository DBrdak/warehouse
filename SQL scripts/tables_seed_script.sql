-- Insert sample data into Klienci (Clients)
INSERT INTO Klienci (id_klienta, nip, nazwa, czy_usunieto)
VALUES 
(NEWID(), 'PL5556665454', 'Klient A', 0),
(NEWID(), 'PL4446665454', 'Klient B', 0),
(NEWID(), 'PL2226665454', 'Klient C', 0),
(NEWID(), 'PL1116665454', 'Klient D', 0),
(NEWID(), 'PL3336665454', 'Klient E', 0);

-- Insert sample data into Kierowcy (Drivers)
INSERT INTO Kierowcy (id_kierowcy, imie, nazwisko, numer_rejestracyjny_pojazdu, czy_usunieto)
VALUES 
(NEWID(), 'Jan', 'Kowalski', 'WPR1234', 0),
(NEWID(), 'Andrzej', 'Nowak', 'WPI5678', 0),
(NEWID(), 'Tomasz', 'Wiśniewski', 'EL9012', 0),
(NEWID(), 'Piotr', 'Wójcik', 'DW3456', 0),
(NEWID(), 'Marek', 'Lewandowski', 'PO7890', 0);

-- Insert sample data into Sektory (Sectors)
INSERT INTO Sektory (id_sektora, numer, czy_usunieto)
VALUES 
(NEWID(), 1, 0);

-- Insert sample data into Magazynierzy (Warehouse Workers)
INSERT INTO Magazynierzy (id_magazyniera, id_sektora, numer_identyfikacyjny, imie, nazwisko, pozycja, czy_zwolniony, czy_usunieto)
VALUES 
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 101, 'Paweł', 'Kaczmarek', 'Brygadzista', 0, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 102, 'Robert', 'Zieliński', 'Wózek widłowy', 0, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 103, 'Jacek', 'Kowalczyk', 'Dokumentacja', 0, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 104, 'Karol', 'Wiśniewski', NULL, 0, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 105, 'Adam', 'Wojciechowski', 'Wózek widłowy', 0, 0);

-- Insert sample data into Miejsca_paletowe (Pallet Spaces)
INSERT INTO Miejsca_paletowe (id_miejsca_paletowego, id_sektora, numer, polka, regal, czy_usunieto)
VALUES 
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 1, 1, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 2, 1, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 3, 1, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 4, 2, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 5, 2, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 6, 2, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 7, 3, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory ), 8, 3, 1, 0),
(NEWID(), (SELECT TOP 1 id_sektora FROM Sektory), 9, 3, 1, 0);

-- Insert sample data into Transporty (Transports)
INSERT INTO Transporty (id_transportu, id_magazyniera, id_kierowcy, id_klienta, numer, data_czas, rodzaj, czy_usunieto)
VALUES 
(NEWID(), (SELECT TOP 1 id_magazyniera FROM Magazynierzy ORDER BY NEWID()), (SELECT TOP 1 id_kierowcy FROM Kierowcy ORDER BY NEWID()), (SELECT TOP 1 id_klienta FROM Klienci ORDER BY NEWID()), 301, GETDATE(), 'Import', 0),
(NEWID(), (SELECT TOP 1 id_magazyniera FROM Magazynierzy ORDER BY NEWID()), (SELECT TOP 1 id_kierowcy FROM Kierowcy ORDER BY NEWID()), (SELECT TOP 1 id_klienta FROM Klienci ORDER BY NEWID()), 302, GETDATE(), 'Export', 0),
(NEWID(), (SELECT TOP 1 id_magazyniera FROM Magazynierzy ORDER BY NEWID()), (SELECT TOP 1 id_kierowcy FROM Kierowcy ORDER BY NEWID()), (SELECT TOP 1 id_klienta FROM Klienci ORDER BY NEWID()), 303, GETDATE(), 'Import', 0),
(NEWID(), (SELECT TOP 1 id_magazyniera FROM Magazynierzy ORDER BY NEWID()), (SELECT TOP 1 id_kierowcy FROM Kierowcy ORDER BY NEWID()), (SELECT TOP 1 id_klienta FROM Klienci ORDER BY NEWID()), 304, GETDATE(), 'Export', 0);

-- Insert sample data into Towary (Freights)
INSERT INTO Towary (id_towaru, nazwa, rodzaj, ilosc, jednostka, id_odbioru, id_dostawy, id_miejsca_paletowego, czy_usunieto)
VALUES 
(NEWID(), 'Ser kozi', 'Nabiał', 100.0, 'szt', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Łopatka wp.', 'Mięso surowe - chłodzone', 150.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Szynka Wędzona', 'Wędliny', 200.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Twaróg', 'Nabiał', 250.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Schab wp. b/k', 'Mięso surowe - chłodzone', 300.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Kiełbasa Śląska', 'Wędliny', 350.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Jogurt naturalny', 'Nabiał', 400.0, 'szt', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Antrykot wołowy z/k', 'Mięso surowe - chłodzone', 450.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Polędwica Sopocka', 'Wędliny', 500.0, 'kg', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0),
(NEWID(), 'Serek homogenizowany', 'Nabiał', 550.0, 'szt', (SELECT TOP 1 id_transportu FROM Transporty WHERE rodzaj = 'Import' ORDER BY NEWID()), (SELECT TOP 1 id_transportu FROM Transporty  WHERE rodzaj = 'Export' ORDER BY NEWID()), (SELECT TOP 1 id_miejsca_paletowego FROM Miejsca_paletowe ORDER BY NEWID()), 0);

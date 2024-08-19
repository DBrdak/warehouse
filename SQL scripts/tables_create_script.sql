DROP TABLE IF EXISTS Towary;
DROP TABLE IF EXISTS Transporty;
DROP TABLE IF EXISTS Miejsca_paletowe;
DROP TABLE IF EXISTS Magazynierzy;
DROP TABLE IF EXISTS Sektory;
DROP TABLE IF EXISTS Kierowcy;
DROP TABLE IF EXISTS Klienci;

CREATE TABLE Klienci (
    id_klienta UNIQUEIDENTIFIER CONSTRAINT PK_Klienci PRIMARY KEY,
    nip VARCHAR(12) NOT NULL CONSTRAINT UQ_Klienci_Nip UNIQUE,
    nazwa VARCHAR(55) NOT NULL,
    czy_usunieto BIT NOT NULL DEFAULT 0
);

CREATE TABLE Kierowcy (
    id_kierowcy UNIQUEIDENTIFIER CONSTRAINT PK_Kierowcy PRIMARY KEY,
    imie VARCHAR(55) NOT NULL,
    nazwisko VARCHAR(55) NOT NULL,
    numer_rejestracyjny_pojazdu VARCHAR(8) NOT NULL CONSTRAINT UQ_Kierowcy_NumerRejestracyjny UNIQUE,
    czy_usunieto BIT NOT NULL DEFAULT 0
);

CREATE TABLE Sektory (
    id_sektora UNIQUEIDENTIFIER CONSTRAINT PK_Sektory PRIMARY KEY,
    numer INT NOT NULL CONSTRAINT UQ_Sektory_Numer UNIQUE,
    czy_usunieto BIT NOT NULL DEFAULT 0
);

CREATE TABLE Magazynierzy (
    id_magazyniera UNIQUEIDENTIFIER CONSTRAINT PK_Magazynierzy PRIMARY KEY,
    id_sektora UNIQUEIDENTIFIER NOT NULL,
    numer_identyfikacyjny INT NOT NULL CONSTRAINT UQ_Magazynierzy_NumerIdentyfikacyjny UNIQUE,
    imie VARCHAR(55) NOT NULL,
    nazwisko VARCHAR(55) NOT NULL,
    pozycja VARCHAR(55),
    czy_zwolniony BIT NOT NULL DEFAULT 0,
    czy_usunieto BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Magazynierzy_Sektory FOREIGN KEY (id_sektora) REFERENCES Sektory(id_sektora)
);

CREATE TABLE Miejsca_paletowe (
    id_miejsca_paletowego UNIQUEIDENTIFIER CONSTRAINT PK_MiejscaPaletowe PRIMARY KEY,
    id_sektora UNIQUEIDENTIFIER NOT NULL,
    numer INT NOT NULL,
    regal INT NOT NULL,
    polka INT NOT NULL,
    czy_usunieto BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_MiejscaPaletowe_Sektory FOREIGN KEY (id_sektora) REFERENCES Sektory(id_sektora)
);

CREATE TABLE Transporty (
    id_transportu UNIQUEIDENTIFIER CONSTRAINT PK_Transporty PRIMARY KEY,
    id_magazyniera UNIQUEIDENTIFIER NOT NULL,
    id_kierowcy UNIQUEIDENTIFIER NOT NULL,
    id_klienta UNIQUEIDENTIFIER NOT NULL,
    numer INT NOT NULL CONSTRAINT UQ_Transporty_Numer UNIQUE,
    data_czas DATETIME NOT NULL,
    rodzaj VARCHAR(6) NOT NULL,
    czy_usunieto BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Transporty_Magazynierzy FOREIGN KEY (id_magazyniera) REFERENCES Magazynierzy(id_magazyniera),
    CONSTRAINT FK_Transporty_Kierowcy FOREIGN KEY (id_kierowcy) REFERENCES Kierowcy(id_kierowcy),
    CONSTRAINT FK_Transporty_Klienci FOREIGN KEY (id_klienta) REFERENCES Klienci(id_klienta)
);

CREATE TABLE Towary (
    id_towaru UNIQUEIDENTIFIER CONSTRAINT PK_Towary PRIMARY KEY,
    nazwa VARCHAR(55) NOT NULL,
    rodzaj VARCHAR(55) NOT NULL,
    ilosc DECIMAL(10,2) NOT NULL,
    jednostka VARCHAR(55) NOT NULL,
    id_odbioru UNIQUEIDENTIFIER,
    id_dostawy UNIQUEIDENTIFIER NOT NULL,
    id_miejsca_paletowego UNIQUEIDENTIFIER NOT NULL,
    czy_usunieto BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Towary_MiejscaPaletowe FOREIGN KEY (id_miejsca_paletowego) REFERENCES Miejsca_paletowe(id_miejsca_paletowego),
    CONSTRAINT FK_Towary_Dostawy FOREIGN KEY (id_dostawy) REFERENCES Transporty(id_transportu),
    CONSTRAINT FK_Towary_Odbiory FOREIGN KEY (id_odbioru) REFERENCES Transporty(id_transportu)
);

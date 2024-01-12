# Library Project

## Spis Treści

1. [Wstęp](#wprowadzenie)
    - [Cel Projektu](#cel-projektu)
    - [Wymagania Systemowe](#wymagania-systemowe)
2. [Instalacja](#instalacja)
    - [Wymagania Przedinstalacyjne](#wymagania-przedinstalacyjne)
    - [Instrukcje Instalacji](#instrukcje-instalacji)
3. [Konfiguracja](#konfiguracja)
    - [Konfiguracja Łańcucha Połączenia z Bazą Danych](#konfiguracja-łańcucha-połączenia-z-bazą-danych)
    - [Testowi Użytkownicy i Hasła](#testowi-użytkownicy-i-hasła)
4. [Opis Działania Aplikacji](#opis-działania-aplikacji)
    - [Struktura Aplikacji](#struktura-aplikacji)
    - [Funkcje Główne](#funkcje-główne)
    - [Moduły](#moduły)
    - [Katalogi](#katalogi)

# Wprowadzenie
## Cel Projektu
Celem projektu jest stworzenie aplikacji internetowej do zarządzania biblioteką online. 
Aplikacja umożliwia dodawanie, edycję, usuwanie książek, rezerwację oraz wypożyczanie dla zalogowanych użytkowników.

## Wymagania Systemowe
  - .NET 6.X
  - Microsoft SQL Server
  - Przeglądarka internetowa


# Instalacja
## Wymagania Przedinstalacyjne
Sprawdź, czy masz zainstalowane wymagane oprogramowanie, takie jak .NET 6.X.X i Microsoft SQL Server.

## Instrukcje Instalacji
1. Sklonuj repozytorium z projektem.
   - `https://github.com/EddieCarbon/Library.git`
2. Skonfiguruj Łańcuch Połączenia z Bazą Danych w pliku appsettings.json.
   - `cd Library`
3. Używając konsoli menagera paczek NuGet utwórz bazę danych korzystając z migracji.
   - `Update-Database` 
5. Uruchom migracje bazy danych.
   - `dotnet run`


# Konfiguracja
## Konfiguracja Łańcucha Połączenia z Bazą Danych
W pliku appsettings.json znajdziesz sekcję dotyczącą łańcucha połączenia z bazą danych. Skonfiguruj odpowiednio wartości tego łańcucha, aby połączyć się z Twoją bazą danych.

    "ConnectionStrings": {
        "ApplicationDbContextConnection": "Server=localhost;Database=Library;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True"
    }

## Testowi Użytkownicy i Hasła
- Admin:
    - Email: admin@example.com
    - Hasło: Admin@123

- Manager:
    - Email: manager@example.com
    - Hasło: Manager@123

# Opis Działania Aplikacji

## Struktura Aplikacji
Projekt "**Library**" został zorganizowany zgodnie z najlepszymi praktykami programowania i architektury oprogramowania. 
- **Admin:** Ma dostęp do zakładki `Book Manager`. Może dodawać wydawców i autorów. Dodawać nowe książki, edytować je i usuwać.
- **Manager:** Ma takie same uprawnienia jak admin
- **Użytkownik zalogowany:** Może wypożyczać książki
- **Użytkownik niezalogowany:** Może przeglądać dostępne książki w zakładce **Books**

    
## Funkcje Główne
- ### Book Manager
    - **Dodawanie Książki:**
        - Kontroler `BooksController` obsługuje akcje związane z dodawaniem nowych książek.
    - **Edycja i Usuwanie Książki:**
        - Funkcje edycji i usuwania są dostępne dla administratorów i managerów.
- ### Author & Publisher Management
    - **Dodawanie i Edycja Autorów oraz Wydawnictw:**
        - Administratorzy mogą dodawać nowych autorów i edytować informacje o nich.
- ### Home
    - **Rezerwacja Książki:**
        - Użytkownicy mogą rezerwować dostępne książki na swoje konto.
    - **Wypożyczanie Książki:**
        - Funkcja wypożyczania książek dostępna jest dla zalogowanych użytkowników.

### Moduły
- **Books Manager:** Moduł odpowiedzialny za zarządzanie książkami, w tym dodawanie, edycję i usuwanie. Zawiera kontrolery, modele i widoki związane z zarządzaniem książkami.
- **Author & Publisher Management:** Moduł umożliwiający zarządzanie autorami i wydawnictwami. Zapewnia kontrolery, modele i widoki do edycji informacji na temat autorów i wydawnictw. 
- **Home:** Moduł obsługujący wypożyczenia i rezerwacje użytkowników. Zapewnia funkcjonalność zarządzania wypożyczonymi książkami.

### Katalogi
- **Controllers:** Zawiera kontrolery obsługujące żądania HTTP, odpowiedzialne za przetwarzanie danych i zarządzanie logiką biznesową.
- **Data:** Katalog zawiera klasy kontekstu baz danych oraz konfiguracje encji.
- **Models** Tutaj znajdują się klasy reprezentujące modele danych, czyli encje bazy danych.
- **Views**: Zawiera pliki widoków, czyli interfejsu użytkownika, napisane w formacie Razor lub HTML.
- **wwwroot:** Katalog ten przechowuje pliki statyczne, takie jak arkusze stylów CSS, skrypty JavaScript czy obrazy.
- **Areas:** Dodatkowy katalog zawierający obszary aplikacji, gdzie każdy obszar może mieć swoje kontrolery, modele i widoki.

 


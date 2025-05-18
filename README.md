# 📦 Konfiguracja bazy danych i migracji

## 🔧 Wymagania wstępne

- Projekt zawiera poprawnie skonfigurowany `DbContext`.
- Zainstalowane pakiety NuGet:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.EntityFrameworkCore.Sqlite`

## 🛠️ Utworzenie migracji i pliku bazy danych

Aby przygotować bazę danych oraz utworzyć początkową migrację, wykonaj następujące kroki:

1. Otwórz **Package Manager Console** w Visual Studio:  
   `Tools > NuGet Package Manager > Package Manager Console`

2. Dodaj migrację poleceniem:
   ```powershell
   Add-Migration InitialCreate -OutputDir "Data/Migrations"
   ```

3. Zastosuj migrację do bazy danych:
   ```powershell
   Update-Database
   ```

## 📂 Efekty

- W folderze `Data/Migrations` pojawią się pliki migracji.


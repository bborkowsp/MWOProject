@echo off
SETLOCAL

REM Ustalamy nazwę dla naszej solucji na podstawie nazwy folderu
for %%I in ("%~dp0.") do set "SOLUTION_NAME=%%~nxI"

REM Tworzymy nową solucję przy użyciu dotnet CLI
dotnet new sln -n %SOLUTION_NAME%

REM Szukamy wszystkich plików .csproj w podfolderach i dodajemy każdy znaleziony projekt do solucji
for /r %%f in (*.csproj) do dotnet sln %SOLUTION_NAME%.sln add "%%f"

ENDLOCAL

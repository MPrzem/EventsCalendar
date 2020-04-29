# Aplikacja "EventsCalendar"

![Test Image 1](GitHubImages/zdjecie_apki.PNG)

|  Nr.   |   | |
| :------------: | :------------: | :------------: |
| 1 | Autorzy  | Przemys�aw Malec, Przemys�aw Widz|
| 2 | J�zyk programowania | C# |
| 3 |  Technologia  | Aplikacja WPF (.NET Framework)  |
| 4 | NuGet   Packages | Newtonsoft.Json |
| 5 | Po��czenie z baz� danych | Entity Framework|
| 6 | IDE | Microsoft Visual Studio 2019 |
| 7 | API Pogoda | https://openweathermap.org/ |

## Funkcjonalno�� aplikacji : 
- mo�liwo�� sprawdzenia pogody oraz jej prognozy dla dowolnego miejsca na Ziemi dzi�ki API udost�pnionemu przez serwis https://openweathermap.org/ oraz zapyta� sieciowych zaimplementowanych w aplikacji 
- wygodny podgl�d wydarze� w kalendarzu dzi�ki zastosowaniu ikonki **Calendar**
- estetyczny podgl�d pogody stworzony z my�l� o wygodzie korzystania
- mo�liwo�� dodawania nowego wydarzenia do bazy danych oraz usuwanie ju� istniej�cych 
- mo�liwo�� dodawania nowego typu wydarzenia oraz usuwania ju� dodanych typ�w

# Aktualna pogoda i jej prognoza 

#### Program pobiera takie informacje o aktualnej pogodzie jak :
- temperatura (w �C)
- wilgotno�� (w %)
- ci�nienie (w hPa)
- pr�dko�� wiatru (w m/s)
- pobierana jest r�wnie� grafika obrazuj�ca opis pogody 

#### Program pobiera takie informacje dla ka�dego dnia prognozy jak:
- dok�adna informacja o dacie (RRRR-MM-DD HH:MM:SS)
- temperatura (jednostka jak wy�ej)
- ci�nienie (jednostka jak wy�ej)
- pr�dko�� wiatru (jednostka jak wy�ej)

![](GitHubImages/gif_1.gif)

# Kontrola poprawno�ci zapyta� wysy�anych na serwer

Program kontroluje, czy zapytanie wysy�ane na serwer odby�o si� poprawnie.

![](GitHubImages/gif_2.gif)

# Dodawanie nowego wydarzenia

#### Przy dodawaniu nowego wydarzenia mo�emy wprowadza� takie jego atrybuty,  jak :
- nazwa 
- data startu
- czas startu 
- data zako�czenia 
- czas zako�czenia 
- notatka 
- typ wydarzenia (wybierany z listy dost�pnych typ�w)

![](GitHubImages/gif_3.gif)

# Usuwanie istniej�cego wydarzenia 

Poni�szy gif obrazuje, jak wygl�da usuwanie wydarzenia 

![](GitHubImages/gif_4.gif)

# Dodawanie nowego typu wydarzenia 

#### Przy dodawaniu nowego typu wydarzenia mo�emy wprowadza� takie jego atrybuty,  jak :
- nazwa 
- kolor na kalendarzu (nie aktywny)
- kolor na li�cie

![](GitHubImages/gif_5.gif)

# Usuwanie istniej�cego typu wydarzenia 

Poni�szy gif obrazuje, jak wygl�da usuwanie typu wydarzenia 

![](GitHubImages/gif_6.gif)
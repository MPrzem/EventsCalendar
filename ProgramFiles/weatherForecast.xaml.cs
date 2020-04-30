using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for weatherForecast.xaml
    /// </summary>
    public partial class weatherForecast : Window
    {
        
        // WebClient dla prognozy pogody 
        public WebClient web2 = new WebClient();

        // Zmienna potrzebna do pobrania nazwy miasta z głównego okienka
        public string cityNameFromFirstPage { get; set; }

        // Zmienna potrzebna do przechodzenia do kolejnych stron prognozy pogody
        public int tmpInt { get; set; }

        /// <summary>
        /// Ustawienie odpowiednich atrybutów obiektu klasy przy użyciu 
        /// konstruktora tej klasy 
        /// </summary>

        public weatherForecast(string passCityName)
        {
            // Przypisanie odpowiednim zmiennym w klasie odpowiednich wartosci
            this.cityNameFromFirstPage = passCityName;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.tmpInt = 1;

            InitializeComponent();

            // Żeby uniknąć sytuacji niepożądanej -> zamykamy okno główne, a okno "dziecko" (child window) zostaje 
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
                mainWindow.Closed += (s, e) => Close();

            // Uruchomienie metody pobierającej prognozę pogody
            getForecast(cityNameFromFirstPage,web2,tmpInt);
        }

        /// <summary>
        /// Pobieranie prognozy pogody za pomocą API ze strony OpenWeather, 
        /// użycie JsonConvert do konwersji między typami JSON, a .Net 
        /// </summary>
        void getForecast(string cityNameFromFirstPage,WebClient web,int tmpInt)
        {

                // ApiKey na stałe oraz pełny link z apiKey oraz miastem
                string apiKey = "8b7c4545e3874014261a301b43f9d144";
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}", cityNameFromFirstPage, apiKey);

                // Obiekt klasy handleWrongCitynames posiadający metodę do sprawdzania poprawności działania zapytania wysyłanego do serwera 
                handleWrongCitynames tmp = new handleWrongCitynames();

                // Obsluga bledu 404 oraz innych  innych wynikajacych ze źle wpisanego miasta miasta lub jego braku wpisania 
                bool decision = tmp.checkUrl(url);

                if (decision == true)
                {
                    MessageBox.Show("Prawdopodobnie pomyliłeś się przy wpisywaniu nazwy miasta ! \n Spróbuj ponownie");
                    cityTextBox.Text = "Miasto : " + "Error 404";
                }

                else
                {
                    cityTextBox.Text="Miasto : " + cityNameFromFirstPage;

                        var json = web.DownloadString(url);

                        // Użycie JsonConvert
                        var result = JsonConvert.DeserializeObject<weatherforecast.RootObject>(json);

                        weatherforecast.RootObject outPut = result;

                        // Zwracanie danych na temat prognozy pogody dla konkretnej daty 
                        // za pomocą odnoszenia się do odpowiedniego elementu w liście 

                        string forecastDate = outPut.list[tmpInt].dt_txt;
                        string forecastTemperature = string.Format("{0}", Math.Round(outPut.list[tmpInt].main.temp - 273, 2));
                        string forecastPressure = string.Format("{0}", outPut.list[tmpInt].main.pressure);
                        string forecastWind = string.Format("{0}", outPut.list[tmpInt].wind.speed);
                        forecast1TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        tmpInt += 1;

                        forecastDate = outPut.list[tmpInt].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[tmpInt].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[tmpInt].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[tmpInt].wind.speed);
                        forecast2TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        tmpInt += 1;        

                        forecastDate = outPut.list[tmpInt].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[tmpInt].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[tmpInt].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[tmpInt].wind.speed);
                        forecast3TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        tmpInt += 1;

                        forecastDate = outPut.list[tmpInt].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[tmpInt].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[tmpInt].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[tmpInt].wind.speed);
                        forecast4TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        tmpInt += 1;    

                        forecastDate = outPut.list[tmpInt].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[tmpInt].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[tmpInt].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[tmpInt].wind.speed);
                        forecast5TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        tmpInt += 1;
                }
            
        }

        /// <summary>
        /// Milisekundy na typ date, okazuje sie po zrozumieniu Api,
        /// ze chyba funkcja ta jest jednak niepotrzebna
        /// </summary>
        DateTime getDate(double millisecound)
        {

            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }

        /// <summary>
        /// Metoda zamykająca okno z prognoza pogody
        /// </summary>
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda wyświetlająca kolejne daty prognozy pogody w bieżącym oknie 
        /// </summary>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // "Przechodzenie" za pomocą operacji modulo między 0 a 39 dostepnymi elementami typu pogoda w tablicy prognozy pogody
            tmpInt += 5;
            tmpInt = tmpInt % 35;

            getForecast(cityNameFromFirstPage, web2, tmpInt);
        }
    }
}

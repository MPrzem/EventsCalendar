﻿using System;
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

        /// <summary>
        /// Ustawienie odpowiednich atrybutów obiektu klasy przy użyciu 
        /// konstruktora tej klasy 
        /// </summary>

        public weatherForecast(string passCityName)
        {
            // Przypisanie odpowiednim zmiennym w klasie odpowiednich wartosci
            this.cityNameFromFirstPage = passCityName;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            // Żeby uniknąć sytuacji niepożądanej -> zamykamy okno główne, a okno "dziecko" (child window) zostaje 
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
                mainWindow.Closed += (s, e) => Close();

            // Uruchomienie metody pobierającej prognozę pogody
            getForecast(cityNameFromFirstPage,web2);
        }

        /// <summary>
        /// Pobieranie prognozy pogody za pomocą API ze strony OpenWeather, 
        /// użycie JsonConvert do konwersji między typami JSON, a .Net 
        /// </summary>
        void getForecast(string cityNameFromFirstPage,WebClient web)
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

                        // Index [7]
                        string forecastDate = outPut.list[7].dt_txt;
                        string forecastTemperature = string.Format("{0}", Math.Round(outPut.list[7].main.temp - 273, 2));
                        string forecastPressure = string.Format("{0}", outPut.list[7].main.pressure);
                        string forecastWind = string.Format("{0}", outPut.list[7].wind.speed);
                        forecast1TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        // end

                        // Index [15]
                        forecastDate = outPut.list[15].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[15].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[15].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[15].wind.speed);
                        forecast2TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        // end

                        // Index [23]
                        forecastDate = outPut.list[23].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[23].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[23].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[23].wind.speed);
                        forecast3TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        // end

                        // Index [31]
                        forecastDate = outPut.list[31].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[31].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[31].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[31].wind.speed);
                        forecast4TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        // end

                        // Index [39]
                        forecastDate = outPut.list[39].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(outPut.list[39].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", outPut.list[39].main.pressure);
                        forecastWind = string.Format("{0}", outPut.list[39].wind.speed);
                        forecast5TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                        // end
                }
            
        }

        /// <summary>
        /// Milisekundy na date, okazuje sie po zrozumieniu klasy wygenerowanej z neta 
        /// Json to C# , ze chyba funkcja ta jest jednak niepotrzebna
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
    }
}

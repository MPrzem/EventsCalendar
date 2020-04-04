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
    /// Logika interakcji dla klasy weatherForecast.xaml
    /// </summary>
    public partial class weatherForecast : Window
    {
        public WebClient web2 = new WebClient();

        // Zmienna potrzebna do przekazania nazwy miasta z poprzedniego okienka 
        public string cityNameFromFirstPage { get; set; }

        public weatherForecast(string passCityName)
        {
            this.cityNameFromFirstPage = passCityName;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            // Żeby uniknąć sytuacji, zamykamy okno główne, a child okno zostaje 

            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
                mainWindow.Closed += (s, e) => Close();

            getForecast(cityNameFromFirstPage,web2);

        }

        /// <summary>
        /// Pobieranie prognozy pogody API , Json , OpenWeather
        /// </summary>

        void getForecast(string cityNameFromFirstPage,WebClient web)
        {

                // ApiKey na stałe
                string apiKey = "8b7c4545e3874014261a301b43f9d144";

                // Pełny link z apiKey oraz miastem
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}", cityNameFromFirstPage, apiKey);

                handleWrongCitynames tmp = new handleWrongCitynames();

                // Obsluga bledu 404
                bool decision = tmp.checkUrl(url);
                //bool decision = false;

                if (decision == true)
                {
                    MessageBox.Show("Prawdopodobnie pomyliłeś się przy wpisywaniu nazwy miasta ! \n Spróbuj ponownie");
                    cityTextBox.Text = "Miasto : " + "Error 404";
                }

                else
                {
                    cityTextBox.Text="Miasto : " + cityNameFromFirstPage;

                        var json = web.DownloadString(url);

                        // Json Conventer i kawał super roboty, który odwala w poniższej linijce
                        var result = JsonConvert.DeserializeObject<weatherforecast.RootObject>(json);

                        weatherforecast.RootObject outPut = result;

                        // Zwracanie danych na temat prognozy pogody 

                        string forecastDate = result.list[7].dt_txt;
                        string forecastTemperature = string.Format("{0}", Math.Round(result.list[7].main.temp - 273, 2));
                        string forecastPressure = string.Format("{0}", result.list[7].main.pressure);
                        string forecastWind = string.Format("{0}", result.list[7].wind.speed);
                        forecast1TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";

                        forecastDate = result.list[15].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(result.list[15].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", result.list[15].main.pressure);
                        forecastWind = string.Format("{0}", result.list[15].wind.speed);
                        forecast2TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";

                        forecastDate = result.list[23].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(result.list[23].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", result.list[23].main.pressure);
                        forecastWind = string.Format("{0}", result.list[23].wind.speed);
                        forecast3TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";

                        forecastDate = result.list[31].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(result.list[31].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", result.list[31].main.pressure);
                        forecastWind = string.Format("{0}", result.list[31].wind.speed);
                        forecast4TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";

                        forecastDate = result.list[39].dt_txt;
                        forecastTemperature = string.Format("{0}", Math.Round(result.list[39].main.temp - 273, 2));
                        forecastPressure = string.Format("{0}", result.list[39].main.pressure);
                        forecastWind = string.Format("{0}", result.list[39].wind.speed);
                        forecast5TextBox.Text = forecastDate + "  ;  Temperatura: " + forecastTemperature + " °C" + "  ;  Ciśnienie: " + forecastPressure + " hPa" + "  ;  Wiatr: " + forecastWind + " m/s";
                    
                }
            
        }

        /// <summary>
        /// Milisekundy na date, okazuje sie po zrozumieniu jsona z forecast ze chyba niepotrzebny 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            GetProperlyDirectory();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            EventsDBContext db = new EventsDBContext();
            var contents = db.EventsTables;
            foreach (var item in contents)
                ;//label.Content = item.Day;
        }
        /// <summary>
        /// Jedyne co tu sie dzieje to pobranie rekordow(wszystkich=jednego)
        /// z BD przez entity po nacisnieciu przycisku
        /// </summary>

        private void GetProperlyDirectory()
        {
            string Path = Environment.CurrentDirectory;
            string[] appPath = Path.Split(new string[] { "bin" }, StringSplitOptions.None);
            AppDomain.CurrentDomain.SetData("DataDirectory", appPath[0]);

        }

        /// <summary>
        /// Przycisk do wyswietlania pogody 
        /// </summary>

        private void ShowWeather_Click(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

        /// <summary>
        /// Pobieranie pogody API , Json , OpenWeather
        /// </summary>

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string cityName = cityTextBox.Text.ToString();

                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?appid=8b7c4545e3874014261a301b43f9d144&q={0}",cityName);

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<weatherinfo.root>(json);

                weatherinfo.root outPut = result;

                string temperature = string.Format("{0}", Math.Round(outPut.main.temp-273,2));
                string humidity = string.Format("{0}",outPut.main.humidity);
                string pressure = string.Format("{0}", outPut.main.pressure);
                string windSpeed = string.Format("{0}", outPut.wind.speed);

                temperatureTextBox.Text=temperature+ " °C";
                humidityTextBox.Text = humidity + " %";
                pressureTextBox.Text = pressure + " hPa";
                windSpeedTextBox.Text = windSpeed + " m/s";
            }
        }

        /// <summary>
        /// Text Box City 
        /// </summary>

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
///Ogarniete dodanie Entity, standardowego uzytkownika, nadanie mu prawa do INSERT,SELECT,DELETE,UPDATE do tabeli events. Teraz bedzie jazda z zalogowaniem sie do niego.
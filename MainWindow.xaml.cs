﻿using System;
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
using System.Drawing;


namespace Calendar
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// WebClient dla aktualnej pogody 
        /// </summary>
        public WebClient web = new WebClient();
        public EventsCollections collections = new EventsCollections();


        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            GetProperlyDirectory();
            if (CalendarContol.SelectedDate == null)
                CalendarContol.SelectedDate = DateTime.Now;
                (new Window1(CalendarContol.SelectedDate)).Show();
            this.DataContext = this.collections ;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

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
            getWeather(web);
        }

        /// <summary>
        /// Pobieranie pogody API , Json , OpenWeather
        /// </summary>

        void getWeather(WebClient web)
        {
            handleWrongCitynames tmp = new handleWrongCitynames ();

            // CityName z TextBoxa , apiKey na stałe
            string cityName = cityTextBox.Text.ToString();
            string apiKey = "8b7c4545e3874014261a301b43f9d144";

            // Pełny link z apiKey oraz miastem
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?appid={0}&q={1}", apiKey, cityName);

            // Obsługa błędu 404 oraz innych wynikajacych ze źle wpisanego miasta , a wlasciwie jego nazwy 
            bool decision = tmp.checkUrl(url);

            if (decision == true)
            {
                MessageBox.Show("Prawdopodobnie pomyliłeś się przy wpisywaniu nazwy miasta ! \nSpróbuj ponownie");
            }

            else
            {
                    var json = web.DownloadString(url);

                    // Json Conventer i kawał super roboty, który odwala w poniższej linijce
                    var result = JsonConvert.DeserializeObject<weatherinfo.root>(json);

                    weatherinfo.root outPut = result;

                    // Wyświetlanie danych o pogodzie
                    string temperature = string.Format("{0}", Math.Round(outPut.main.temp - 273, 2));
                    string humidity = string.Format("{0}", outPut.main.humidity);
                    string pressure = string.Format("{0}", outPut.main.pressure);
                    string windSpeed = string.Format("{0}", outPut.wind.speed);

                    temperatureTextBox.Text = temperature + " °C";
                    humidityTextBox.Text = humidity + " %";
                    pressureTextBox.Text = pressure + " hPa";
                    windSpeedTextBox.Text = windSpeed + " m/s";

                    // Walka z wyświetlaniem obrazka pogody

                    string imageId = string.Format("{0}", outPut.weather[0].icon);
                    string fullImageUrl = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageId);

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(fullImageUrl);
                    bitmap.EndInit();

                    weatherLogo.Source = bitmap;
            }
        }

        /// <summary>
        /// City Text Box , niechcacy kliknałem , po usunieciu tego fragmentu nie kompiluje sie :-( , jakis problem z visualem ???
        /// mimo ze usuwam rowniez z xmla fragment kodu dotyczacy tego text boxa
        /// </summary>

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Otwieranie nowegho okna z prognoza pogody
        /// </summary>

        private void ShowForecast_Click(object sender, RoutedEventArgs e)
        {
            // Zmienna potrzebna do przekazania nazwy miasta do nowego okienka
            string cityName = cityTextBox.Text.ToString();

                // Tworzenie nowego okienka
                weatherForecast sW = new weatherForecast(cityName);
                sW.Show();      
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            collections.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();
            //context.EventsTypes.Add(new EventsTypes() { Color1 = "Blue", Color2 = "Blue", Id = 1, Name = "Typ1" });
           // context.SaveChanges();
           /// context.Events.Add(new Events() { Name = "Takietam", Id = 1, StartDate = new DateTime(2019, 12, 11), EndDate = new DateTime(), TypeID = 1, Note= "SASDasd" });
            //context.SaveChanges();
            DateTime date = new DateTime(2019, 12, 11);
            var akuku  = context.Events.Where(s => s.StartDate ==date);
            collections.FillDailyEvents(akuku);
            
         }

        private void EventsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            collections.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();

            var calendar = sender as System.Windows.Controls.Calendar;
               

                            var akuku = context.Events.Where(s => s.StartDate == calendar.SelectedDate);
            collections.FillDailyEvents(akuku);

        }
    }
}
///Ogarniete dodanie Entity, standardowego uzytkownika, nadanie mu prawa do INSERT,SELECT,DELETE,UPDATE do tabeli events. Teraz bedzie jazda z zalogowaniem sie do niego.
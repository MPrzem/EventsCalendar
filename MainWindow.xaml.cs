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
                label.Content = item.Day;
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
    }
}
///Ogarniete dodanie Entity, standardowego uzytkownika, nadanie mu prawa do INSERT,SELECT,DELETE,UPDATE do tabeli events. Teraz bedzie jazda z zalogowaniem sie do niego.
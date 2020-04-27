using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        CalendarEvent calendarEvent;
        TypeProperties typeproperties;
        IndividualEventProperties individualEventProperties;
        System.Windows.Controls.Calendar calendar;
        ObservableCollection<TypeProperties> types;
        Action<DateTime?> refresh;
        public Window1(DateTime? selectedDate, EventsCollections collections, System.Windows.Controls.Calendar _calendar, Action<DateTime?> _refresh)
        {
            typeproperties = new TypeProperties("Blue", "Blue", "Typ Wydarzenia");
            individualEventProperties = new IndividualEventProperties("Nazwa", new DateTime(), new DateTime(),"Notka");
            calendarEvent = new CalendarEvent(typeproperties, individualEventProperties);
            this.DataContext = calendarEvent;
            calendar = _calendar;
            InitializeComponent();
            startpicker.SelectedDate = selectedDate;
            endpicker.SelectedDate = selectedDate;
            refresh = _refresh;
            types = new ObservableCollection<TypeProperties>();



        }

        private DateTime Add_Time(string uiText, DateTime? date)
        {

            int Hours = 0;
            int Minutes = 0;


            if (uiText[2] == ':')
            {
                Hours = Int32.Parse(uiText.Substring(0, 2));
                string _minutes = uiText.Substring(3, uiText.Length - 3);
                Minutes = Int32.Parse(_minutes);
            }
            else if (uiText[1] == ':')
            {
                Hours = Int32.Parse(uiText.Substring(0, 1));
                Minutes = Int32.Parse(uiText.Substring(2, uiText.Length-2));
            }

            DateTime dateTime = new DateTime(individualEventProperties.TimeStart.Ticks);

            dateTime=individualEventProperties.TimeStart.AddHours(Hours);
            dateTime =individualEventProperties.TimeStart.AddMinutes(Minutes);

            return dateTime;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

BazaDanychEntities context = new BazaDanychEntities();


            bool refreshRequired = calendar.SelectedDate == calendarEvent.indiv_prop.TimeStart;
            context.Events.Add(new Events() { Name = calendarEvent.indiv_prop.Name, StartDate = Add_Time(czasStartu.Text, individualEventProperties.TimeStart), EndDate = Add_Time(czasKonca.Text, individualEventProperties.TimeEnd), TypeID = 2, Note = calendarEvent.indiv_prop.Note });
            context.SaveChanges();
            if(refreshRequired)
            refresh(calendar.SelectedDate);

        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            BazaDanychEntities context = new BazaDanychEntities();
            var akuku = context.EventsTypes;

            akuku.ToList().ForEach(tmfunc);



        }
        void tmfunc(EventsTypes s)
        {
            types.Add(new TypeProperties(s.Color1, s.Color2, s.Name));
        
    }
        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            types.Clear();
            BazaDanychEntities context = new BazaDanychEntities();
            var akuku = context.EventsTypes;

        akuku.ToList().ForEach(tmfunc);
            typessControl.ItemsSource = types;
    }
    }
}

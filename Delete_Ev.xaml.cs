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
    /// Logika interakcji dla klasy Delete_Ev.xaml
    /// </summary>
    public partial class Delete_Ev : Window

    {/// <summary>
    /// Klasa zawierajaca obie kolekcje typów wydarzeń oraz samych wydarzeń, kolekcja typów wydarzeń nie była potrzebna dotychczas.  
    /// </summary>
        EventsCollections collections = new EventsCollections();

        /// <summary>
        /// Odswieża liste wydarzeń dla danego dnia.
        /// </summary>
        /// <param name="date"></param>
        public void refresh_Event_list(DateTime? date)
        {

            collections.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();

            var akuku = context.Events.Where(s => s.StartDate == date);
            var akukudebug = akuku.ToList();
            collections.FillDailyEvents(akuku);
        }

        /// <summary>
        /// Konstruktor Okna. Kolekcje dodane do DataContextu.
        /// </summary>
        public Delete_Ev()
        {
            InitializeComponent();
            this.DataContext = collections;
        }
        /// <summary>
        /// Reakcja na zmiane wybranej daty. Odswieżenie listy wydarzeń.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void startpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datepicker = sender as System.Windows.Controls.DatePicker;

            refresh_Event_list(datepicker.SelectedDate);
        }
        /// <summary>
        /// Rekacja na kliknięcie przycisku usuwania. Usuwanie odbywa sie tylko gdy wybrano wydarzenie. Wydarzenie usuwane za pomocą kwerendy wyszukującej je po ID. 
        /// Po usunieciu wybierane jest poprzednie wydarzenie. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            if (EventsListBox.SelectedItem != null)
            {
                int index = EventsListBox.SelectedIndex;
                CalendarEvent selectedItem = (CalendarEvent)EventsListBox.SelectedItem;

                BazaDanychEntities context = new BazaDanychEntities();

                const string query = "DELETE FROM [dbo].[Events] WHERE [id]={0}";
                var rows = context.Database.ExecuteSqlCommand(query, selectedItem.indiv_prop.ID);
                context.SaveChanges();
                refresh_Event_list(startpicker.SelectedDate);
                if(index>0)
                EventsListBox.SelectedItem = EventsListBox.Items[index - 1];

            }
        }
    }
}

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

    {
        EventsCollections collections = new EventsCollections();

        public void refresh_Event_list(DateTime? date)
        {

            collections.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();

            var akuku = context.Events.Where(s => s.StartDate == date);
            var akukudebug = akuku.ToList();
            collections.FillDailyEvents(akuku);
        }


        public Delete_Ev()
        {
            InitializeComponent();
            this.DataContext = collections;
        }

        private void startpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datepicker = sender as System.Windows.Controls.DatePicker;

            refresh_Event_list(datepicker.SelectedDate);
        }

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

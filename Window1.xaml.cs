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
        public Window1(DateTime? selectedDate)
        {
            typeproperties = new TypeProperties("Blue", "Blue", "Typ Wydarzenia");
            individualEventProperties = new IndividualEventProperties("Nazwa", new DateTime(), new DateTime(),"Notka");
            calendarEvent = new CalendarEvent(typeproperties, individualEventProperties);
            this.DataContext = calendarEvent;
            InitializeComponent();
            startpicker.SelectedDate = selectedDate;
            endpicker.SelectedDate = selectedDate;





        }


    }
}

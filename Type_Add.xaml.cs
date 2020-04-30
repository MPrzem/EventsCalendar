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
    /// Logika interakcji dla klasy Type_Add.xaml
    /// </summary>
    public partial class Type_Add : Window
    {
        public Type_Add()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BazaDanychEntities context = new BazaDanychEntities();
            context.EventsTypes.Add(new EventsTypes() { Name = type_name.Text, Color1 = cp1.SelectedColorText, Color2 = cp2.SelectedColorText });
            context.SaveChanges();




        }
    }
}

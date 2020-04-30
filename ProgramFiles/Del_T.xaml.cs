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
    /// Logika interakcji dla klasy Del_Ev.xaml
    /// </summary>
    public partial class Del_Ev : Window
    {
        /// <summary>
        /// Kolekcja z zaimplementowanym INotifyPropertyChanged wypełniana z bazy danych
        /// </summary>
        ObservableCollection<TypeProperties> types = new ObservableCollection<TypeProperties>();
        /// <summary>
        /// Konstruktor dodający kolekcje do DataContextu
        /// </summary>
        public Del_Ev()
        {
            InitializeComponent();
            ComboboxRefresh();
            this.DataContext = types;
        }
        /// <summary>
        /// Metoda odswieżająca liste typów w Comboboxie
        /// </summary>
        private void ComboboxRefresh()
        {
            types.Clear();
            BazaDanychEntities context = new BazaDanychEntities();
            var akuku = context.EventsTypes;
            akuku.ToList().ForEach(s => types.Add(new TypeProperties(s.Color1, s.Color2, s.Name, s.Id)));

        }
        /// <summary>
        /// Reakcja na event rozwiniecia listy ComboBoxa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void typessControl_DropDownOpened(object sender, EventArgs e)
        {
            ComboboxRefresh();
        }
        /// <summary>
        /// Usunięcie typu wydarzenia. Za pomocą kwerendy, przed usunięciem typu usuwane są wszystkie wydarzenia z nim stowarzyszone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (typessControl.SelectedItem != null)
            {
                int index = typessControl.SelectedIndex;
                TypeProperties selectedItem = (TypeProperties)typessControl.SelectedItem;

                BazaDanychEntities context = new BazaDanychEntities();
                const string query1 = "DELETE FROM [dbo].[Events] WHERE [TypeID]={0}";
                const string query = "DELETE FROM [dbo].[EventsTypes] WHERE [id]={0}";
                context.Database.ExecuteSqlCommand(query1, selectedItem.ID);
                context.Database.ExecuteSqlCommand(query, selectedItem.ID);
                context.SaveChanges();
                ComboboxRefresh();
                if (index > 0)
                    typessControl.SelectedItem = typessControl.Items[index - 1];

            }
        }
    }
}

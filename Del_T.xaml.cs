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

        ObservableCollection<TypeProperties> types = new ObservableCollection<TypeProperties>();

        public Del_Ev()
        {
            InitializeComponent();
            ComboBox_ContextMenuOpening();
            this.DataContext = types;
        }

        private void ComboBox_ContextMenuOpening()
        {
            types.Clear();
            BazaDanychEntities context = new BazaDanychEntities();
            var akuku = context.EventsTypes;
            akuku.ToList().ForEach(tmfunc);

        }
        void tmfunc(EventsTypes s)
        {
            types.Add(new TypeProperties(s.Color1, s.Color2, s.Name, s.Id));
        }

        private void typessControl_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox_ContextMenuOpening();
        }

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
                ComboBox_ContextMenuOpening();
                if (index > 0)
                    typessControl.SelectedItem = typessControl.Items[index - 1];

            }
        }
    }
}

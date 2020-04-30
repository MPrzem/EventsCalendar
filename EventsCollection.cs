using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Calendar
{
    public class CalendarEvent
    {
        private TypeProperties _typ_prop;
        public TypeProperties typ_prop { get { return _typ_prop; } set { _typ_prop = value; } }
        public IndividualEventProperties indiv_prop { get { return _indiv_prop; } set { _indiv_prop=value; } }
        private IndividualEventProperties _indiv_prop;// Moze tu powinny być interfejsy ale chyba przerost formy nad treścią bla bla bla
        public CalendarEvent(TypeProperties typP,IndividualEventProperties indivP)
        {
            typ_prop = typP;
            indiv_prop = indivP;

        }

    }
    public class TypeProperties : INotifyPropertyChanged///Obiekt tej kasy tworzony na podstawie typów wydarzen zdefiniowanych przez użytkownika
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string colorOnCalendar;
        private string colorOnList;
        private string typeName;
        public int ID;


        public String ColorOnCalendar { get { return colorOnCalendar; } set { colorOnCalendar = value; OnPropertyChanged(); } }
        public String ColorOnList { get { return colorOnList; } set { colorOnList = value; OnPropertyChanged(); } }
        public String TypeName { get { return typeName; } set { typeName = value; OnPropertyChanged(); } }

        public TypeProperties(string _colorOnCalendar, string _colorOnList, string _typeName,int id)
        {
            ColorOnCalendar = _colorOnCalendar;
            ColorOnList = _colorOnList;
            TypeName = _typeName;
            ID = id;
        }

    }
    public class IndividualEventProperties : INotifyPropertyChanged///Obiekt tej kasy tworzony na podstawie typów wydarzen zdefiniowanych przez użytkownika
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }///Te pobierane z bazy danych
        private string name;
        private DateTime timeStart;
        private DateTime timeEnd;
        private string note;
        private int _ID;
        public String Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        public DateTime TimeStart { get { return timeStart; } set { timeStart = value; OnPropertyChanged(); } }
        public DateTime TimeEnd { get { return timeEnd; } set { timeEnd = value; OnPropertyChanged(); } }
        public String Note { get { return note; } set { note = value; OnPropertyChanged(); } }

        public int ID { get { return _ID; } private set { _ID = value; OnPropertyChanged(); } }
        public IndividualEventProperties(string name,DateTime start,DateTime end, string Note, int _id)
        {
            Name =name;
            TimeStart = start;
            TimeEnd = end;
            this.Note = Note;
            ID = _id;

        }


    }

    public class EventsCollections
    {
        ////Zeby nie pobierac eventów z całej histori weszechswiata pobierane beda po wybraniu konkretnego dnia
        private ObservableCollection<CalendarEvent> _DailyEventList = new ObservableCollection<CalendarEvent>();
        private ObservableCollection<TypeProperties> _TypesList = new ObservableCollection<TypeProperties>(); // Pobierane z tabeli typów i dodawane do niej w UI

        public ObservableCollection<CalendarEvent> DailyEventList { get { return _DailyEventList; } set { value = _DailyEventList; }}
        public ObservableCollection<TypeProperties> TypesList { get { return _TypesList; } set { value = _TypesList; } }

        public void FillDailyEvents(IQueryable<Events> events)
        {
            events.ToList().ForEach(AddDailyEvent);

        }
      public void AddDailyEvent(Events indiv_ev_info) // Nie ma co sie silić na stosowanie interfejsow, nie wyobrazam sobie rozbudowy. Raczej typy beda roznic sie tylko kolorami
        {
            IndividualEventProperties indiv_tmp = new IndividualEventProperties("Name: " + indiv_ev_info.Name, indiv_ev_info.StartDate, indiv_ev_info.EndDate, "Note: " + indiv_ev_info.Note, indiv_ev_info.Id);
            TypeProperties type_tmp = new TypeProperties(indiv_ev_info.EventsTypes.Color1, indiv_ev_info.EventsTypes.Color2, "Name: " + indiv_ev_info.EventsTypes.Name,0); // Jego trzeba generować inaczej
           
            DailyEventList.Add(new CalendarEvent(type_tmp, indiv_tmp));
        }

      public void ClearDailyEvents()
        {
            DailyEventList.Clear();
        }
       
        
        
        
        /// <summary>
         /// indexOfType indexOfType in TypesList( W przy tworzeniu maja byc wyswietlane nazwy typów wg kolejnosci z TypesList(właściwośc TypeName)
         /// </summary>
         /// <param name="indexOfType"></param>
        public void MakeEvent(ushort indexOfType, IndividualEventProperties properties)
        {
            /// Stworzyć, dodać do bazy danych za pomocą entity i wstawić do DailiEventList
            
        }




        ///W pliku xaml Dodać datatemplate https://www.youtube.com/watch?v=YW3gq-zyzoE&t=623s 
        /// 

    }

}

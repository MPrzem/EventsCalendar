using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class CalendarEvent
    {
        public TypeProperties typ_prop;
        public IndividualEventProperties indiv_prop;// Moze tu powinny być interfejsy ale chyba przerost formy nad treścią bla bla bla
        public CalendarEvent(TypeProperties typP,IndividualEventProperties indivP)
        {
            typ_prop = typP;
            indiv_prop = indivP;

        }

    }
    public class TypeProperties///Obiekt tej kasy tworzony na podstawie typów wydarzen zdefiniowanych przez użytkownika
    {
        public String ColorOnCalendar { get; private set; }
        public String ColorOnList { get; private set; }
        public String TypeName { get; private set; }

        public TypeProperties(string colorOnCalendar, string colorOnList, string typeName)
        {
            ColorOnCalendar = colorOnCalendar;
            ColorOnList = colorOnList;
            TypeName = typeName;
        }

    }
    public class IndividualEventProperties///Te pobierane z bazy danych
    {
        public String Name { get; private set; }
        public DateTime TimeStart { get; private set; }
        public DateTime TimeEnd { get; private set; }
        public String Note { get; private set; }

        public IndividualEventProperties(string name,DateTime start,DateTime end, string Note)
        {
            Name = name;
            TimeStart = start;
            TimeEnd = end;
            this.Note = Note;

        }


    }

    public class EventsCollections
    {
        ////Zeby nie pobierac eventów z całej histori weszechswiata pobierane beda po wybraniu konkretnego dnia
        ObservableCollection<CalendarEvent> DailyEventList = new ObservableCollection<CalendarEvent>();
        ObservableCollection<TypeProperties> TypesList = new ObservableCollection<TypeProperties>(); // Pobierane z tabeli typów i dodawane do niej w UI


        public void FillDailyEvents(IQueryable<Events> events)
        {
            events.ToList().ForEach(AddDailyEvent);

        }
      public void AddDailyEvent(Events indiv_ev_info) // Nie ma co sie silić na stosowanie interfejsow, nie wyobrazam sobie rozbudowy. Raczej typy beda roznic sie tylko kolorami
        {
            IndividualEventProperties indiv_tmp = new IndividualEventProperties(indiv_ev_info.Name, indiv_ev_info.StartDate, indiv_ev_info.EndDate, indiv_ev_info.Note);
            TypeProperties type_tmp = new TypeProperties(indiv_ev_info.EventsTypes.Color1, indiv_ev_info.EventsTypes.Color2, indiv_ev_info.EventsTypes.Name);
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class CalendarEvents
    {
        public TypeProperties typ_prop;
        public IndividualEventProperties indiv_prop;// Moze tu powinny być interfejsy ale chyba przerost formy nad treścią bla bla bla
        public CalendarEvents(TypeProperties typP,IndividualEventProperties indivP)
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


    }

    public class EventsCollections
    {
        ////Zeby nie pobierac eventów z całej histori weszechswiata pobierane beda po wybraniu konkretnego dnia
        ObservableCollection<CalendarEvents> DailyEventList;
        ObservableCollection<TypeProperties> TypesList; // Pobierane z tabeli typów i dodawane do niej w UI

      public void GenerateDailyEventsFromDB()
        {
            /* 
             * 1. Pobierz jeden rekord z tablei wydarzeń(Typ eventu jako klucz głowny z innej tabeli typów)
             * 2. Pobierz dany typ 
             * 3. Zapakuj to w TypeProperties i IndividualProperties
             * 4. Wstaw do DailiEventsList
             * 5.   Wróc do 1 i powtarzaj do końca danych z D
             */




        }/// <summary>
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

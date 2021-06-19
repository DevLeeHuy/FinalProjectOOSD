using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverTable
{
    class Program
    {
        static void Main(string[] args)
        {
            BookingTable bookingTable = new BookingTable();
            NewsAgency agency1 = new NewsAgency("Table busy is");
            bookingTable.Attach(agency1);
            NewsAgency agency2 = new NewsAgency("Table is not busy is");
            bookingTable.Attach(agency2);
            bookingTable.Table = 1;
            bookingTable.Table = 2;
            Console.ReadLine();
        }
        interface ISubject
        {
            void Attach(IObserver observer);
            void Notify();
        }

        class BookingTable : ISubject
        {
            private List<IObserver> _observers;
            public int Table
            {
                get { return _table; }
                set
                {
                    _table = value;
                    Notify();
                }
            }
            private int _table;
            public BookingTable()
            {
                _observers = new List<IObserver>();
            }
            public void Attach(IObserver observer)
            {
                _observers.Add(observer);
            }
            public void Notify()
            {
                _observers.ForEach(o =>
                {
                    o.Update(this);
                });
            }
        }



        interface IObserver
        {
            void Update(ISubject subject);
        }
        class NewsAgency : IObserver
        {
            public string AgencyName { get; set; }
            public NewsAgency(string name)
            {
                AgencyName = name;
            }
            public void Update(ISubject subject)
            {
                if (subject is BookingTable bookingTable)
                {
                    Console.WriteLine(String.Format("{0} reporting table {1}", AgencyName, bookingTable.Table));
                    Console.WriteLine();
                }
            }
        }


    }
}

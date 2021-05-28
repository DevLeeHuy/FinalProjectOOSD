using System;
using System.Data;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProvider dtpr1 = DataProvider.Instance;
            DataProvider dtpr2 = DataProvider.Instance;
            DataProvider dtpr3 = DataProvider.Instance;
            DataProvider dtpr4 = DataProvider.Instance;
        }
    }
    

    class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance 
        {
            get{ if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set => instance = value; 
        }
        private DataProvider() {
            Console.WriteLine("Init Dataprovider from singleton object...");
        }
        private string ConnectionStr = "...";
        public DataTable ExecuteQuery(string query) { return null; }
    }
}

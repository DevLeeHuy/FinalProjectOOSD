using System;
using System.Collections.Generic;

namespace ObserverPattern.New
{
    class Program
    {
        static void Main(string[] args)
        {
            var Restaurantinformation = new RestaurantInformation("Hadilao","Hongkong Hotpot","District 1","1800","100$");
            var EmailNotifier = new EmailNotifier(Restaurantinformation);
            var PhoneNotifier = new PhoneNotifier(Restaurantinformation);
            var WebNotifier = new WebNotifier(Restaurantinformation);

            Console.WriteLine("_______________Change name of restaurant_______________");
            Restaurantinformation.SetName("Observer Restaurant");

            Console.WriteLine("_______________Detach notify from WEBSITE & change description_______________");
            Restaurantinformation.DetachObserver(WebNotifier);
            Restaurantinformation.SetDescription("Huy Le's restaurant");

            Console.WriteLine("_______________Attach notify from FACEBOOK & change address_______________");
            var FacebookNotifier = new FacebookNotifier(Restaurantinformation);
            Restaurantinformation.SetAddress("District 2");

            Console.ReadKey();
        }
    }

    public abstract class Observer
    {
        protected Subject Subject;
        public abstract void Notify();
    }
    public class Subject
    {
        private readonly List<Observer> _observers = new List<Observer>();

        public void AttachObserver(Observer observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            _observers.ForEach((observer) => observer.Notify());
        }
    }

    public class RestaurantInformation : Subject
    {
        #region Private Properties
        private string _name;
        private string _description;
        private string _address;
        private string _phone;
        private string _price;
        #endregion

        #region GetSetProperties
        public string GetName()
        {
            return _name;
        }

        public void SetName(string value)
        {
            _name = value;
            RestaurantinformationChanged();
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string value)
        {
            _description = value;
            RestaurantinformationChanged();
        }

        public string GetAddress()
        {
            return _address;
        }

        public void SetAddress(string value)
        {
            _address = value;
            RestaurantinformationChanged();
        }
        public string GetPhone()
        {
            return _phone;
        }
        public void SetPhone(string value)
        {
            _phone = value;
            RestaurantinformationChanged();
        }

        public string GetPrice()
        {
            return _price;
        }
        public void SetPrice(string value)
        {
            _price = value;
            RestaurantinformationChanged();
        }

        #endregion

        public RestaurantInformation(string name , string des, string address, string phone ,string price)
        {
            this._name = name;
            this._description = des;
            this._address = address;
            this._phone = phone;
            this._price = price;
        }
        private void RestaurantinformationChanged()
        {
            NotifyObservers();
        }
    }

     public class EmailNotifier : Observer
    {
        public EmailNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify()
        {
            if(Subject is RestaurantInformation Restaurantinformation)
            {
                    Console.WriteLine("Notify all subscribers via EMAIL with new data" +
                                  "\n\tName: {0}" +
                                  "\n\tDescription: {1}" +
                                  "\n\tAddress: {2}"+
                                  "\n\tPhone: {3}" +
                                  "\n\tPrice: {4}", Restaurantinformation.GetName(),
                                  Restaurantinformation.GetDescription(),
                                  Restaurantinformation.GetAddress(),
                                  Restaurantinformation.GetPhone(),
                                  Restaurantinformation.GetPrice());
            }
        }
    }
     public class FacebookNotifier : Observer
    {
        public FacebookNotifier(Subject subject)
        {
            this.Subject = subject;
            this.Subject.AttachObserver(this);
        }

        public override void Notify()
        {
            if (Subject is RestaurantInformation Restaurantinformation)
            {
                Console.WriteLine("Notify all subscribers via FACEBOOK with new data" +
                                  "\n\tName: {0}" +
                                  "\n\tDescription: {1}" +
                                  "\n\tAddress: {2}"+
                                  "\n\tPhone: {3}" +
                                  "\n\tPrice: {4}", Restaurantinformation.GetName(),
                                  Restaurantinformation.GetDescription(),
                                  Restaurantinformation.GetAddress(),
                                  Restaurantinformation.GetPhone(),
                                  Restaurantinformation.GetPrice());
            }
        }
    }
     public class PhoneNotifier : Observer
    {
        public PhoneNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify()
        {
            if (Subject is RestaurantInformation Restaurantinformation)
            {
                Console.WriteLine("Notify all subscribers via PHONE with new data" +
                                  "\n\tName: {0}" +
                                  "\n\tDescription: {1}" +
                                  "\n\tAddress: {2}"+
                                  "\n\tPhone: {3}" +
                                  "\n\tPrice: {4}", Restaurantinformation.GetName(),
                                  Restaurantinformation.GetDescription(),
                                  Restaurantinformation.GetAddress(),
                                  Restaurantinformation.GetPhone(),
                                  Restaurantinformation.GetPrice());
            }
        }
    }
    public class WebNotifier : Observer
    {
        public WebNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify()
        {
            if (Subject is RestaurantInformation Restaurantinformation)
            {
                Console.WriteLine("Notify all subscribers via WEBSITE with new data" +
                                  "\n\tName: {0}" +
                                  "\n\tDescription: {1}" +
                                  "\n\tAddress: {2}"+
                                  "\n\tPhone: {3}" +
                                  "\n\tPrice: {4}", Restaurantinformation.GetName(),
                                  Restaurantinformation.GetDescription(),
                                  Restaurantinformation.GetAddress(),
                                  Restaurantinformation.GetPhone(),
                                  Restaurantinformation.GetPrice());
            }
        }
    }
}

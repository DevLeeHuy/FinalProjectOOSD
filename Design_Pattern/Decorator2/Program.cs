using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IPizza tomato = new TomatoPizza();
            IPizza chicken = new ChickenPizza();

            Console.WriteLine(tomato.doPizza() + $" (cost: {tomato.Cost()})");
            Console.WriteLine(chicken.doPizza() + $" (cost: {chicken.Cost()})");

            // Use Decorator pattern to extend existing pizza dynamically

            // Add pepper to tomato-pizza
            Pepper Pepper = new Pepper(tomato);
            Console.WriteLine(Pepper.doPizza() +$" (cost: {Pepper.Cost()})");

            // Add cheese to tomato-pizza
            Cheese Cheese = new Cheese(tomato);
            Console.WriteLine(Cheese.doPizza() + $" (cost: {Cheese.Cost()})");

            // Add cheese and pepper to tomato-pizza
            // We combine functionalities together easily.
            Cheese Cheese2 = new Cheese(Pepper);
            Console.WriteLine(Cheese2.doPizza() + $" (cost: {Cheese2.Cost()})");
        }
        public interface IPizza
        {
            string doPizza();
            double Cost();
        }

        public class TomatoPizza : IPizza
        {
            private double cost = 5;
            public double Cost()
            {
                return this.cost;
            }

            public string doPizza()
            {
                return "You order a Tomato Pizza";
            }
        }
        public class ChickenPizza : IPizza
        {
            private double cost = 10;

            public double Cost()
            {
                return cost;
            }

            public string doPizza()
            {
                return "You order a Chicken Pizza";
            }
        }
        public abstract class PizzaDecorator : IPizza
        {
        // Reference to component object
            private IPizza mPizza;

            // We initialize a Decorator with existing pizza we need decorate
            public PizzaDecorator(IPizza pizza)
            {
                this.mPizza = pizza;
            }

            public virtual double Cost()
            {
               return mPizza.Cost();
            }

            public virtual string doPizza()
            {
                return mPizza.doPizza();
            }


        }
        public class Cheese : PizzaDecorator
        {
            public Cheese(IPizza pizza) : base(pizza) { }
            private double cost = 5;
            public override double Cost()
            {
                return this.cost + base.Cost();
            }
            public override string doPizza()
            {
                string type = base.doPizza();
                return base.doPizza() + addCheese();
            }
            private string addCheese()
            {
                return "+ Cheese";
            }
        }
        public class Pepper : PizzaDecorator
        {
            public Pepper(IPizza pizza) : base(pizza) { }
            private double cost = 3;
            public override double Cost()
            {
                return this.cost + base.Cost();
            }
            public override string doPizza()
            {
                return base.doPizza() + addPepper();
            }

            private  string addPepper()
            {
                return "+ Pepper";
            }
           
        }

}

}
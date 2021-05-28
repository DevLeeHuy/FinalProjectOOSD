using System;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
     
            var firsttable = new FireworkStick(
                                new Balloon(
                                    new Candle(
                                        new Cake(
                                            new Table()))));
            Console.WriteLine("___________________Table 1_________________\n" +
                              "**INCLUDE:\n"+
                                "\t- Cake\n"+
                                "\t- Candle\n"+
                                "\t- Balloon\n"+
                                "\t- Firework stick\n"+
                                "=====> Total cost: " + firsttable.Cost());

         

            var secondtable = new FireworkStick(
                                new LetterBubble(
                                        new Cake(
                                            new Table())));
            Console.WriteLine("___________________Table 2_________________\n"+
                                "**INCLUDE:\n" +
                                "\t- Cake\n" +
                                "\t- Letter bubble\n"+
                                "\t- Firework stick\n"+
                                "=====> Total cost: "+ secondtable.Cost());
        }
    }

    public interface ITable
    {
        double Cost();
    }
    public abstract class TableDecorator : ITable
    {
        private ITable _table;

        protected TableDecorator(ITable inner)
        {
            this._table = inner;
        }

        public virtual double Cost()
        {
            return _table.Cost();
        }
    }
    public class Table : ITable
    {
        private double cost = 5;
        public double Cost()
        {
            return cost;
        }
    }

     public class Balloon : TableDecorator
    {
        private double cost = 1;
        public Balloon(ITable inner) : base(inner)
        {
        }

        public override double Cost()
        {
            return this.cost + base.Cost();
        }
    }
    public class LetterBubble : TableDecorator
    {
        private double cost = 2;
        public LetterBubble(ITable inner) : base(inner)
        {
        }

        public override double Cost()
        {
            return this.cost + base.Cost();
        }
    }
    public class Cake : TableDecorator
    {
        private double cost = 5;
        public Cake(ITable inner) : base(inner)
        {
        }

        public override double Cost()
        {
            return this.cost + base.Cost();
        }
    }
    public class Candle : TableDecorator
    {
        private double cost = 1.5;
        public Candle(ITable inner) : base(inner)
        {
        }

        public override double Cost()
        {
            return this.cost + base.Cost();
        }
    }
    public class FireworkStick : TableDecorator
    {
        private double cost = 3;
        public FireworkStick(ITable inner) : base(inner)
        {
        }

        public override double Cost()
        {
            return this.cost + base.Cost();
        }
    }
}

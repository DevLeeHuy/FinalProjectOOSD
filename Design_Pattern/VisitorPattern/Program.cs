using System;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookingVisitor table = new TableBookingVisitor();
            IMembership MemberA = new BronzeMember();
            IMembership MemberB = new SilverMember();
            IMembership MemberC = new GoldMember();
            Console.WriteLine("Member A: book a table...");
            Console.Write("Restaurant: ");
            MemberA.accept(table);
            Console.WriteLine("Member B: book a table...");
            Console.Write("Restaurant: ");
            MemberB.accept(table);
            Console.WriteLine("Member C: book a table...");
            Console.Write("Restaurant: ");
            MemberC.accept(table);
        }
    }

    interface IMembership
    {
         string getName();
        void accept(IBookingVisitor v);
    }
    class BronzeMember : IMembership
    {

        public string getName()
        {
            return "Bronze member";
        }
    
        public void accept(IBookingVisitor v)
        {
            v.visitBronzeMember(this);
        }

    }
    class SilverMember : IMembership
    {

        public string getName()
        {
            return "Silver member";
        }
      
        public void accept(IBookingVisitor v)
        {
           v.visitSilverMember(this);
        }
    }
    class GoldMember : IMembership
    {

        public string getName()
        {
            return "Gold member";
        }
      
        public void accept(IBookingVisitor v)
        {
            v.visitGoldMember(this);
        }
    }

    interface IBookingVisitor
    {
        void visitBronzeMember(BronzeMember bronze);
        void visitSilverMember(SilverMember silver);
        void visitGoldMember(GoldMember gold);
    }

    class TableBookingVisitor : IBookingVisitor
    {
        public void visitBronzeMember(BronzeMember bronze)
        {
            Console.WriteLine($"Your table booking get a discount 15% because you are {bronze.getName()}.");
        }

        public void visitGoldMember(GoldMember gold)
        {
            Console.WriteLine($"Your table booking get a discount 20% because you are {gold.getName()}.");
        }

        public void visitSilverMember(SilverMember silver)
        {
            Console.WriteLine($"Your table booking get a discount 30% because you are {silver.getName()}.");
        }
    }
}

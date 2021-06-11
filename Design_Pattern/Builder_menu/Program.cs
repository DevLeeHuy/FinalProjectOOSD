using System;
using System.Collections.Generic;
using System.Linq;

namespace Builder_menu
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuBuilder menuBuilder = new MenuBuilder(new List<IMenuItem>
            {
                new MenuItem("Chicken Sandwich", 6.99),
                new MenuItem("Pizza", 3.99),
                new MenuItem("Salad", 4.99),
            });

            IMenu menu = menuBuilder
                .WithDiscounts(50)
                .WithDailySpecial(new MenuItem("Milk", 0.99, true))
                .Build();

            Console.WriteLine("--------MENU----------");
            foreach (IMenuItem item in menu.Items)
            {
                Console.WriteLine(item);
            }
        }
        public interface IMenu
        {
            IEnumerable<IMenuItem> Items { get; }
        }
        public interface IMenuItem
        {
            string Name { get; }
            double Price { get; }
            bool IsSpecial { get; }
        }
        public class Menu : IMenu
        {
            public IEnumerable<IMenuItem> Items { get; }

            public Menu(IEnumerable<IMenuItem> menuItems)
            {
                Items = menuItems;
            }
        }
        public class MenuBuilder
        {
            private readonly IEnumerable<IMenuItem> _menuItems;

            private bool _withDiscounts;
            private double _discountPercentage;

            private bool _withDailySpecial;
            private IMenuItem _dailySpecialMenuItem;

            public MenuBuilder(IEnumerable<IMenuItem> menuItems)
            {
                _menuItems = menuItems;
            }

            public MenuBuilder WithDiscounts(double discountPercentage)
            {
                _withDiscounts = true;
                _discountPercentage = discountPercentage;

                return this;
            }

            public MenuBuilder WithDailySpecial(IMenuItem dailySpecialMenuItem)
            {
                _withDailySpecial = true;
                _dailySpecialMenuItem = dailySpecialMenuItem;

                return this;
            }

            public IMenu Build()
            {
                IMenu menu = new Menu(_menuItems);

                if (_withDiscounts)
                {
                    menu = new DiscountMenu(menu, _discountPercentage);
                }

                if (_withDailySpecial)
                {
                    menu = new DailySpecialMenu(menu, _dailySpecialMenuItem);
                }

                return menu;
            }
        }
        public class MenuItem : IMenuItem
        {
            public string Name { get; }
            public double Price { get; }
            public bool IsSpecial { get; }

            public MenuItem(string name, double price, bool isSpecial = false)
            {
                Name = name;
                Price = price;
                IsSpecial = isSpecial;
            }

            public override string ToString()
            {
                string specialDisplay = IsSpecial ? "-=- SPECIAL -=- " : string.Empty;
                return $"{specialDisplay}{Name}: {Price:C}";
            }
        }
        public class DailySpecialMenu : IMenu
        {
            private readonly IMenu _menu;
            private readonly IMenuItem _dailySpecialMenuItem;

            public IEnumerable<IMenuItem> Items => _menu.Items.Append(_dailySpecialMenuItem);

            public DailySpecialMenu(IMenu menu, IMenuItem dailySpecialMenuItem)
            {
                _menu = menu;
                _dailySpecialMenuItem = dailySpecialMenuItem;
            }
        }
        public class DiscountMenu : IMenu
        {
            private readonly IMenu _menu;
            private readonly double _discountPercentage;

            public IEnumerable<IMenuItem> Items => _menu.Items.Select(ToDiscountMenuItems);

            public DiscountMenu(IMenu menu, double discountPercentage)
            {
                _menu = menu;
                _discountPercentage = discountPercentage;
            }

            private IMenuItem ToDiscountMenuItems(IMenuItem menuItem)
            {
                return new DiscountMenuItem(menuItem, _discountPercentage);
            }
        }
        public class DiscountMenuItem : IMenuItem
        {
            private readonly IMenuItem _menuItem;
            private readonly double _discountPercentage;

            public double Price => _menuItem.Price * (_discountPercentage / 100);

            public string Name => _menuItem.Name;
            public bool IsSpecial => _menuItem.IsSpecial;

            public DiscountMenuItem(IMenuItem menuItem, double discountPercentage)
            {
                _menuItem = menuItem;
                _discountPercentage = discountPercentage;
            }

            public override string ToString()
            {
                // Lazily copy/pasted from MenuItem.cs
                string specialDisplay = IsSpecial ? "-=- SPECIAL -=- " : string.Empty;
                return $"{specialDisplay}{Name}: {Price:C}";
            }
        }
    }
}

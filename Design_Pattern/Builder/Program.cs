using System;

namespace BuilderPattern
{
  class Program
  {
    static void Main(string[] args)
    {
            Service Service = new Service(4,
                        new Restaurant("Hadilao Hotpot"),
                        "Alacarte",
                        new PriceRange(200,500),
                        new Table("A3-209"));
            Service ServiceByBuilder = new ServiceBuilder().AddDiners(4)
                                                        .AddRestaurant(new Restaurant("Hadilao Hotpot"))
                                                        .AddTable(new Table("A3-209"))
                                                        .AddPriceRange(new PriceRange(200,500))
                                                        .AddCategory("Alacarte")
                                                        .Build();
      Console.WriteLine(Service.ToString());
      Console.WriteLine("-------------SERVICE BY BUILDER-----------------------");
      Console.WriteLine(ServiceByBuilder.ToString());
    }
  }
  class Service {  
    public int NumberOfDiners{ get; set; }
    public Restaurant restaurant { get; set; }
    public string category { get; set; }
    public PriceRange price { get; set; }
    public Table Table { get; set; }

    public Service(int NumberOfDiners,
               Restaurant restaurant, 
               string category, 
               PriceRange price, 
               Table Table)
    {
        this.NumberOfDiners = NumberOfDiners;
        this.restaurant = restaurant;
        this.category = category;
        this.price = price;
        this.Table = Table;
    }

    public override string ToString()
    {
      var content = "";
      content += $"Number of Diners:\t {NumberOfDiners}\n";
      content += $"Restaurant:      \t {restaurant.Name}\n";
      content += $"Form of service: \t {category}\n";
      content += $"Price range:     \t from {price.minimum}$ to {price.maximum}$\n";
      content += $"Table position:  \t {Table.Id}";
      return content;
    }
  }
  public class Restaurant
  {
    public Restaurant(string name)
    {
      Name = name;
    }

    public string Name { get; set; }
  }
  public class PriceRange
  {
    public PriceRange(double from, double to)
    {
      minimum = from;
      maximum = to; 
    }

    public double minimum { get; set; }
    public double maximum { get; set; }
  }
  public class Table
  {
    public Table(string id)
    {
      Id = id;
    }

    public string Id { get; set; }
  }

    interface IServiceBuilder {
      ServiceBuilder AddDiners(int NumberOfDiners);
      ServiceBuilder AddRestaurant(Restaurant restaurant);
      ServiceBuilder AddCategory(string category);
      ServiceBuilder AddPriceRange(PriceRange priceRange);
      ServiceBuilder AddTable(Table table);
      Service Build();
    }
  class ServiceBuilder : IServiceBuilder {
    public int NumberOfDiners{ get; set; }
    public Restaurant restaurant { get; set; }
    public string category { get; set; }
    public PriceRange price { get; set; }
    public Table table { get; set; }

    public ServiceBuilder AddDiners(int NumberOfDiners){
      this.NumberOfDiners = NumberOfDiners;
      return this;
    }
    public  ServiceBuilder AddRestaurant(Restaurant restaurant){
      this.restaurant = restaurant;
      return this;
    }
    public  ServiceBuilder AddCategory(string category){
      this.category = category;
      return this;
    }
     public ServiceBuilder AddPriceRange(PriceRange priceRange){
      this.price = priceRange;
      return this;
     }
    public  ServiceBuilder AddTable(Table table){
      this.table = table;
      return this;
    }
    public  Service Build(){
       return new Service(NumberOfDiners, restaurant, category, price, table);
    }
  }
}

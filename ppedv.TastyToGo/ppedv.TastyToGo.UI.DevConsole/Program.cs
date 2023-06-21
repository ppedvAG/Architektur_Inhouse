using ppedv.TastyToGo.Core;
using ppedv.TastyToGo.Model.DomainModel;

Console.WriteLine("Hello, World!");

string conString = "Server=(localdb)\\MSSQLLOCALDB;Database=TastyToGo_tests;Trusted_Connection=true;";

var repo = new ppedv.TastyToGo.Data.Db.EfRepositoryAdapter(conString);

var orderService = new OrderService(repo);

var bestCustomer = orderService.GetBestPayingCustomer();
Console.WriteLine($"Best Customer: {bestCustomer.Name}");

foreach (var custo in repo.GetAll<Customer>())
{
    Console.WriteLine($"{custo.Name}");
}
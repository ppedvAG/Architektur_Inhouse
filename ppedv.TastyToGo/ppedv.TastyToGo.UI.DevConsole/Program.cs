using Autofac;
using ppedv.TastyToGo.Core;
using ppedv.TastyToGo.Data.Db;
using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;
using System.Reflection;

Console.WriteLine("Hello, World!");

string conString = "Server=(localdb)\\MSSQLLOCALDB;Database=TastyToGo_tests;Trusted_Connection=true;";

//var repo = new ppedv.TastyToGo.Data.Db.EfRepositoryAdapter(conString);
var path = @"C:\Users\ar2\source\repos\ppedvAG\Architektur_Inhouse\ppedv.TastyToGo\ppedv.TastyToGo.Data.Db\bin\Debug\net7.0\ppedv.TastyToGo.Data.Db.dll";

//di per Reflection
//var ass = Assembly.LoadFrom(path);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = Activator.CreateInstance(typeMitRepo,conString) as IRepository;

//di per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<EfRepositoryAdapter>().AsImplementedInterfaces()
                                           .WithParameter("conString", conString);
var container = builder.Build();

var repo = container.Resolve<IRepository>();   
        
var orderService = new OrderService(repo);

var bestCustomer = orderService.GetBestPayingCustomer();
Console.WriteLine($"Best Customer: {bestCustomer.Name}");

foreach (var custo in repo.GetAll<Customer>())
{
    Console.WriteLine($"{custo.Name}");
}
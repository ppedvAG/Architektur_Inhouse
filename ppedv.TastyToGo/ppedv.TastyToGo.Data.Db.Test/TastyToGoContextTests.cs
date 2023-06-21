using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ppedv.TastyToGo.Model;
using ppedv.TastyToGo.Model.DomainModel;
using System.Reflection;

namespace ppedv.TastyToGo.Data.Db.Tests
{
    public class TastyToGoContextTests
    {
        string conString = "Server=(localdb)\\MSSQLLOCALDB;Database=TastyToGo_tests;Trusted_Connection=true;";

        [Fact]
        public void Can_create_Db()
        {
            var con = new TastyToGoContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            result.Should().BeTrue();

            //cleanup con.Database.EnsureDeleted();
        }

        [Fact]
        public void Can_insert_product()
        {
            var con = new TastyToGoContext(conString);
            con.Database.EnsureCreated();
            var prod = new Product() { Name = "Käse", KCal = 4, Price = 12 };

            con.Add(prod);
            var result = con.SaveChanges();

            result.Should().Be(1);
        }

        [Fact]
        public void Can_read_product()
        {
            var prod = new Product() { Name = $"Käse_{Guid.NewGuid()}", KCal = 4, Price = 12 };
            using (var con = new TastyToGoContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(prod);
                con.SaveChanges();
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                loaded.Name.Should().Be(prod.Name);
            }
        }

        [Fact]
        public void Can_update_product()
        {
            var prod = new Product() { Name = $"Käse_{Guid.NewGuid()}", KCal = 4, Price = 12 };
            var newName = $"Wurst_{Guid.NewGuid()}";
            using (var con = new TastyToGoContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(prod);
                con.SaveChanges();
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                loaded.Name = newName;
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                loaded.Name.Should().Be(newName);
            }
        }

        [Fact]
        public void Can_delete_product()
        {
            var prod = new Product() { Name = $"Käse_{Guid.NewGuid()}", KCal = 4, Price = 12 };
            using (var con = new TastyToGoContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(prod);
                con.SaveChanges();
            }

            using (var con = new TastyToGoContext(conString))
            {
                con.Products.Remove(prod);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                loaded.Should().BeNull();
            }
        }


        [Fact]
        public void Can_insert_Order_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter("Id"));
            var order = fix.Build<Order>().Create();

            using (var con = new TastyToGoContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(order);
                con.SaveChanges();
            }

            using (var con = new TastyToGoContext(conString))
            {
                //var loaded = con.Orders.Find(order.Id);
                var loaded = con.Orders.Include(x => x.Customer)
                                       .Include(x => x.OrderItems)
                                       .ThenInclude(x => x.Product)
                                       .FirstOrDefault(x => x.Id == order.Id);
                loaded.Should().BeEquivalentTo(order, x => x.IgnoringCyclicReferences());
            }
        }

        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;

            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }
    }
}
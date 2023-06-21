using FluentAssertions;
using ppedv.TastyToGo.Model;

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

    }
}
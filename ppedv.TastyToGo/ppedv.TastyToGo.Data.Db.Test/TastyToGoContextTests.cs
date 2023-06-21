using ppedv.TastyToGo.Model;
using System.Linq.Expressions;

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

            Assert.True(result);

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

            Assert.Equal(1, result);
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
                Assert.Equal(prod.Name, loaded.Name);
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
                var rowCount = con.SaveChanges();
                Assert.Equal(1, rowCount);
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                Assert.Equal(newName, loaded.Name);
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
                var rowCount = con.SaveChanges();
                Assert.Equal(1, rowCount);
            }

            using (var con = new TastyToGoContext(conString))
            {
                var loaded = con.Products.Find(prod.Id);
                Assert.Null(loaded);
            }
        }

    }
}
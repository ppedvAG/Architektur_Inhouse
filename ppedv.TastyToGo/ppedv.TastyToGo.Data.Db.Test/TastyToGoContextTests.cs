
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


        }
    }
}
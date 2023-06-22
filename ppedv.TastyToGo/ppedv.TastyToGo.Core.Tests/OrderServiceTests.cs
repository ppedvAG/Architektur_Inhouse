using Moq;
using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Core.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void GetBestPayingCustomer_0_customers_returns_null()
        {
            var mock = new Mock<IRepository>();
            var orderService = new OrderService(mock.Object);

            var result = orderService.GetBestPayingCustomer();

            Assert.Null(result);
            mock.Verify(x => x.GetAll<Customer>(), Times.Once());
            mock.Verify(x => x.SaveAll(), Times.Never());
        }

        [Fact]
        public void GetBestPayingCustomer_2_customers_with_same_order_sum_return_the_customer_with_last_orderDate()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Customer>()).Returns(() =>
            {
                var p = new Product() { Name = "P1" };
                var c1 = new Customer() { Name = "C1" };
                var c1o1 = new Order() { Customer = c1, OrderDate = DateTime.Now.AddDays(-10) };
                c1.Orders.Add(c1o1);
                c1o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 7m, Product = p, Order = c1o1 });

                var c2 = new Customer() { Name = "C2" };
                var c2o1 = new Order() { Customer = c2, OrderDate = DateTime.Now.AddDays(-1) };
                c2.Orders.Add(c2o1);
                c2o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 7m, Product = p, Order = c2o1 });

                return new[] { c1, c2 };
            });
            var orderService = new OrderService(mock.Object);

            var result = orderService.GetBestPayingCustomer();

            Assert.Equal("C2", result.Name);
        }

        [Fact]
        public void GetBestPayingCustomer_3_customers_number_2_is_best_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Customer>()).Returns(() =>
            {
                var p = new Product() { Name = "P1" };
                var c1 = new Customer() { Name = "C1" };
                var c1o1 = new Order() { Customer = c1 };
                c1.Orders.Add(c1o1);
                c1o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 7m, Product = p, Order = c1o1 });

                var c2 = new Customer() { Name = "C2" };
                var c2o1 = new Order() { Customer = c2 };
                c2.Orders.Add(c2o1);
                c2o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 6m, Product = p, Order = c2o1 });
                c2o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 16m, Product = p, Order = c2o1 });

                var c3 = new Customer() { Name = "C3" };
                var c3o1 = new Order() { Customer = c3 };
                c2.Orders.Add(c3o1);
                c3o1.OrderItems.Add(new OrderItem() { Amount = 2, Price = 7m, Product = p, Order = c3o1 });

                return new[] { c1, c2, c3 };
            });
            var orderService = new OrderService(mock.Object);

            var result = orderService.GetBestPayingCustomer();

            Assert.Equal("C2", result.Name);
        }


        [Fact]
        public void GetBestPayingCustomer_3_customers_number_2_is_best()
        {
            var orderService = new OrderService(new TestRepo());

            var result = orderService.GetBestPayingCustomer();

            Assert.Equal("C2", result.Name);
        }

        class TestRepo : IRepository
        {
            public void Add<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public void Delete<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetAll<T>() where T : Entity
            {
                if (typeof(T).IsAssignableFrom(typeof(Customer)))
                {
                    var p = new Product() { Name = "P1" };
                    var c1 = new Customer() { Name = "C1" };
                    var c1o1 = new Order() { Customer = c1 };
                    c1.Orders.Add(c1o1);
                    c1o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 7m, Product = p, Order = c1o1 });

                    var c2 = new Customer() { Name = "C2" };
                    var c2o1 = new Order() { Customer = c2 };
                    c2.Orders.Add(c2o1);
                    c2o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 6m, Product = p, Order = c2o1 });
                    c2o1.OrderItems.Add(new OrderItem() { Amount = 1, Price = 16m, Product = p, Order = c2o1 });

                    var c3 = new Customer() { Name = "C3" };
                    var c3o1 = new Order() { Customer = c3 };
                    c2.Orders.Add(c3o1);
                    c3o1.OrderItems.Add(new OrderItem() { Amount = 2, Price = 7m, Product = p, Order = c3o1 });

                    return new[] { c1, c2, c3 }.Cast<T>();
                }

                throw new NotImplementedException();
            }

            public T? GetById<T>(int id) where T : Entity
            {
                throw new NotImplementedException();
            }

            public void SaveAll()
            {
                throw new NotImplementedException();
            }

            public void Update<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }
        }
    }
}
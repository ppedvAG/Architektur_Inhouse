using Moq;
using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Core.Tests
{
    public class OrderServiceTests
    {

        [Fact]
        public void CalcOrderSum_WithDiscount_ReturnsCorrectSum()
        {
            // Arrange
            var customer = new Customer();
            var order = new Order() { Customer = customer };
            var p = new Product() { Name = "P" };

            var orderItems = new List<OrderItem>
            {
                new OrderItem { Amount = 2, Price = 10,Order=order,Product= p },
                new OrderItem { Amount = 3, Price = 5,Order=order,Product=p }
            };
            order.OrderItems = orderItems;

            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(x => x.DoesCustomerGetRabatt(customer)).Returns(true);

            var orderService = new OrderService(null, mockCustomerService.Object);

            // Act
            decimal result = orderService.CalcOrderSum(order);

            // Assert
            decimal expectedSum = (2 * 10 + 3 * 5) - 10;
            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void CalcOrderSum_WithoutDiscount_ReturnsCorrectSum()
        {
            // Arrange
            // Arrange
            var customer = new Customer();
            var order = new Order() { Customer = customer };
            var p = new Product() { Name = "P" };

            var orderItems = new List<OrderItem>
            {
                new OrderItem { Amount = 2, Price = 10,Order=order,Product= p },
                new OrderItem { Amount = 3, Price = 5,Order=order,Product=p }
            };
            order.OrderItems = orderItems;

            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(x => x.DoesCustomerGetRabatt(customer)).Returns(false);

            var orderService = new OrderService(null, mockCustomerService.Object);

            // Act
            decimal result = orderService.CalcOrderSum(order);

            // Assert
            decimal expectedSum = 2 * 10 + 3 * 5;
            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void GetBestPayingCustomer_0_customers_returns_null()
        {
            var mock = new Mock<IRepository>();
            var orderService = new OrderService(mock.Object, null);

            var result = orderService.GetBestPayingCustomer();

            Assert.Null(result);
            mock.Verify(x => x.Query<Customer>(), Times.Once());
            mock.Verify(x => x.SaveAll(), Times.Never());
        }

        [Fact]
        public void GetBestPayingCustomer_2_customers_with_same_order_sum_return_the_customer_with_last_orderDate()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Customer>()).Returns(() =>
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

                return new[] { c1, c2 }.AsQueryable();
            });
            var orderService = new OrderService(mock.Object, null);

            var result = orderService.GetBestPayingCustomer();

            Assert.Equal("C2", result.Name);
        }

        [Fact]
        public void GetBestPayingCustomer_3_customers_number_2_is_best_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Customer>()).Returns(() =>
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

                return new[] { c1, c2, c3 }.AsQueryable();
            });
            var orderService = new OrderService(mock.Object, null);

            var result = orderService.GetBestPayingCustomer();

            Assert.Equal("C2", result.Name);
        }


        [Fact]
        public void GetBestPayingCustomer_3_customers_number_2_is_best()
        {
            var orderService = new OrderService(new TestRepo(), null);

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

            public IQueryable<T> Query<T>() where T : Entity
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

                    return new[] { c1, c2, c3 }.Cast<T>().AsQueryable();
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

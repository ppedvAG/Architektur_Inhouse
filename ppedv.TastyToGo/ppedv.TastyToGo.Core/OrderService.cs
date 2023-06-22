using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Core
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uow;
        private readonly ICustomerService customerService;

        public OrderService(IUnitOfWork uow, ICustomerService customerService)
        {
            this.uow = uow;
            this.customerService = customerService;
        }

        public decimal CalcOrderSum(Order order)
        {
            if (customerService.DoesCustomerGetRabatt(order.Customer))
            {
                return order.OrderItems.Sum(x => x.Amount * x.Price) - 10;
            }

            return order.OrderItems.Sum(x => x.Amount * x.Price);
        }


        public Customer GetBestPayingCustomer()
        {
            //decimal maxTotalAmount = decimal.MinValue;
            //Customer bestPayingCustomer = null;

            //foreach (var customer in repo.GetAll<Customer>())
            //{
            //    decimal totalAmount = customer.Orders.Sum(order => order.OrderItems.Sum(item => item.Amount * item.Price));

            //    if (totalAmount > maxTotalAmount)
            //    {
            //        maxTotalAmount = totalAmount;
            //        bestPayingCustomer = customer;
            //    }
            //}

            //return bestPayingCustomer;
            //repo.SaveAll();

            //return repo.Query<Customer>().OrderByDescending(c => c.Orders.Sum(o => o.OrderItems.Sum(oi => oi.Amount * oi.Price)))
            //                             .ThenByDescending(x=>x.Orders.Select(x=>x.OrderDate).Max()).FirstOrDefault();
            return uow.CustomerRepo.Query()
           .OrderByDescending(c => c.Orders.SelectMany(x => x.OrderItems).Sum(oi => oi.Amount * oi.Price))
           .ThenByDescending(c => c.Orders.Max(o => o.OrderDate))
           .FirstOrDefault();
        }

    }
}
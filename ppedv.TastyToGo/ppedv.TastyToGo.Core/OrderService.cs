using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Core
{
    public class OrderService
    {
        private readonly IRepository repo;

        public OrderService(IRepository repo)
        {
            this.repo = repo;
        }

        public decimal CalcOrderSum(Order order)
        {
            throw new NotImplementedException();
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

            return repo.Query<Customer>()
           .OrderByDescending(c => c.Orders.SelectMany(x => x.OrderItems).Sum(oi => oi.Amount * oi.Price))
           .ThenByDescending(c => c.Orders.Max(o => o.OrderDate))
           .FirstOrDefault();
        }

    }
}